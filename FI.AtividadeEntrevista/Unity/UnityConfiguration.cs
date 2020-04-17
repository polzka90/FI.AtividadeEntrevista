

using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.Interfaces.BLL;
using System.Web.Http;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace FI.AtividadeEntrevista.Unity
{
    public static class UnityConfiguration
    {

        public static void ConfigureIoCContainer()
        {
            IUnityContainer container = new UnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            //GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);


            RegisterTypes(container);
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            // BLL
            container.RegisterType<IBoCliente, BoCliente>();
        }
    }
}
