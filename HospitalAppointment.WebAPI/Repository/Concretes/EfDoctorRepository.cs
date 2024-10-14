using HospitalAppointment.WebAPI.Contexts;
using HospitalAppointment.WebAPI.Models;
using HospitalAppointment.WebAPI.Repository.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointment.WebAPI.Repository.Concretes
{
    public class EfDoctorRepository : IDoctorRepository
    {
        private MsSqlContext _context;

        public EfDoctorRepository(MsSqlContext context)
        {
            _context = context;
        }

        public Doctor Add(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
            return doctor;
        }

        public Doctor Delete(int id)
        {
            Doctor doctor = GetById(id);
            _context.Doctors.Remove(doctor);
            _context.SaveChanges();
            return doctor;
        }

        public List<Doctor> GetAll()
        {
            return _context.Doctors.Include(x => x.Appointments).ToList();
        }

        public IQueryable<Doctor> GetAll2()
        {
            return _context.Set<Doctor>().AsQueryable();
        }

        public Doctor? GetById(int id)
        {

            Doctor? doctor= _context.Doctors.Find(id);
            Doctor? doctor2 = _context.Doctors.Include(x => x.Appointments).SingleOrDefault(x => x.Id == id);
            return doctor;
        }
      
        public Doctor Update(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            _context.SaveChanges();
            return doctor;
        }
    }
}
