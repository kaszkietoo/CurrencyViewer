using CurrencyViewer.Web.Services.Interfaces;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CurrencyViewer.Web.Controllers
{
    public class HomeController : Controller
    {
        private ICurrenciesService _currenciesService;
        private IExchangeRatesService _exchangeRatesSerice;

        public HomeController(ICurrenciesService currenciesService, IExchangeRatesService exchangeRatesSerice)
        {
            _currenciesService = currenciesService;
            _exchangeRatesSerice = exchangeRatesSerice;
        }

        public async Task<ActionResult> Index()
        {            
            return View(await _currenciesService.Get());
        }
        
        //consider creating web api with api key in order to prevent undesired calls
        public async Task<ActionResult> Rates(string code)
        {
            var rates = await _exchangeRatesSerice.Get(code);
            return Json(rates, JsonRequestBehavior.AllowGet);
        }
    }
}