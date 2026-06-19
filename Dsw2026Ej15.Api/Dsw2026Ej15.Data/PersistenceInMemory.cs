using System;
using System.Collections.Generic;
using System.Text;
using Dsw2026Ej15.Domain.Interface;
using Dsw2026Ej15.Domain.Entities;
using System.Text.Json;
using Dsw2026Ej15.Data.Dtos;

namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistencia
    {
        private List<Speciality> _specialities = [];
        private List<Doctor> _doctors = []; 

        public PersistenceInMemory()
        {
            LoadSpecialities();
        }

        private void LoadSpecialities()
        {
            try
            {
                string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Source", "specialities.json");
                var json = File.ReadAllText(jsonPath);
                var specialities = JsonSerializer.Deserialize<List<SpecialityDto>>(json,new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? [];
                _specialities = [.. specialities.Select(s => new Speciality(s.Name, s.Description, s.Id))];

            }
            catch (Exception)
            {

                
            }
        }
        public Speciality? GetSpecialityById(Guid id)
        {
            return _specialities.FirstOrDefault(s => s.Id == id);
        }

        public void SaveDoctor(Doctor doctor)
        {
            _doctors.Add(doctor);
        }
        public List<Doctor> GetActiveDoctors()
        {
            return _doctors.Where(d => d.IsActive).ToList();
        }
        public Doctor? GetDoctorById(Guid id)
        {
            return _doctors.FirstOrDefault(d => d.Id == id);
        }
    }
}
