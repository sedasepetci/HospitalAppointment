using Azure.Core;
using HospitalAppointment.WebAPI.Models;
using HospitalAppointment.WebAPI.Models.Enums;
using HospitalAppointment.WebAPI.Services.Dtos.Appointments.Requests;
using HospitalAppointment.WebAPI.Services.Dtos.Appointments.Responses;

namespace HospitalAppointment.WebAPI.Services.Mapper
{
    public class AppointmentMapper
    {
        public Appointment ConvertToEntity(CreateAppointmentRequest request)
        {
            return new Appointment
            {
              
                PatientName = request.PatientName,
                DoctorId = request.DoctorId,
               
                AppointmentDate = request.AppointmentDate,
                CreatedDate = request.CreatedDate,
                UpdatedDate = request.UpdatedDate
            };
        }


        public AppointmentResponseDto ConvertToResponse(Appointment appointment)
        {
            return new AppointmentResponseDto(
              
               PatientName: appointment.PatientName, 
               DoctorId: appointment.DoctorId,
             
               AppointmentDate: appointment.AppointmentDate
              
                );
        }

        public List<AppointmentResponseDto> ConvertToResponseList(List<Appointment> appointments)
        {
            return appointments.Select(x => ConvertToResponse(x)).ToList();


        }
       


    }
}
