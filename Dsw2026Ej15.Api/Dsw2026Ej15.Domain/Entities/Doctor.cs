using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Entities
{
    public class Doctor : BaseEntity
    {
        public object LicenceNumber;

        public string Name { get; set; }

        public string LicenseNumber { get; set; }

        public bool IsActive { get; set; } = true;

        public Speciality Speciality { get; set; }


        public Doctor(string name, string licenseNumber, Speciality speciality, Guid? id = null) : base(id)

        {

            Name = name;

            LicenseNumber = licenseNumber;

            Speciality = speciality;


        }
    }
}
