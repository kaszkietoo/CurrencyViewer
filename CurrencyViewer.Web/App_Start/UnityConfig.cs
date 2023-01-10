using CurrencyViewer.Web.Services.Implementations;
using CurrencyViewer.Web.Services.Interfaces;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace CurrencyViewer.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<ICurrenciesService, CurrenciesService>();
            container.RegisterType<IExchangeRatesService, ExchangeRatesService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}