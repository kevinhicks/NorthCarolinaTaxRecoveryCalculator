using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using NorthCarolinaTaxRecoveryCalculator.Misc;
using NorthCarolinaTaxRecoveryCalculator.Security;
using NorthCarolinaTaxRecoveryCalculator.Models.Service;
using NorthCarolinaTaxRecoveryCalculator.Models;

namespace NorthCarolinaTaxRecoveryCalculator
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();   
            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IEmailSender, SendGridEmailSender>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IProjectRepository, ProjectRepository>();
            container.RegisterType<IPaymentVoucherRepository, PaymentVoucherRepository>();
        }
    }
}