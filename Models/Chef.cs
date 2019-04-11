using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChefsDishes.Models
{
    
    public class OldEnoughAttribute : ValidationAttribute 
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext) 
        {
            if((DateTime)value >= DateTime.Today.AddYears(-18)) 
            {
                return new ValidationResult("The chef must be at least 18 years old.");
            }
            else 
            {
                return ValidationResult.Success;
            }
        }
    }

    public class Chef
    {
        [Key]
        public int UserId {get;set;}

        [Required]
        [MinLength(2)]
        [Display(Name="First Name")]
        public string Fname {get;set;}

        [Required]
        [MinLength(2)]
        [Display(Name="Last Name")]
        public string Lname {get;set;}

        [Required]
        [OldEnough]
        [Display(Name="Date of Birth")]
        public DateTime Birthday {get;set;}

        public List<Dish> DishesMade {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}