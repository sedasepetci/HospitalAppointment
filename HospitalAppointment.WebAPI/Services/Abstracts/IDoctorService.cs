using HospitalAppointment.WebAPI.Models;
using HospitalAppointment.WebAPI.Models.ReturnModels;
using HospitalAppointment.WebAPI.Services.Dtos.Doctors.Requests;
using HospitalAppointment.WebAPI.Services.Dtos.Doctors.Responses;

namespace HospitalAppointment.WebAPI.Services.Abstracts
{
    public interface IDoctorService
    {
        ReturnModel<List<DoctorResponseDto>> GetAll();
        ReturnModel<DoctorResponseDto?> GetById(int id);
        ReturnModel<Doctor> Add(CreateDoctorRequest doctor);
        ReturnModel<Doctor> Update(Doctor doctor);
        ReturnModel<Doctor> Delete(int id);
        ReturnModel<List<DoctorResponseDto>> GetAllDoctors();
    }
}
