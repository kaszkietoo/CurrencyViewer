using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurrencyViewer.Web.Models
{
    public class Rate
    {
        [JsonProperty("bid")]
        public decimal Bid { get; set; }
        [JsonProperty("ask")]
        public decimal Ask { get; set; }
        [JsonProperty("effectiveDate")]
        public DateTime EffectiveDate { get; set; }
    }
}