using CurrencyViewer.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyViewer.Web.Services.Interfaces
{
    public interface ICurrenciesService
    {
        Task<IEnumerable<Currency>> Get();
    }
}
