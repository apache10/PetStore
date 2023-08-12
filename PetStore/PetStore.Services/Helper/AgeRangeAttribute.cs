using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.Services.Helper
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AgeRangeAttribute : ValidationAttribute
    {
        private readonly int _maximumAge;

        public AgeRangeAttribute(int maximumAge)
        {
            _maximumAge = maximumAge;
        }

        public override bool IsValid(object value)
        {
            if (value is DateTime dateOfBirth)
            {
                var today = DateTime.Today;
                var age = today.Year - dateOfBirth.Year;

                if (dateOfBirth > today.AddYears(-age))
                {
                    age--;
                }
                if(age < 0)
                {
                    return false;
                }

                return age <= _maximumAge;
            }

            return false;
        }
    }
}
