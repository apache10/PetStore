using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PetStore.Services.Data;
using Microsoft.Extensions.DependencyInjection;

namespace PetStore.Services.Helper
{
    public class UniqueNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = validationContext.GetService<ApplicationDbContext>(); 
            var name = value.ToString();

            var exists = dbContext.Pets.Any(u => u.Name == name);

            if (exists)
            {
                return new ValidationResult("Name already in use.");
            }

            return ValidationResult.Success;
        }
    }
}
