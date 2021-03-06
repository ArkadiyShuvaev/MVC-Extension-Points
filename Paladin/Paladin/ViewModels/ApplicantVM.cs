﻿using Paladin.Models;
using System;
using System.ComponentModel.DataAnnotations;
using Paladin.Infrastructure;

namespace Paladin.ViewModels
{
    public class ApplicantVM : LogData
    {
	    public string UserAgent { get; set; }
	    [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
		[BirthdateValidator]
        [Display(Name = "Date of Birth")]
        public DateTime? Dob { get; set; }
        [Required]
        public double? Phone { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }
        [Required]
        [Display(Name = "Highest Education")]
        public string HighestEducation { get; set; }
        [Required]
        [Display(Name = "License Status")]
        public string LicenseStatus { get; set; }
        [Required]
        [Display(Name = "Years Licensed")]
        public string YearsLicensed { get; set; }
    }
}