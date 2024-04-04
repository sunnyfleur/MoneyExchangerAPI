using Microsoft.AspNetCore.Mvc;
using MoneyExchangerAPI.Scripts;

namespace MoneyExchangerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoneyExchangerAPIController : ControllerBase
    {
        private string exchangeRateAPI = "https://portal.vietcombank.com.vn/Usercontrols/TVPortal.TyGia/pXML.aspx";

        [HttpGet]
        public ActionResult<double> Get(string currencyID, double amount)
        {
            ExchangeRateGetter exchangeRateGetter = new ExchangeRateGetter(exchangeRateAPI);
            var exchangeRateInfo = exchangeRateGetter.GetExchangeRate(currencyID); 

            if (exchangeRateInfo != null)
            {
                double rate = Convert.ToDouble(exchangeRateInfo["Transfer"].Replace(",", ""));
                double convertedAmount = amount * rate;
                return convertedAmount;
            }
            else
            {
                return NotFound($"Không tìm thấy tỷ giá cho mã " + currencyID);
            }
        }
    }
}