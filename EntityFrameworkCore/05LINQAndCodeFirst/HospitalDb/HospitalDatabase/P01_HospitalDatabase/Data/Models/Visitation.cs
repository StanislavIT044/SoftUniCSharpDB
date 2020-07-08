using System;
using System.ComponentModel.DataAnnotations;

namespace P01_HospitalDatabase.Models
{
    public class Visitation
    {
        [Key]
        public int VisitationId { get; set; }

        public DateTime Date { get; set; }

        [MaxLength(50)]
        public string Comments { get; set; }

        public Patient Patient { get; set; }
    }
}
