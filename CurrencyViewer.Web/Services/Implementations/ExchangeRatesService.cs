using CurrencyViewer.Web.Models;
using CurrencyViewer.Web.Services.Interfaces;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace CurrencyViewer.Web.Services.Implementations
{
    public class ExchangeRatesService : IExchangeRatesService
    {
        private readonly string _url = ConfigurationManager.AppSettings["ExchangeRatesUrl"];
        
        //consider caching rates
        public async Task<ExchangeRate> Get(string code)
        {            
            var url = _url.Replace("code", code);
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);            

            string json = await response.Content.ReadAsStringAsync();            
            return JsonConvert.DeserializeObject<ExchangeRate>(json);
        }
    }
}