using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using AnushkaSales.Model.Repositorys;
using AnushkaSales.Web.Controllers;
using AnushkaSales.Web.Infrastructure;
using Microsoft.Owin.Security;
using System.Web;

namespace AnushkaSales.Web.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            container.RegisterType<IBranchRepository, BranchRepository>();
            container.RegisterType<ICustomerRepository, CustomerRepository>();

            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<IAuthenticationManager>(new InjectionFactory(o => System.Web.HttpContext.Current.GetOwinContext().Authentication));
            container.RegisterType<IUserDetailsProvider, UserDetailsProvider>();
        }
    }
}
