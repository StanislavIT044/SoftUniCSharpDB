using System.ComponentModel.DataAnnotations;

namespace P01_HospitalDatabase.Models
{
    public class Diagnose
    {
        [Key]
        public int DiagnoseId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Comments { get; set; }

        public Patient Patient { get; set; }
    }
}
