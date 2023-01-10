using CurrencyViewer.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyViewer.Web.Services.Interfaces
{
    public interface IExchangeRatesService
    {
        Task<ExchangeRate> Get(string code);
    }
}
