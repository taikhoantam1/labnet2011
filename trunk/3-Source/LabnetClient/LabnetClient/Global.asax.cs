using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using DataRepository;
using DomainModel;
using System.Timers;
using System.Configuration;

namespace LabnetClient
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private Timer updateToServerTimer = new Timer();
        private int LabId = 0;
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "BenhNhan", action = "Create", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            LabId = int.Parse(ConfigurationManager.AppSettings["LabId"]);
            int timerInterval = int.Parse(ConfigurationManager.AppSettings["UpdateServerInterval"]);
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
            // Code that runs on application startup
            log4net.Config.XmlConfigurator.Configure();
            updateToServerTimer.Interval = timerInterval;
            updateToServerTimer.Elapsed += new ElapsedEventHandler(updateToServerTimer_Elapsed);
            updateToServerTimer.Start();
            Configure();
        }

        void updateToServerTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            IServerConnector connecter = new ServerConnector();
            connecter.UpdateToServer(LabId);
        }

        public static void Configure()
        {
            Mapper.CreateMap<Partner, VMPartner>();

            Mapper.CreateMap<VMPartner,Partner>();

            Mapper.CreateMap<Test, VMTest>()
                .ForMember("TestSectionName",p=>p.Ignore());

            Mapper.CreateMap<VMTest, Test>();

            Mapper.CreateMap<Panel, VMPanel>()
                .ForMember("PanelEditLink", p => p.Ignore());

            Mapper.CreateMap<VMPanel, Panel>();

            Mapper.CreateMap<Doctor, VMDoctor>();

            Mapper.CreateMap<VMDoctor, Doctor>();

            Mapper.CreateMap<Patient, VMPatient>()
            .ForMember("PatientEditLink", p => p.Ignore());

            Mapper.CreateMap<TestSection, VMTestSection>();
            Mapper.CreateMap<VMTestSection, TestSection>();

            Mapper.CreateMap<PatientsGets_Result, VMPatient>()
            .ForMember("PatientEditLink", p => p.Ignore())
            .ForMember("PatientNumber", p => p.Ignore())
            .ForMember("Gender", p => p.Ignore())
            .ForMember("Status", p => p.Ignore())
            .ForMember("IndentifierNumber", p => p.Ignore())
            .ForMember("Email", p => p.Ignore())
            .ForMember("BirthDate", p => p.Ignore())
            .ForMember(dest=>dest.LabExamination, p => p.MapFrom(m => new VMLabExamination{ReceivedDate= m.ReceivedDate, OrderNumber=m.OrderNumber}));

            Mapper.CreateMap<Instrument, VMInstrument>();
            Mapper.CreateMap<VMInstrument, Instrument>();
        }
    }
}