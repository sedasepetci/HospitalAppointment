using HospitalAppointment.WebAPI.Models;
using HospitalAppointment.WebAPI.Models.ReturnModels;
using HospitalAppointment.WebAPI.Services.Dtos.Appointments.Requests;
using HospitalAppointment.WebAPI.Services.Dtos.Appointments.Responses;

namespace HospitalAppointment.WebAPI.Services.Abstracts
{
    public interface IAppointmentService
    {
        ReturnModel<List<AppointmentResponseDto>> GetAll();
        ReturnModel<AppointmentResponseDto> GetById(Guid id);
        ReturnModel<Appointment> Add(CreateAppointmentRequest appointment);
        ReturnModel<Appointment> Update(Appointment appointment);
        ReturnModel<Appointment> Delete(Guid id);
        bool CanAddAppointment(int doctorId);
    }
}
