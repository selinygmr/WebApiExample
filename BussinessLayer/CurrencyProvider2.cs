using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class CurrencyProvider2 : ICurrencyProvider
    {
        private string url;
        public CurrencyProvider2(string url)
        {
            this.url = url;
        }

        public decimal GetRate(string currencyCode)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;

            List<CurrencyProvider2Item> rates = JsonConvert.DeserializeObject<List<CurrencyProvider2Item>>(responseBody);

            return rates.FirstOrDefault(c => c.currency == currencyCode).rate_mid;
        }

    }

    public class CurrencyProvider2Item
    {
        public string currency;
        public decimal rate_mid;
    }
}
