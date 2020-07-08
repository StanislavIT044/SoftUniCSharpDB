using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P01_HospitalDatabase.Models
{
    public class PatientMedicament
    {
        public PatientMedicament()
        {
            this.Patients = new HashSet<Patient>();
            this.Medicaments = new HashSet<Medicament>();
        }

        [Required, ForeignKey("Patient")]
        public int PatientId { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }

        [Required, ForeignKey("Medicament")]
        public int MedicamentId { get; set; }
        public virtual ICollection<Medicament> Medicaments { get; set; }
    }
}
