using System.Xml;

namespace MoneyExchangerAPI.Scripts
{
    public class ExchangeRateGetter
    {
        private string exchangeRateAPI;

        public ExchangeRateGetter(string exchangeRateAPI)
        {
            this.exchangeRateAPI = exchangeRateAPI;
        }

        public Dictionary<string, string> GetExchangeRate(string currencyCode)
        {
            var exchangeRateInfo = new Dictionary<string, string>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(exchangeRateAPI); 

            XmlNodeList exrateNodes = xmlDoc.SelectNodes("/ExrateList/Exrate");

            foreach (XmlNode node in exrateNodes)
            {
                string code = node.Attributes["CurrencyCode"].Value;

                if (code == currencyCode)
                {
                    exchangeRateInfo["Buy"] = node.Attributes["Buy"].Value;
                    exchangeRateInfo["Transfer"] = node.Attributes["Transfer"].Value;
                    exchangeRateInfo["Sell"] = node.Attributes["Sell"].Value;
                    return exchangeRateInfo;
                }
            }

            return null;
        }
    }
}