using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;

namespace CurrencyViewer.Web.Models
{
    public class ExchangeRate
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("rates")]
        public Rate[] Rates { get; set; }
    }   
}