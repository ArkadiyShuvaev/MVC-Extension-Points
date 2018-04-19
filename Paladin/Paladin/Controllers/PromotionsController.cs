using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Paladin.Infrastructure;

namespace Paladin.Controllers
{
	public class PromotionsController : Controller
    {
	    [IsPaladinMobile]
		public ActionResult Promos()
		{
			return View();
		}

	    [IsPaladinMobile]
		public ActionResult GoPromo()
        {
            return View();
        }
    }
}
