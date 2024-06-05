using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication2.Models;

namespace WebApplication2.EfConfiguration;

public class PrescriptionMedicamentEfConfiguration : IEntityTypeConfiguration<PrescriptionMedicament>
{
    public void Configure(EntityTypeBuilder<PrescriptionMedicament> builder)
    {
           builder.HasKey(e => new {e.IdMedicament,e.IdPrescription});
           builder.Property(e => e.Dose).IsRequired();
           builder.Property(e => e.Details).IsRequired().HasMaxLength(100);
           
           builder.HasOne(e => e.PrescriptionNavigation)
               .WithMany(p => p.PrescriptionMedicaments)
               .HasForeignKey(e => e.IdPrescription)
               .OnDelete(DeleteBehavior.Cascade);
           
           builder.HasOne(e => e.MedicamentNavigation)
               .WithMany(m => m.PrescriptionMedicaments)
               .HasForeignKey(e => e.IdMedicament)
               .OnDelete(DeleteBehavior.Cascade);
          
    }
}