using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

//Author : Navjit Kaur

namespace Xamine.Models
{
    public class Min18Year : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var user = (UserModel)validationContext.ObjectInstance;
            if (user.DOB.HasValue)
            {
                var age = DateTime.Today.Year - user.DOB.Value.Year;
                return (age >= 18) ? ValidationResult.Success : new ValidationResult("Must be the age of atleast 18");
            }
            return new ValidationResult("Date of birth is a required field");
        }
    }
}