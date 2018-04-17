using AutoMapper;
using Paladin.Models;
using Paladin.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Paladin.Infrastructure;

namespace Paladin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
	        var activeTheme = ConfigurationManager.AppSettings["ActiveTheme"];
	        if (!string.IsNullOrEmpty(activeTheme))
	        {
		        ViewEngines.Engines.Insert(0, new ThemeViewEngine(activeTheme));
	        }

			ValueProviderFactories.Factories.Insert(0, new HttpValueProviderFactory());
			ModelBinderProviders.BinderProviders.Insert(0, new XMLModelBinderProvider());

	        AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Mapper.CreateMap<ApplicantVM, Applicant>();
            Mapper.CreateMap<VehicleVM, Vehicle>();
            Mapper.CreateMap<AddressVM, Address>();
            Mapper.CreateMap<EmploymentVM, Employment>();
            Mapper.CreateMap<ProductsVM, Products>();

            Mapper.CreateMap<Applicant, ApplicantVM>();
            Mapper.CreateMap<Vehicle, VehicleVM>();
            Mapper.CreateMap<Address, AddressVM>();
            Mapper.CreateMap<Employment, EmploymentVM>();
            Mapper.CreateMap<Products, ProductsVM>();
        }

	    void Application_Error()
	    {
			Debug.WriteLine("test");
	    }
    }
}
