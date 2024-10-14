using System;

namespace HospitalAppointment.WebAPI.Services.Dtos.Appointments.Responses
{
    public sealed record AppointmentResponseDto
    (
        string PatientName,        
        int DoctorId,          
        DateTime AppointmentDate  
    )
    {
        public override string ToString()
        {
            return $"Hasta Adı: {PatientName}, Doktor ID: {DoctorId}, Randevu Tarihi: {AppointmentDate}";
        }
    }
}
