using HospitalAppointment.WebAPI.Models.ReturnModels;
using HospitalAppointment.WebAPI.Services.Abstracts;
using HospitalAppointment.WebAPI.Services.Dtos.Doctors.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HospitalAppointment.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _doctorService.GetAll();

            if (!result.Success)
            {
                return StatusCode((int)result.StatusCode, result.Message);
            }

            return Ok(result.Data);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] CreateDoctorRequest doctorRequest)
        {
            var result = _doctorService.Add(doctorRequest);

            if (!result.Success)
            {
                return StatusCode((int)result.StatusCode, result.Message);
            }

            return Ok(result.Data);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var result = _doctorService.GetById(id);

            if (!result.Success)
            {
                return StatusCode((int)result.StatusCode, result.Message);
            }

            return Ok(result.Data);
        }

        [HttpDelete("delete/{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var result = _doctorService.Delete(id);

            if (!result.Success)
            {
                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound(result.Message);
                }
                return StatusCode((int)result.StatusCode, result.Message);
            }

            return Ok(result.Data);
        }

      
    }
}
