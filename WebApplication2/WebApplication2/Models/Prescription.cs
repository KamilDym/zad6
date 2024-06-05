namespace WebApplication2.Models;

public class Prescription
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }

    public int IdPatient { get; set; }
    public virtual Patient PatientNavigator { get; set; }

    public int IdDoctor { get; set; }
    public virtual Doctor DoctorNavigator { get; set; }

    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
}
