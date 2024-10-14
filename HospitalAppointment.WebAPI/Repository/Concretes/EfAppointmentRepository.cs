
using HospitalAppointment.WebAPI.Contexts;
using HospitalAppointment.WebAPI.Repository.Abstracts;
using HospitalAppointment.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointment.WebAPI.Repository.Concretes
{
    public class EfAppointmentRepository : IAppointmentRepository
    {
        private readonly MsSqlContext _context;

        public EfAppointmentRepository(MsSqlContext context)
        {
            _context = context;
        }
        public IQueryable<Appointment> GetAppointmentsByDoctorId(int doctorId)
        {
            return _context.Set<Appointment>().Where(a => a.DoctorId == doctorId);
        }
        public Appointment Add(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
            return appointment;
        }

        public Appointment Delete(Guid id)
        {
            Appointment appointment = GetById(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                _context.SaveChanges();
            }
            return appointment;
        }

        public List<Appointment> GetAll()
        {
           
            return _context.Appointments
                           .Include(x => x.Doctor) 
                           .ToList();
        }

        public Appointment? GetById(Guid id)
        {
           
            return _context.Appointments
                           .Include(x => x.Doctor) 
                           .FirstOrDefault(x => x.Id == id); 
        }

        public Appointment Update(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            _context.SaveChanges();
            return appointment;
        }
    }
}