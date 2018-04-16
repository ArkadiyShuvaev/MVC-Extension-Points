using System;
using System.ComponentModel.DataAnnotations;

namespace Paladin.ViewModels
{
    public class EmploymentVM
    {
        [Display(Name = "Employment Type")]
        public string EmploymentType { get; set; }
        public string Employer { get; set; }
        public string Position { get; set; }
        [Display(Name = "Gross Monthly Income")]
        public double GrossMonthlyIncome { get; set; }
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }
    }
}
