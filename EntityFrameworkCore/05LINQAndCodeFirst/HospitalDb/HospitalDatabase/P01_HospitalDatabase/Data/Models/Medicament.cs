﻿using System.ComponentModel.DataAnnotations;

namespace P01_HospitalDatabase.Models
{
    public class Medicament
    {
        [Key]
        public int MedicamentId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
    }
}
