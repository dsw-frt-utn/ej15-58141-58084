using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Dsw2026Ej15.Domain.Entities;

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
        public async Task<IActionResult> CreateDoctor(DoctorModel.Request request)
        {
            if(string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.LicenceNumber))
            {
                return BadRequest("Nombre y Matricula son requeridos");
            }

            var speciality = _persistencia.GetSpecialityById(request.SpecialityId);
            if (speciality == null)
            {
                return BadRequest("Especialidad no Existe");
            }
            var doctor = new Doctor(request.Name, request.LicenceNumber, speciality, request.SpecialityId);

            _persistencia.SaveDoctor(doctor);

            return Created();

        }

    }
}
