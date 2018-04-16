using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using Paladin.Models;
using WebGrease.Css.Ast.Selectors;

namespace Paladin.Controllers
{
	public class EMarketingController : Controller
	{
		private readonly PaladinDbContext _context;

		public EMarketingController(PaladinDbContext context)
		{
			_context = context;
		}

		public ActionResult WeeklyReport(EWeeklyReport weeklyReport)
		{
			if (ModelState.IsValid)
			{
				_context.WeeklyReports.Add(weeklyReport);
				_context.SaveChanges();
				return Content("OK");
			}

			return Content("Error");
		}

		public ActionResult MonthlyReport(EMonthlyReport monthlyReport)
		{
			if (ModelState.IsValid)
			{
				_context.MonthlyReport.Add(monthlyReport);
				_context.SaveChanges();
				return Content("OK");
			}

			return Content("Error");
		}



	}
}