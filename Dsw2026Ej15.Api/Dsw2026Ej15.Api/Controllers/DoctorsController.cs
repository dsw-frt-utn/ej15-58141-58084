using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Exceptions;
using Dsw2026Ej15.Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Dsw2026Ej15.Domain.Exceptions;

namespace Dsw2026Ej15.Api.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorsController : ControllerBase
    {
        private readonly IPersistencia _persistencia;

        public DoctorsController(IPersistencia persistencia)
        {
            _persistencia = persistencia;
        }
        [HttpPost]
        public IActionResult CreateDoctor(DoctorModel.Request request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) ||
        string.IsNullOrWhiteSpace(request.LicenceNumber))
            {
                throw new ValidationException(
                    "Nombre y Matricula son requeridos");
            }

            var speciality = _persistencia.GetSpecialityById(request.SpecialityId);

            if (speciality == null)
            {
                throw new ValidationException(
                    "Especialidad no Existe");
            }

        }

        [HttpGet]
        public IActionResult GetDoctors()
        {
            var doctors = _persistencia
                .GetActiveDoctors()
                .Select(d => new
                {
                    d.Id,
                    d.Name,
                    d.LicenceNumber,
                    SpecialityName = d.Speciality.Name
                });

            return Ok(doctors);
            }
        [HttpGet("{id}")]
        public IActionResult GetDoctor(Guid id)
        {
            var doctor = _persistencia.GetDoctorById(id);

            if (doctor == null || !doctor.IsActive)
                return NotFound();

            return Ok(new
            {
                doctor.Name,
                doctor.LicenceNumber,
                SpecialityName = doctor.Speciality.Name
            });
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(Guid id)
        {
            var doctor = _persistencia.GetDoctorById(id);

            if (doctor == null || !doctor.IsActive)
                return NotFound();

            doctor.IsActive = false;

            return NoContent();
        }
    }

    }
    
    
