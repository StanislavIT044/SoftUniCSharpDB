namespace VaporStore.DataProcessor
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Globalization;
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Xml.Serialization;
	using Data;
	using Newtonsoft.Json;
	using VaporStore.Data.Models;
	using VaporStore.Data.Models.Enums;
	using VaporStore.DataProcessor.Dto;
	using VaporStore.DataProcessor.Dto.Import;

	public static class Deserializer
	{
		public static string ErrorMessage { get; set; } = "Invalid Data";
		public static string SuccessfulAppendGames { get; set; } = "Added {0} ({1}) with {2} tags";
		public static string SuccessfulAppendUsers { get; set; } = "Imported {0} with {1} cards";
		public static string SuccessfulAppendPurchases { get; set; } = "Imported {0} for {1}";
		public static string ImportGames(VaporStoreDbContext context, string jsonString)
		{
			var info = JsonConvert.DeserializeObject<List<ImportGamesDevsGenresAndTagsDto>>(jsonString);

			StringBuilder sb = new StringBuilder();

			List<Game> newGames = new List<Game>();
			List<Developer> newDevelopers = new List<Developer>();
			List<Genre> newGenres = new List<Genre>();
			List<Tag> newTags = new List<Tag>();

			foreach (var g in info)
			{
				if (IsValid(g) == false)
				{
					sb.AppendLine(ErrorMessage);
					continue;
				}

				if (g.Tags.Length == 0)
				{
					sb.AppendLine(ErrorMessage);
					continue;
				}

				DateTime dateTime = new DateTime();

				if (DateTime.TryParseExact(g.ReleaseDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime) == false)
				{
					sb.AppendLine(ErrorMessage);
					continue;
				}

				Game newGame = new Game
				{
					Price = g.Price,
					Name = g.Name,
					ReleaseDate = dateTime,
					GameTags = new List<GameTag>()
				};
				
				Developer newDeveloper = newDevelopers.FirstOrDefault(x => x.Name == g.Developer);

				if (newDeveloper == null)
				{
					newDeveloper = new Developer { Name = g.Developer };
				}

				newDevelopers.Add(newDeveloper);
				newGame.Developer = newDeveloper;

				Genre newGenre = newGenres.FirstOrDefault(x => x.Name == g.Genre);

				if (newGenre == null)
				{
					newGenre = new Genre { Name = g.Genre };
				}

				newGenres.Add(newGenre);
				newGame.Genre = newGenre;

				foreach (var tag in g.Tags)
				{
					var newTag = newTags.FirstOrDefault(x => x.Name == tag);
					if (newTag == null) newTag = new Tag { Name = tag };
					newTags.Add(newTag);

					newGame.GameTags.Add(new GameTag { Tag = newTag, Game = newGame });
				}

				newGames.Add(newGame);
				sb.AppendLine(string.Format(SuccessfulAppendGames, newGame.Name, newGame.Genre.Name, newGame.GameTags.Count));
			}

			//var debugDevelopersCount = newDevelopers.Select(x => x.Name).Distinct().Count();
			//var debugGenreCount = newGenres.Select(x => x.Name).Distinct().Count();
			//var debugTagsCount = newTags.Select(x => x.Name).Distinct().Count();

			context.Tags.AddRange(newTags);
			context.Genres.AddRange(newGenres);
			context.Developers.AddRange(newDevelopers);
			context.Games.AddRange(newGames);
			context.SaveChanges();

			return sb.ToString();
		}

		public static string ImportUsers(VaporStoreDbContext context, string jsonString)
		{
			var info = JsonConvert.DeserializeObject<List<ImportUsersАndCardsDto>>(jsonString);

			StringBuilder sb = new StringBuilder();
			List<User> newUsers = new List<User>();
			List<Card> newCards = new List<Card>();

			foreach (var u in info)
			{
				if (IsValid(u) == false)
				{
					sb.AppendLine(ErrorMessage);
					continue;
				}

				var newUser = new User
				{
					Age = u.Age,
					FullName = u.FullName,
					Username = u.Username,
					Email = u.Email,
					Cards = new List<Card>()
				};

				var validCard = true;

				foreach (var c in u.Cards)
				{
					if (IsValid(c) == false)
					{
						sb.AppendLine(ErrorMessage);
						validCard = false;
						break;
					}

					var validType = new object { };
					if (Enum.TryParse(typeof(CardType), c.Type, false, out validType) == false)
					{
						sb.AppendLine(ErrorMessage);
						validCard = false;
						break;
					}

					var newCard = new Card { Cvc = c.Cvc, Number = c.Number, Type = (CardType)Enum.Parse(typeof(CardType), c.Type) };
					newUser.Cards.Add(newCard);
				}

				if (validCard == false)
				{
					continue;
				}

				newUsers.Add(newUser);

				foreach (var card in newUser.Cards)
				{
					newCards.Add(card);
				}

				sb.AppendLine(String.Format(SuccessfulAppendUsers, newUser.Username, newUser.Cards.Count));
			}

			var debugCardsCount = newCards.Count;

			context.Cards.AddRange(newCards);
			context.Users.AddRange(newUsers);
			context.SaveChanges();


			return sb.ToString();
		}

		public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
		{
			var sb = new StringBuilder();
			var serializer = new XmlSerializer(typeof(List<ImportPurchasesDto>), new XmlRootAttribute("Purchases"));
			var info = (List<ImportPurchasesDto>)serializer.Deserialize(new StringReader(xmlString));
			var purchases = new List<Purchase>();

			foreach (var purchase in info)
			{
				if (IsValid(purchase) == false)
				{
					sb.AppendLine(ErrorMessage);
					continue;
				}

				var newDate = new DateTime();

				if (DateTime.TryParseExact(purchase.Date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out newDate) == false)
				{
					sb.AppendLine(ErrorMessage);
					continue;
				}

				var purchaseCard = context.Cards.FirstOrDefault(card => card.Number == purchase.Card);
				if (purchaseCard == null)
				{
					sb.AppendLine(ErrorMessage);
					continue;
				}

				var purchaseGame = context.Games.FirstOrDefault(game => game.Name == purchase.Title);
				if (purchaseGame == null)
				{
					sb.AppendLine(ErrorMessage);
					continue;
				}

				var purchaseValid = new object { };

				if (Enum.TryParse(typeof(PurchaseType), purchase.Type, out purchaseValid) == false)
				{
					sb.AppendLine(ErrorMessage);
					continue;
				}

				var newPurchase = new Purchase
				{
					Date = newDate,
					ProductKey = purchase.ProductKey,
					Type = (PurchaseType)Enum.Parse(typeof(PurchaseType), purchase.Type),
					Card = purchaseCard,
					Game = purchaseGame
				};
				purchases.Add(newPurchase);
				sb.AppendLine(string.Format(SuccessfulAppendPurchases, newPurchase.Game.Name, newPurchase.Card.User.Username));
			}

			context.Purchases.AddRange(purchases);
			context.SaveChanges();
			return sb.ToString();
		}

		private static bool IsValid(object dto)
		{
			var validationContext = new ValidationContext(dto);
			var validationResult = new List<ValidationResult>();

			return Validator.TryValidateObject(dto, validationContext, validationResult, true);
		}
	}
}