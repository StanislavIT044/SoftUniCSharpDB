namespace VaporStore.DataProcessor
{
	using System;
    using System.Linq;
    using Data;
    using Newtonsoft.Json;

    public static class Serializer
	{
		public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
		{
			var employees = context
                .Genres
                .ToArray()
                .Where(x => genreNames.Contains(x.Name) == true)
                .Select(x => new
                {
                    Id = x.Id,
                    Genre = x.Name,
                    Games = x.Games
                        .Where(g => g.Purchases.Count > 0)
                        .Select(g => new
                        {
                            Id = g.Id,
                            Title = g.Name,
                            Developer = g.Developer.Name,
                            Tags = string.Join(", ", g.GameTags.Select(gt => gt.Tag.Name)),
                            Players = g.Purchases.Count()
                        })
                        .OrderByDescending(g => g.Players)
                        .ThenBy(g => g.Id)
                        .ToList(),
                    TotalPlayers = x.Games.SelectMany(g => g.Purchases).Count(),
                })
                .OrderByDescending(x => x.TotalPlayers)
                .ThenBy(x => x.Id)
                .ToList();

            var jsonOutput = JsonConvert.SerializeObject(employees, Formatting.Indented);

            return jsonOutput;
		}

		public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
		{
			throw new NotImplementedException();
		}
	}
}