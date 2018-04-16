using Paladin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Paladin.ViewModels
{
    public class EmploymentSummary : IValidatableObject
    {
        public EmploymentSummary()
        {
            PrimaryEmployer = new EmploymentVM();
            PreviousEmployer = new EmploymentVM();
        }
		public EmploymentVM PrimaryEmployer { get; set; }
	    public EmploymentVM PreviousEmployer { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			
			if (PrimaryEmployer.StartDate > DateTime.UtcNow.AddYears(-3))
			{
				if (PreviousEmployer.EmploymentType != "Unemployed")
				{
					if (string.IsNullOrWhiteSpace(PreviousEmployer.Employer))
					{
						yield return new ValidationResult("Previous employer is required.", new[] { nameof(EmploymentVM.Employer) });
					}

					if (string.IsNullOrWhiteSpace(PreviousEmployer.Position))
					{
						yield return new ValidationResult("Previous position is required.");
					}
				}
			}
		}
	}
}