using HospitalAppointment.WebAPI.Services.Abstracts;
using HospitalAppointment.WebAPI.Services.Dtos.Appointments.Requests;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _appointmentService.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }

            return StatusCode((int)result.StatusCode, result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] CreateAppointmentRequest appointmentRequest)
        {
            var result = _appointmentService.Add(appointmentRequest);

            if (result.Success)
            {
                return Ok(result);
            }

            return StatusCode((int)result.StatusCode, result.Message);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var result = _appointmentService.GetById(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return StatusCode((int)result.StatusCode, result.Message);
        }

        [HttpDelete("delete/{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var result = _appointmentService.Delete(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return StatusCode((int)result.StatusCode, result.Message);
        }
    }
}
