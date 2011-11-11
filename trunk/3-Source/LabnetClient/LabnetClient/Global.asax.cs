﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using DataRepository;
using DomainModel;

namespace LabnetClient
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
            // Code that runs on application startup
            log4net.Config.XmlConfigurator.Configure();
            Configure();
        }

        public static void Configure()
        {
            Mapper.CreateMap<Partner, VMPartner>();

            Mapper.CreateMap<VMPartner,Partner>();

            Mapper.CreateMap<Test, VMTest>()
                .ForMember("TestSectionName",p=>p.Ignore());

            Mapper.CreateMap<VMTest, Test>();

            Mapper.CreateMap<Panel, VMPanel>();

            Mapper.CreateMap<VMPanel, Panel>();
        }
    }
}