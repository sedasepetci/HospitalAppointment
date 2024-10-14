using System.Net;
using HospitalAppointment.WebAPI.Models;
using HospitalAppointment.WebAPI.Models.ReturnModels;
using HospitalAppointment.WebAPI.Repository.Abstracts;
using HospitalAppointment.WebAPI.Services.Abstracts;
using HospitalAppointment.WebAPI.Services.Dtos.Appointments.Requests;
using HospitalAppointment.WebAPI.Services.Dtos.Appointments.Responses;
using HospitalAppointment.WebAPI.Services.Mapper;
using HospitalAppointment.WebAPI.Exceptions;

namespace HospitalAppointment.WebAPI.Services.Concretes
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly AppointmentMapper _appointmentMapper;

        public AppointmentService(IAppointmentRepository appointmentRepository, AppointmentMapper appointmentMapper, IDoctorRepository doctorRepository)
        {
            _appointmentRepository = appointmentRepository;
            _appointmentMapper = appointmentMapper;
            _doctorRepository = doctorRepository;
        }

        public ReturnModel<Appointment> Add(CreateAppointmentRequest dto)
        {
            try
            {
                ValidateAppointmentRequest(dto);

                var existingDoctor = _doctorRepository.GetById(dto.DoctorId);
                if (existingDoctor == null)
                {
                    return new ReturnModel<Appointment>
                    {
                        Success = false,
                        Message = "Doktor bulunamadı.",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                if (!CanAddAppointment(dto.DoctorId))
                {
                    return new ReturnModel<Appointment>
                    {
                        Success = false,
                        Message = "Bir doktor en fazla 10 randevuya sahip olabilir.",
                        StatusCode = HttpStatusCode.BadRequest
                    };
                }

                Appointment appointment = _appointmentMapper.ConvertToEntity(dto);
                appointment.DoctorId = existingDoctor.Id;

                var addedAppointment = _appointmentRepository.Add(appointment);
                return new ReturnModel<Appointment>
                {
                    Success = true,
                    Data = addedAppointment,
                    Message = "Randevu başarıyla eklendi.",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (ValidationException ex)
            {
                return new ReturnModel<Appointment>
                {
                    Success = false,
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
            catch (Exception ex)
            {
                return new ReturnModel<Appointment>
                {
                    Success = false,
                    Message = $"İç hata: {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public ReturnModel<Appointment> Delete(Guid id)
        {
            try
            {
                var appointment = _appointmentRepository.GetById(id);
                if (appointment == null)
                {
                    return new ReturnModel<Appointment>
                    {
                        Success = false,
                        Message = "Randevu bulunamadı.",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                _appointmentRepository.Delete(id);
                return new ReturnModel<Appointment>
                {
                    Success = true,
                    Message = "Randevu başarıyla silindi.",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new ReturnModel<Appointment>
                {
                    Success = false,
                    Message = $"İç hata: {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public ReturnModel<List<AppointmentResponseDto>> GetAll()
        {
            try
            {
                var appointments = _appointmentRepository.GetAll();
                var responseDtos = _appointmentMapper.ConvertToResponseList(appointments);

                return new ReturnModel<List<AppointmentResponseDto>>
                {
                    Success = true,
                    Data = responseDtos,
                    Message = "Randevular başarıyla listelendi.",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new ReturnModel<List<AppointmentResponseDto>>
                {
                    Success = false,
                    Message = $"İç hata: {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public ReturnModel<AppointmentResponseDto> GetById(Guid id)
        {
            try
            {
                var appointment = _appointmentRepository.GetById(id);
                if (appointment == null)
                {
                    return new ReturnModel<AppointmentResponseDto>
                    {
                        Success = false,
                        Message = "Randevu bulunamadı.",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var responseDto = _appointmentMapper.ConvertToResponse(appointment);
                return new ReturnModel<AppointmentResponseDto>
                {
                    Success = true,
                    Data = responseDto,
                    Message = "Randevu başarıyla bulundu.",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new ReturnModel<AppointmentResponseDto>
                {
                    Success = false,
                    Message = $"İç hata: {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public ReturnModel<Appointment> Update(Appointment appointment)
        {
            try
            {
                ValidateAppointment(appointment);

                var existingAppointment = _appointmentRepository.GetById(appointment.Id);
                if (existingAppointment == null)
                {
                    return new ReturnModel<Appointment>
                    {
                        Success = false,
                        Message = "Randevu bulunamadı.",
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var updatedAppointment = _appointmentRepository.Update(appointment);
                return new ReturnModel<Appointment>
                {
                    Success = true,
                    Data = updatedAppointment,
                    Message = "Randevu başarıyla güncellendi.",
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (ValidationException ex)
            {
                return new ReturnModel<Appointment>
                {
                    Success = false,
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
            catch (Exception ex)
            {
                return new ReturnModel<Appointment>
                {
                    Success = false,
                    Message = $"İç hata: {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        private bool CanAddAppointment(int doctorId)
        {
            int appointmentCount = _appointmentRepository.GetAppointmentsByDoctorId(doctorId).Count();
            return appointmentCount < 10;
        }

        private void ValidateAppointmentRequest(CreateAppointmentRequest dto)
        {
            if (dto.DoctorId <= 0)
            {
                throw new ValidationException("Geçersiz doktor ID.");
            }

            if (string.IsNullOrWhiteSpace(dto.PatientName))
            {
                throw new ValidationException("Hasta ismi boş olamaz.");
            }

            if (dto.AppointmentDate < DateTime.UtcNow.AddDays(3))
            {
                throw new ValidationException("Randevu tarihi en az 3 gün sonrasına olmalıdır.");
            }
        }

        private void ValidateAppointment(Appointment appointment)
        {
            if (string.IsNullOrWhiteSpace(appointment.PatientName))
            {
                throw new ValidationException("Hasta ismi boş olamaz.");
            }

            if (appointment.AppointmentDate < DateTime.UtcNow.AddDays(3))
            {
                throw new ValidationException("Randevu tarihi en az 3 gün sonrasına olmalıdır.");
            }
        }

        bool IAppointmentService.CanAddAppointment(int doctorId)
        {
            throw new NotImplementedException();
        }
    }
}
