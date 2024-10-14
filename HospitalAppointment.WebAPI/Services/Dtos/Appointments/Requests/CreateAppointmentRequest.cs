using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalAppointment.WebAPI.Services.Dtos.Appointments.Requests
{
    public sealed record CreateAppointmentRequest
    (
        [Required(ErrorMessage = "Hasta adı boş olamaz.")]
        string PatientName,

        [Required(ErrorMessage = "Doktor ID'si boş olamaz.")]
        int DoctorId,

        [Required(ErrorMessage = "Randevu tarihi boş olamaz.")]
        DateTime AppointmentDate,

        DateTime CreatedDate = default,
        DateTime UpdatedDate = default
    )
    {
        public CreateAppointmentRequest(string patientName, int doctorId, DateTime appointmentDate)
            : this(patientName, doctorId, appointmentDate, DateTime.UtcNow, DateTime.UtcNow)
        {
        }
    }
}
