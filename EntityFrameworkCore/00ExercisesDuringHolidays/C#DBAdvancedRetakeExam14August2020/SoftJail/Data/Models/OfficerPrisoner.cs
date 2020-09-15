namespace SoftJail.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class OfficerPrisoner
    {
        [ForeignKey(nameof(Prisoner))]
        //Should be PK
        public int PrisonerId { get; set; }
        public Prisoner Prisoner { get; set; }

        [ForeignKey(nameof(Officer))]
        //Should be PK
        public int OfficerId { get; set; }
        public Officer Officer { get; set; }
    }
}
