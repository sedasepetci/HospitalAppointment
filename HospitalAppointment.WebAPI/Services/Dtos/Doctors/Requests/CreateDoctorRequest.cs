using HospitalAppointment.WebAPI.Models.Enums;

namespace HospitalAppointment.WebAPI.Services.Dtos.Doctors.Requests;

public sealed record CreateDoctorRequest
(


    string Name,
    Branch Branch,
    DateTime CreatedDate,
    DateTime UpdatedDate
);
