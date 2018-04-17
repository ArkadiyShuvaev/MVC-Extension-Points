using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;


namespace Paladin.Infrastructure
{
	public class HttpValueProvider : IValueProvider
	{
		private readonly NameValueCollection _headers;
		private readonly string[] _headerKeys;

		public HttpValueProvider(NameValueCollection httpHeaders)
		{
			_headers = httpHeaders;
			_headerKeys = httpHeaders.AllKeys;
		}
		public bool ContainsPrefix(string prefix)
		{
			return _headerKeys.Any(k => string.Equals(prefix, k.Replace("-", ""), StringComparison.OrdinalIgnoreCase));
		}

		public ValueProviderResult GetValue(string key)
		{
			var header = _headerKeys.FirstOrDefault(k => string.Equals(key, k.Replace("-", ""), StringComparison.OrdinalIgnoreCase));

			if (!string.IsNullOrEmpty(_headers[header]))
			{
				return new ValueProviderResult(_headers[header], _headers[header], CultureInfo.CurrentCulture);
			}

			return null;
		}
	}

	public class HttpValueProviderFactory : ValueProviderFactory
	{
		public override IValueProvider GetValueProvider(ControllerContext controllerContext)
		{
			return new HttpValueProvider(controllerContext.HttpContext.Request.Headers);
		}
	}
}