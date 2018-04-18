using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Paladin.Infrastructure
{
	public class HttpAuthenticate : FilterAttribute, IAuthenticationFilter
	{
		private readonly string _userName;
		private readonly string _userPassword;

		public HttpAuthenticate(string userName, string userPassword)
		{
			_userName = userName;
			_userPassword = userPassword;
		}
		public void OnAuthentication(AuthenticationContext filterContext)
		{
			var header = filterContext.HttpContext.Request.Headers["Authorization"];
			if (!string.IsNullOrEmpty(header))
			{
				var converted = Convert.FromBase64String(header.Replace("Basic", ""));
				var fromBase64Value = ASCIIEncoding.ASCII.GetString(converted);
				var arr = fromBase64Value.Split(':');
				var username = arr[0];
				var password = arr[1];

				if (username != _userName || password != _userPassword)
				{
					filterContext.Result = new HttpUnauthorizedResult("Wrong username or password");
				}
			}
			else
			{
				filterContext.Result = new HttpUnauthorizedResult("Please set up the Authorization header");
			}
		}

		public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
		{
			//filterContext.Result = new RedirectResult("http://www.stackoverflow.com");
		}
	}
}