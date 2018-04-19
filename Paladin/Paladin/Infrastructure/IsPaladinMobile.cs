using System.Reflection;
using System.Web.Mvc;

namespace Paladin.Infrastructure
{
	// Let's imagine that we have mobile application that send us the x-PaladinMobile:true header
	public class IsPaladinMobile : ActionMethodSelectorAttribute
	{
		public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
		{
			return controllerContext.HttpContext.Request.Headers["x-PaladinMobile"] != null;
		}
	}
}
