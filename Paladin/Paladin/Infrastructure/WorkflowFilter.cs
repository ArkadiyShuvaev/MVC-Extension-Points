using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Paladin.Models;

namespace Paladin.Infrastructure
{
	public class WorkflowFilter : FilterAttribute, IActionFilter
	{
		private int _highestCompletedStage;
		public int MinRequiredStage { get; set; }
		public int CurrentStage { get; set; }

		public void OnActionExecuting(ActionExecutingContext filterContext)
		{
			var applicantId = filterContext.HttpContext.Session["Tracker"];

			if (applicantId != null)
			{
				Guid tracker;
				if (Guid.TryParse(applicantId.ToString(), out tracker))
				{
					var context = DependencyResolver.Current.GetService<PaladinDbContext>();
					_highestCompletedStage = context.Applicants.FirstOrDefault(a => a.ApplicantTracker == tracker).WorkFlowStage;
					if (MinRequiredStage > _highestCompletedStage)
					{
						switch (_highestCompletedStage)
						{
							case (int) WorkflowStatus.ApplicantInfo:
								filterContext.Result = GenerateRedirectUrl("ApplicantInfo", "Applicant");
								break;

							case (int) WorkflowStatus.AddressInfo:
								filterContext.Result = GenerateRedirectUrl("AddressInfo", "Address");
								break;

							case (int) WorkflowStatus.EmploymentInfo:
								filterContext.Result = GenerateRedirectUrl("EmploymentInfo", "Employment");
								break;
							case (int) WorkflowStatus.VehicleInfo:
								filterContext.Result = GenerateRedirectUrl("VehicleInfo", "Vehicle");
								break;
							case (int) WorkflowStatus.Products:
								filterContext.Result = GenerateRedirectUrl("ProductInfo", "Applicant");
								break;
						}
					}
				}

			}
			else
			{
				if (CurrentStage != (int) WorkflowStatus.ApplicantInfo)
				{
					var action = "ApplicantInfo";
					var controller = "Applicant";
					filterContext.Result = GenerateRedirectUrl(action, controller);
				}
			}



		}

		private static RedirectToRouteResult GenerateRedirectUrl(string action, string controller)
		{
			return new RedirectToRouteResult(new RouteValueDictionary(new
			{
				action = action,
				controller = controller
			}));
		}

		public void OnActionExecuted(ActionExecutedContext filterContext)
		{
			var applicantId = filterContext.HttpContext.Session["Tracker"];

			if (applicantId != null)
			{
				Guid tracker;
				if (Guid.TryParse(applicantId.ToString(), out tracker))
				{
					
					if (filterContext.HttpContext.Request.RequestType == "POST"
					    && CurrentStage >= _highestCompletedStage)
					{
						var context = DependencyResolver.Current.GetService<PaladinDbContext>();
						var applicant = context.Applicants.FirstOrDefault(a => a.ApplicantTracker == tracker);
						applicant.WorkFlowStage = CurrentStage;
						context.SaveChanges();
					}
				}

			}
		}
	}
}
