using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace P01_HospitalDatabase.Data
{
    public class HospitalContext : DbContext
    {
        public DbSet<Diagnose> Diagnoses { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientMedicament> PatientsMedicaments { get; set; }
        public DbSet<Visitation> Visitations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigurationOnDiagnose(modelBuilder);
            ConfigurationOnMedicament(modelBuilder);
            ConfigurationOnPatient(modelBuilder);
            ConfigurationOnPatientMedicament(modelBuilder);
            ConfigurationOnVisitation(modelBuilder);
            ConfigurationOnDoctor(modelBuilder);
        }

        private void ConfigurationOnDoctor(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Doctor>()
                .HasKey(k => k.DoctorId);
            modelBuilder
                .Entity<Doctor>()
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsUnicode();
            modelBuilder
                .Entity<Doctor>()
                .Property(p => p.Specialty)
                .HasMaxLength(100)
                .IsUnicode();
            modelBuilder
                .Entity<Doctor>()
                .HasMany(v => v.Visitations)
                .WithOne(d => d.Doctor)
                .HasForeignKey(k => k.DoctorId);
        }

        private void ConfigurationOnVisitation(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Visitation>()
                .HasKey(k => k.VisitationId);
            modelBuilder
                .Entity<Visitation>()
                .Property(p => p.Date);
            modelBuilder
                .Entity<Visitation>()
                .Property(p => p.Comments)
                .HasMaxLength(250)
                .IsUnicode();
            modelBuilder
                .Entity<Visitation>()
                .HasOne(p => p.Patient)
                .WithMany(v => v.Visitations)
                .HasForeignKey(k => k.PatientId);
            modelBuilder
                .Entity<Visitation>()
                .HasOne(d => d.Doctor)
                .WithMany(v => v.Visitations)
                .HasForeignKey(k => k.DoctorId);

        }

        private void ConfigurationOnPatientMedicament(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<PatientMedicament>()
                .HasKey(k => new { k.MedicamentId, k.PatientId });
            modelBuilder
                .Entity<PatientMedicament>()
                .HasOne(p => p.Medicament)
                .WithMany(pm => pm.Prescriptions)
                .HasForeignKey(k => k.MedicamentId);
            modelBuilder
                .Entity<PatientMedicament>()
                .HasOne(p => p.Patient)
                .WithMany(pm => pm.Prescription)
                .HasForeignKey(k => k.PatientId);

        }

        private void ConfigurationOnPatient(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Patient>()
                .HasKey(k => k.PatientId);
            modelBuilder
                .Entity<Patient>()
                .Property(p => p.FirstName)
                .HasMaxLength(50)
                .IsUnicode();
            modelBuilder
                .Entity<Patient>()
                .Property(p => p.LastName)
                .HasMaxLength(50)
                .IsUnicode();
            modelBuilder
                .Entity<Patient>()
                .Property(p => p.Address)
                .HasMaxLength(250)
                .IsUnicode();
            modelBuilder
                .Entity<Patient>()
                .Property(p => p.Email)
                .HasMaxLength(80);
            modelBuilder
                .Entity<Patient>()
                .Property(p => p.HasInsurance);
            modelBuilder
                .Entity<Patient>()
                .HasMany(p => p.Prescription)
                .WithOne(p => p.Patient)
                .HasForeignKey(k => k.PatientId);
            modelBuilder
                .Entity<Patient>()
                .HasMany(p => p.Diagnoses)
                .WithOne(p => p.Patient)
                .HasForeignKey(k => k.PatientId);
            modelBuilder
                .Entity<Patient>()
                .HasMany(p => p.Visitations)
                .WithOne(p => p.Patient)
                .HasForeignKey(k => k.PatientId);

        }

        private void ConfigurationOnMedicament(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Medicament>()
                .HasKey(k => k.MedicamentId);
            modelBuilder
                .Entity<Medicament>()
                .Property(p => p.Name)
                .HasMaxLength(50)
                .IsUnicode();
            modelBuilder
                .Entity<Medicament>()
                .HasMany(p => p.Prescriptions)
                .WithOne(m => m.Medicament)
                .HasForeignKey(k => k.MedicamentId);

        }

        private void ConfigurationOnDiagnose(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Diagnose>()
                .HasKey(k => k.DiagnoseId);

            modelBuilder
                .Entity<Diagnose>()
                .Property(p => p.Name)
                .HasMaxLength(50)
                .IsUnicode();
            modelBuilder
                .Entity<Diagnose>()
                .Property(p => p.Comments)
                .HasMaxLength(250)
                .IsUnicode();
            modelBuilder
                .Entity<Diagnose>()
                .HasOne(p => p.Patient)
                .WithMany(d => d.Diagnoses)
                .HasForeignKey(k => k.PatientId);
        }
    }
}
