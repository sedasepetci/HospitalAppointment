using HospitalAppointment.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointment.WebAPI.Contexts
{
    public class MsSqlContext:DbContext
    {
        public MsSqlContext(DbContextOptions opt) : base(opt)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Hospital_db;User=sa;Password=admin12345678 ;TrustServerCertificate=true");
        }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
       
    }
}
