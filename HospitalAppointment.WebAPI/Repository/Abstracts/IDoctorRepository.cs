using HospitalAppointment.WebAPI.Models;

namespace HospitalAppointment.WebAPI.Repository.Abstracts
{
    public interface IDoctorRepository
    {
        List<Doctor> GetAll();
        Doctor? GetById(int id);
        Doctor Add(Doctor doctor);
        Doctor Update(Doctor doctor);
        Doctor Delete(int id);
        IQueryable<Doctor> GetAll2();
    }
}
