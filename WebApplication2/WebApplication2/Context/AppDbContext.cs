using Microsoft.EntityFrameworkCore;
using WebApplication2.EfConfiguration;
using WebApplication2.Models;

namespace WebApplication2.Context;

public class AppDbContext : DbContext
{

    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    
    public AppDbContext() { }

    public AppDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Doctor>(builder =>
        // {
        //     builder.HasKey(e => e.IdDoctor);
        //     builder.Property(e => e.IdDoctor).ValueGeneratedOnAdd();
        //
        //     builder.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
        //     builder.Property(e => e.LastName).IsRequired().HasMaxLength(100);
        //     builder.Property(e => e.Email).IsRequired().HasMaxLength(100);
        // });

        modelBuilder.ApplyConfiguration(new DoctorEfConfiguration());
        modelBuilder.ApplyConfiguration(new MedicamentEfConfiguration());
        modelBuilder.ApplyConfiguration(new PatientEfConfiguration());
        modelBuilder.ApplyConfiguration(new PrescriptionEfConfiguration());
        modelBuilder.ApplyConfiguration(new PrescriptionMedicamentEfConfiguration());

        // modelBuilder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);

    }
}