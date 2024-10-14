using HospitalAppointment.WebAPI.Models;
using HospitalAppointment.WebAPI.Models.ReturnModels;
using HospitalAppointment.WebAPI.Repository.Abstracts;
using HospitalAppointment.WebAPI.Services.Abstracts;
using HospitalAppointment.WebAPI.Services.Dtos.Doctors.Requests;
using HospitalAppointment.WebAPI.Services.Dtos.Doctors.Responses;
using HospitalAppointment.WebAPI.Services.Mapper;
using System.Net;

namespace HospitalAppointment.WebAPI.Services.Concretes
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly DoctorMapper _doctorMapper;

        public DoctorService(IDoctorRepository doctorRepository, DoctorMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _doctorMapper = mapper;
        }

        public ReturnModel<Doctor> Add(CreateDoctorRequest dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return new ReturnModel<Doctor>
                {
                    Success = false,
                    Message = "Doktor ismi boş olamaz.",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            Doctor doctor = _doctorMapper.ConvertToEntity(dto);
            Doctor added = _doctorRepository.Add(doctor);

            return new ReturnModel<Doctor>
            {
                Success = true,
                Data = added,
                Message = "Doktor başarıyla eklendi.",
                StatusCode = HttpStatusCode.OK
            };
        }

        public ReturnModel<Doctor> Delete(int id)
        {
            try
            {
                Doctor doctor = _doctorRepository.Delete(id);
                return new ReturnModel<Doctor>
                {
                    Success = true,
                    Data = doctor,
                    Message = "Doktor başarıyla silindi.",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (ArgumentException ex)
            {
                return new ReturnModel<Doctor>
                {
                    Success = false,
                    Message = ex.Message, 
                    StatusCode = HttpStatusCode.NotFound
                };
            }
        }

        public ReturnModel<List<DoctorResponseDto>> GetAllDoctors()
        {
            var doctors = _doctorRepository.GetAll();

            return new ReturnModel<List<DoctorResponseDto>>
            {
                Success = true,
                Data = doctors.Select(d => _doctorMapper.ConvertToResponse(d)).ToList(),
                Message = "Doktorlar başarıyla alındı.",
                StatusCode = HttpStatusCode.OK
            };
        }

        public ReturnModel<Doctor> Update(Doctor doctor)
        {
            Doctor updated = _doctorRepository.Update(doctor);

            return new ReturnModel<Doctor>
            {
                Success = true,
                Data = updated,
                Message = "Doktor başarıyla güncellendi.",
                StatusCode = HttpStatusCode.OK
            };
        }

        public ReturnModel<List<DoctorResponseDto>> GetAll()
        {
            List<Doctor> doctors = _doctorRepository.GetAll();
            List<DoctorResponseDto> responses = _doctorMapper.ConvertToResponseList(doctors);

            return new ReturnModel<List<DoctorResponseDto>>
            {
                Success = true,
                Data = responses,
                Message = "Doktorlar başarıyla alındı.",
                StatusCode = HttpStatusCode.OK
            };
        }

        public ReturnModel<DoctorResponseDto?> GetById(int id)
        {
            Doctor doctor = _doctorRepository.GetById(id);

            if (doctor == null)
            {
                return new ReturnModel<DoctorResponseDto?>
                {
                    Success = false,
                    Message = "Doktor bulunamadı.",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            DoctorResponseDto dto = _doctorMapper.ConvertToResponse(doctor);

            return new ReturnModel<DoctorResponseDto?>
            {
                Success = true,
                Data = dto,
                Message = "Doktor başarıyla alındı.",
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
