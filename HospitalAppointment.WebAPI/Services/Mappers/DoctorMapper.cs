using HospitalAppointment.WebAPI.Models;
using HospitalAppointment.WebAPI.Models.Enums;
using HospitalAppointment.WebAPI.Services.Dtos.Doctors.Requests;
using HospitalAppointment.WebAPI.Services.Dtos.Doctors.Responses;

namespace HospitalAppointment.WebAPI.Services.Mapper
{
    public class DoctorMapper
    {
        public Doctor ConvertToEntity(CreateDoctorRequest request)
        {
            return new Doctor
            {
              
                Name = request.Name,
                Branch = request.Branch,
                CreatedDate = request.CreatedDate,
                UpdatedDate = request.UpdatedDate
            };
        }


        public DoctorResponseDto ConvertToResponse(Doctor doctor)
        {
            return new DoctorResponseDto(
              
                Name: doctor.Name,
                Branch: doctor.Branch.ToString(),
                CreatedDate: doctor.CreatedDate,
                UpdatedDate: doctor.UpdatedDate
                );
        }

        public List<DoctorResponseDto> ConvertToResponseList(List<Doctor> doctors)
        {
            return doctors.Select(x => ConvertToResponse(x)).ToList();
        }
    }
}
