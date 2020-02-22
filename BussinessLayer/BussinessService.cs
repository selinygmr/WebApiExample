using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.Web.Configuration;

namespace BussinessLayer
{
    public class BussinessService
    {
        private List<ICurrencyProvider> currencyProviders;

        public BussinessService()
        {
            currencyProviders = new List<ICurrencyProvider>();
        }

        public void AddCurrencyProvider(ICurrencyProvider currencyProvider)
        {
            currencyProviders.Add(currencyProvider);
        }

        public decimal GetMinRate(string currenyCode)
        {
            var cachedValue = GetFromRedis(currenyCode);

            if (!string.IsNullOrEmpty(cachedValue))
            {
                return Convert.ToDecimal(cachedValue);
            }

            List<decimal> allRates = GetAllRates(currenyCode);
            decimal minVal = allRates.Min();

            SetToRedis(currenyCode, minVal.ToString(), new TimeSpan(0, 10, 0));

            return minVal;
        }

        private List<decimal> GetAllRates(string currencyCode)
        {
            List<decimal> rateList = new List<decimal>();

            foreach (var provider in currencyProviders)
            {
                var rate = provider.GetRate(currencyCode);
                rateList.Add(rate);
            }

            return rateList;
        }

        private void SetToRedis(string key, string data, TimeSpan expireTime)
        {
            ConnectionMultiplexer redisCon = ConnectionMultiplexer.Connect("localhost");
            var db = redisCon.GetDatabase();
            db.StringSet(key, data, expireTime);
        }

        private string GetFromRedis(string key)
        {
            ConnectionMultiplexer redisCon = ConnectionMultiplexer.Connect("localhost");
            var db = redisCon.GetDatabase();
            return db.StringGet(key);
        }


        //private List<decimal> GetAllRates(string currencyCode)
        //{
        //    List<decimal> rateList = new List<decimal>();

        //    CurrencyProvider1 mock1 = new CurrencyProvider1("http://www.mocky.io/v2/5d19ec692f00002c00fd7324");
        //    var rate1 = mock1.GetRate(currencyCode);
        //    rateList.Add(rate1);

        //    CurrencyProvider1 mock2 = new CurrencyProvider1("http://www.mocky.io/v2/5d19ec932f00004e00fd7326");
        //    var rate2 = mock2.GetRate(currencyCode);
        //    rateList.Add(rate2);

        //    CurrencyProvider2 mock3 = new CurrencyProvider2("http://www.mocky.io/v2/5e383a4c310000e389d3808d");
        //    var rate3 = mock3.GetRate(currencyCode);
        //    rateList.Add(rate3);

        //    return rateList;
        //}
    }


}
