using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Interface
{
    public interface IPersistencia
    {
        Speciality? GetSpecialityById(Guid id);
        void SaveDoctor(Doctor doctor);

        List<Doctor> GetActiveDoctors();

        Doctor? GetDoctorById(Guid id);

    }
}
