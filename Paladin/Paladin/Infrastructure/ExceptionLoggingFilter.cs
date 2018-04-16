using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paladin.Models;

namespace Paladin.Infrastructure
{
	public class ExceptionLoggingFilter : FilterAttribute, IExceptionFilter
	{
		public void OnException(ExceptionContext filterContext)
		{
			if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
			{
				filterContext.Result = new JsonResult
				{
					JsonRequestBehavior = JsonRequestBehavior.AllowGet,
					Data = new
					{
						Message = "An error has occured. Please try again later."
					}
				};
			}

			filterContext.HttpContext.Response.StatusCode = 500;
			filterContext.ExceptionHandled = true;

			var context = DependencyResolver.Current.GetService<PaladinDbContext>();
			var error = new ErrorLog
			{
				Message = filterContext.Exception.Message,
				StackTrace = filterContext.Exception.StackTrace,
				ControllerName = filterContext.Controller.GetType().Name,
				TargetedResult = filterContext.Result.GetType().Name,
				//SessionId = Convert.ToString(filterContext.HttpContext.Request["LoanId"]),
				SessionId = Convert.ToString(filterContext.HttpContext.Session.SessionID),
				UserAgent = filterContext.HttpContext.Request.UserAgent,
				Timestamp = DateTime.UtcNow
			};

			context.Errors.Add(error);
			context.SaveChanges();
		}
	}
}