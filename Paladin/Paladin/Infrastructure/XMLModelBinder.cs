using System;
using System.Web.Mvc;
using System.Xml.Serialization;


namespace Paladin.Infrastructure
{
	public class XMLModelBinder : IModelBinder
	{
		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			try
			{
				var serializer = new XmlSerializer(bindingContext.ModelType);
				var inputStream = controllerContext.HttpContext.Request.InputStream;

				return serializer.Deserialize(inputStream);
			}
			catch (Exception e)
			{
				bindingContext.ModelState.AddModelError("", "The item cannot be serialized");
				return null;
			}
		}
	}
}