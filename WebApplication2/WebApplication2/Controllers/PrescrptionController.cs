using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Context;
using WebApplication2.DTOs;
using WebApplication2.Models;

namespace WebApplication2.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class PrescriptionController : ControllerBase
{
    private readonly AppDbContext _context;
    public PrescriptionController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    [Route("addPrescription")]
    public async Task<IActionResult> AddPrescription([FromBody] AddPrescriptionDTO request)
    {
        if (request.Medicaments.Count > 10)
        {
            return BadRequest("A prescription can include a maximum of 10 medicaments.");
        }

        if (request.DueDate < request.Date)
        {
            return BadRequest("DueDate cannot be earlier than Date.");
        }
        
        var doctor = await _context.Doctors.FindAsync(request.DoctorId);
        if (doctor == null)
        {
            return BadRequest("Doctor does not exist.");
        }
        
        foreach (var medicamentDto in request.Medicaments)
        {
            var medicament = await _context.Medicaments.FindAsync(medicamentDto.IdMedicament);
            if (medicament == null)
            {
                return BadRequest($"Medicament with ID {medicamentDto.IdMedicament} does not exist.");
            }
            
        }

        var patient = await _context.Patients.FindAsync(request.PatientId);
        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = request.PatientFirstName,
                LastName = request.PatientLastName,
                Birthdate = request.PatientBirthdate
            };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }
        
        var prescription = new Prescription
        {
            Date = request.Date,
            DueDate = request.DueDate,
            IdPatient = patient.IdPatient,
            IdDoctor = doctor.IdDoctor
        };
        
        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();
        
        
        
        foreach (var medicamentDto in request.Medicaments)
        {   
            var prescriptionMedicament = new PrescriptionMedicament
            {
                IdMedicament = medicamentDto.IdMedicament,
                IdPrescription = prescription.IdPrescription,
                Dose = medicamentDto.Dose,
                Details = medicamentDto.Details
            };

            _context.PrescriptionMedicaments.Add(prescriptionMedicament);
            
        }
        await _context.SaveChangesAsync();
        Console.WriteLine("---------------end");

        return Created();
    }

    
}