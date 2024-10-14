using HospitalAppointment.WebAPI.Models;

namespace HospitalAppointment.WebAPI.Repository.Abstracts
{
    public interface IAppointmentRepository
    {
        List<Appointment> GetAll();
        Appointment? GetById(Guid id);
        Appointment Add(Appointment appointment);
        Appointment Update(Appointment appointment);
        Appointment Delete(Guid id);
        IQueryable<Appointment> GetAppointmentsByDoctorId(int doctorId);
    }
}
