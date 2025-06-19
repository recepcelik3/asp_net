using System;
using System.ComponentModel.DataAnnotations;

namespace InsuranceQuoteCalculator.Models
{
    public class Insuree
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Car Year")]
        public int CarYear { get; set; }

        [Required]
        [Display(Name = "Car Make")]
        public string CarMake { get; set; }

        [Required]
        [Display(Name = "Car Model")]
        public string CarModel { get; set; }

        [Required]
        [Display(Name = "Speeding Tickets")]
        public int SpeedingTickets { get; set; }

        [Required]
        [Display(Name = "DUI")]
        public bool DUI { get; set; }

        [Required]
        [Display(Name = "Full Coverage")]
        public bool FullCoverage { get; set; }

        [Display(Name = "Quote")]
        [DataType(DataType.Currency)]
        public decimal Quote { get; set; }
    }
}