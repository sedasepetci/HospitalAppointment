using HospitalAppointment.WebAPI.Models.Enums;

namespace HospitalAppointment.WebAPI.Services.Dtos.Doctors.Responses;

public sealed record DoctorResponseDto
(

    string Name,
    string Branch,
    DateTime CreatedDate,
    DateTime UpdatedDate
    );