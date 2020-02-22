using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class CurrencyProvider1 : ICurrencyProvider
    {
        private string url;
        public CurrencyProvider1(string url)
        {
            this.url = url;
        }

        public decimal GetRate(string currencyCode)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;

            List<CurrencyProvider1Item> rates = JsonConvert.DeserializeObject<List<CurrencyProvider1Item>>(responseBody);

            return rates.FirstOrDefault(c => c.code == currencyCode).rate;
        }

    }

    public class CurrencyProvider1Item
    {
        public string code;
        public decimal rate;
    }
}
