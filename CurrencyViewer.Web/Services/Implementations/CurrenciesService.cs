using CurrencyViewer.Web.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Linq;
using System;
using CurrencyViewer.Web.Services.Interfaces;

namespace CurrencyViewer.Web.Services.Implementations
{
    public class CurrenciesService : ICurrenciesService
    {
        private readonly string _url = ConfigurationManager.AppSettings["CurrenciesUrl"];
        private readonly int _currenciesRefreshIntervalInHours = int.Parse(ConfigurationManager.AppSettings["CurrenciesRefreshIntervalInHours"]);
        private readonly string _currenciesKey = "currencies";

        public async Task<IEnumerable<Currency>> Get()
        {            
            var cache = MemoryCache.Default;
            var currencies = cache.Get(_currenciesKey) as List<Currency>;

            if (currencies == null)
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(_url);
                string json = await response.Content.ReadAsStringAsync();
                var currenciesDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                currencies = new List<Currency>();
                currencies.AddRange(currenciesDictionary.Select(c => new Currency { Code = c.Key, Name = c.Value}));
                cache.Add(_currenciesKey, currencies, new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddHours(_currenciesRefreshIntervalInHours) });
            }

            return currencies;
        }
    }
}