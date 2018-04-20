using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paladin.Infrastructure;

namespace Paladin.Controllers
{
	public abstract class PaladinController : Controller
	{
		private Guid _tracker;

		protected Guid Tracker => _tracker;

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);

			if (Session["Tracker"] != null)
			{
				Guid.TryParse(Convert.ToString(Session["Tracker"]), out _tracker);
			}
		}

		protected ActionResult Xml(object model)
		{
			return new XmlResult(model);
		}

		protected ActionResult Csv(IEnumerable data, string fileName)
		{
			return new CsvResult(data, fileName);
		}

	}
}