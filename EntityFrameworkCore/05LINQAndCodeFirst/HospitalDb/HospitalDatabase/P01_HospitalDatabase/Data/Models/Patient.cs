using System.ComponentModel.DataAnnotations;

namespace P01_HospitalDatabase.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, MaxLength(250)]
        public string Address { get; set; }

        [Required, MaxLength(80)]
        public string Email { get; set; }

        public bool HasInsurance { get; set; }
    }
}
