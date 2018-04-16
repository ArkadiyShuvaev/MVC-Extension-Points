using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Paladin.Infrastructure
{
	public class BirthdateValidator : ValidationAttribute //, IValidatableObject
	{
		public BirthdateValidator()
		{
			ErrorMessage = "Please enter a valid birth date. You should be 18 or older to apply.";
		}
		public override bool IsValid(object value)
		{
			DateTime enteredDate;
			if (DateTime.TryParse(Convert.ToString(value), out enteredDate))
			{
				if (enteredDate.AddYears(18) < DateTime.UtcNow)
				{
					return true;
				}

				return false;
			}

			return false;
		}
	}
}