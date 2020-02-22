using BussinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiEx.Controllers
{
    [RoutePrefix("Currency")]
    public class ValuesController : ApiController
    {

        // GET api/values/5
        [Route("Get")]
        public string Get(string currencyCode)
        {
            try
            {
                BussinessService bussinessService = new BussinessService();
                bussinessService.AddCurrencyProvider(new CurrencyProvider1("http://www.mocky.io/v2/5d19ec692f00002c00fd7324"));
                bussinessService.AddCurrencyProvider(new CurrencyProvider1("http://www.mocky.io/v2/5d19ec932f00004e00fd7326"));
                bussinessService.AddCurrencyProvider(new CurrencyProvider2("http://www.mocky.io/v2/5e383a4c310000e389d3808d"));

                return bussinessService.GetMinRate(currencyCode).ToString();
            }
            catch(Exception ex)
            {
                return "Beklenmedik bir hata oluştu.";
            }
        }
    }
}
