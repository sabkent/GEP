using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class WorldPayService
    {
        private static readonly WorldPaySettings TestSettings = new WorldPaySettings
        {
            BrandSettings = new List<WorldPayBrandSettings>
            {
                new WorldPayBrandSettings
                {
                    BrandId = "OE",
                    BaseAddress = "https://trx3.wpstn.com/stlinkssl/stlink.dll",
                    MerchantId = "200539",                  
                    UserName = "okmsptetest",
                    UserPassword = "okmsptetest",
                    StoreId = "88800143",
                    StoreIdRecurring = "88800143",
                    CurrencyId = "826", // GBP
                    //CurrencyId = "978", // EUR
                    IsTest = true
                }
            }
        };

        private readonly WorldPaySettings _settings;

        public Func<string, Dictionary<string, string>, Dictionary<string, string>> Post;

        public WorldPayService(WorldPaySettings settings)
        {
            Post = HttpPost;
            _settings = settings;
        }

        public WorldPayOttFormInfo RequestOttForm(string brandCode, string resultCallBack)
        {
            var response = PostWithCredentials(brandCode, new Dictionary<string, string>
                {
                    {"VersionUserd", "6"},
                    {"TransactionType", "RD"},
                    {"RequestType", "G"},
                    {"Action", "A"},
                    {"OTTResultURL", resultCallBack},
                });

            if (response.GetOrDefault("MessageCode") != "7050")
            {
                throw new WorldPayException("WorldPay request OTT failed", response, "error");
            }

            return new WorldPayOttFormInfo
            {
                Url = response["OTTProcessURL"],
                Ott = response["OTT"]
            };
        }

        public WorldPayCardInfo GetCreditCardInfo(string brandCode, string ott)
        {
            var response = PostWithCredentials(brandCode, new Dictionary<string, string>
                {
                    {"VersionUserd", "6"},
                    {"TransactionType", "RD"},
                    {"RequestType", "Q"},
                    {"OTT", ott},                   
                });

            if (response.GetOrDefault("MessageCode") != "7102")
            {
                throw new WorldPayException("WorldPay request OTT failed", response, "error");
            }

            return new WorldPayCardInfo
            {
                CustomerId = response["CustomerId"],
                CardId = response["CardId"],
                OrderNumber = response["OrderNumber"],
                AcctNumber = response["AcctNumber"],
                AcctName = response["AcctName"],
                CreditCardType = response["CreditCardType"],
                ExpDate = response["ExpDate"]
            };
        }


        public WorldPayPaymentResult ChargeCreditCard(string brandCode, string customerId, string cardId, string cvn, decimal amount, string orderNumber, bool isRecurring, string userId = null)
        {
            var settings = _settings.BrandSettings.First(s => s.BrandId == brandCode);
            var parameters = new Dictionary<string, string>
            {
                {"TransactionType", "PT"},
                {"RequestType", "S"},
                {"StoreID", isRecurring ? settings.StoreIdRecurring : settings.StoreId },
                {"MOP", "CC"},
                {"TRXSource", isRecurring ? "8" : "4"},
                {"OrderNumber", orderNumber/*.Truncate(35)*/},
                {"MerchantReference", userId ?? "Online payment"},
                {"CustomerId", customerId},
                {"CardId", cardId},
                {"CurrencyId", settings.CurrencyId},
                {"Amount", amount.ToString("#.00", CultureInfo.InvariantCulture)}
            };

            if (!string.IsNullOrWhiteSpace(cvn))
            {
                parameters["CVN"] = StripEncodingChars(cvn)/*.Truncate(4)*/;
            }

            var response = PostWithCredentials(brandCode, parameters);

            if (response.GetOrDefault("MessageCode") != "2100")
            {
                throw new WorldPayException("WorldPay authorize failed ORDER: " + orderNumber, response, "error");
            }

            return new WorldPayPaymentResult { AuthCode = response.GetOrDefault("AuthCode") };
        }



        public WorldPayCardInfo SaveCreditCard(string brandCode, string creditCardNumber, string nameOnCard, int expiryYear, int expiryMonth)
        {
            var response = PostWithCredentials(brandCode, new Dictionary<string, string>
                {
                    {"TransactionType","RD"},
                    {"RequestType", "A"},
                    {"StoreID", "88800143"}, // for spain
                    {"AcctName", StripEncodingChars(nameOnCard)/*.Truncate(60)*/},
                    {"AcctNumber", StripEncodingChars(creditCardNumber).Replace(" ","")/*.Truncate(16)*/},
                    {"ExpDate", new DateTime(expiryYear, expiryMonth, 1).ToString("MMyyyy")},
                });

            if (response.GetOrDefault("MessageCode") != "7102")
            {
                throw new WorldPayException("WorldPay save card failed", response, "error");
            }

            return new WorldPayCardInfo
            {
                CustomerId = response["CustomerId"],
                CardId = response["CardId"],
                OrderNumber = response["OrderNumber"],
                AcctNumber = response["AcctNumber"],
                AcctName = response["AcctName"],
                CreditCardType = response["CreditCardType"],
                ExpDate = response["ExpDate"]
            };
        }


        public void ChargeAndRefundVerificationDeposit(string brandCode, string customerId, string cardId, string cvn, string orderNumber)
        {
            const decimal amount = 0.1m;

            var settings = _settings.BrandSettings.First(s => s.BrandId == brandCode);
            var parameters = new Dictionary<string, string>
            {
                {"TransactionType", "PT"},
                {"RequestType", "A"},
                {"StoreID", settings.StoreId },
                {"MOP", "CC"},
                {"TRXSource", "4"},
                {"OrderNumber", orderNumber/*.Truncate(35)*/},
                {"MerchantReference", "Online payment"},
                {"CustomerId", customerId},
                {"CardId", cardId},
                {"CurrencyId", settings.CurrencyId},
                {"Amount", amount.ToString("#.00", CultureInfo.InvariantCulture)},
                {"IsVerify", "1"},
                {"CVN", cvn}
            };


            var response = PostWithCredentials(brandCode, parameters);

            if (response.GetOrDefault("MessageCode") != "2100")
            {
                throw new WorldPayException("WorldPay charge auth deposit failed ORDER: " + orderNumber, response, "error");
            }

            /*
            var cancelParameters = new Dictionary<string, string>
            {
                {"TransactionType", "PT"},
                {"RequestType", "R"},
                {"StoreID", settings.StoreId },
                {"MOP", "CC"},
                {"TRXSource", "4"},
                {"OrderNumber", orderNumber.Truncate(35)},
                {"MerchantReference", "Online payment"},
                {"CustomerId", customerId},
                {"CardId", cardId},
                {"CurrencyId", settings.CurrencyId},
                {"Amount", amount.ToString("#.00", CultureInfo.InvariantCulture)},
                {"PTTID", response["PTTID"]}
            };

            var cancelResponse = PostWithCredentials(brandCode, cancelParameters);

            if (cancelResponse.GetOrDefault("MessageCode") != "2050")
            {
                throw new WorldPayException("WorldPay cancel auth deposit failed ORDER: " + orderNumber, response, "error");
            }*/
        }



        private Dictionary<string, string> PostWithCredentials(string brandCode, Dictionary<string, string> values)
        {
            var settings = _settings.BrandSettings.First(s => s.BrandId == brandCode);
            AddIfNotSet(values, "VersionUsed", "3");
            AddIfNotSet(values, "MerchantId", settings.MerchantId);
            AddIfNotSet(values, "UserName", settings.UserName);
            AddIfNotSet(values, "UserPassword", settings.UserPassword);
            AddIfNotSet(values, "IsTest", settings.IsTest ? "1" : "0");
            AddIfNotSet(values, "TimeOut", "60000");
            return Post(brandCode, values);
        }



        private string StripEncodingChars(string value)
        {
            return value.Replace("~", "").Replace("^", "");
        }

        public static string Serialize(Dictionary<string, string> values)
        {
            return "StringIn=" + string.Join("~", values.Select(kv => kv.Key + "^" + kv.Value));
        }

        public static Dictionary<string, string> DeSerialize(string message)
        {
            try
            {
                return message.Split(new[] { '~' }, StringSplitOptions.RemoveEmptyEntries).Select(pair => pair.Split('^')).ToDictionary(kv => kv[0], kv => kv[1]);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to DeSerialize WorldPay response: [" + message + "]", e);
            }
        }

        private Dictionary<string, string> HttpPost(string brandCode, Dictionary<string, string> values)
        {
            var settings = _settings.BrandSettings.First(s => s.BrandId == brandCode);
            var request = Serialize(values);
            using (var webClient = new WebClient())
            {
                var response = webClient.UploadString(settings.BaseAddress, "POST", request);
                return DeSerialize(response);
            }
        }

        private void AddIfNotSet(Dictionary<string, string> dict, string key, string val)
        {
            if (!dict.ContainsKey(key))
            {
                dict[key] = val;
            }
        }



    }

    public static class DictionaryExtensions
    {
        public static TValue GetOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
        {
            return dict.ContainsKey(key) ? dict[key] : default(TValue);
        }
    }

    public class WorldPayException : Exception
    {
        public WorldPayException(string message, Dictionary<string, string> worldPayResult, string errorCode)
            : base(message + " (" + worldPayResult.GetOrDefault("MessageCode") + " " + worldPayResult.GetOrDefault("Message") + ")")
        {
            ErrorCode = errorCode;
            ExternalErrorCode = worldPayResult.GetOrDefault("MessageCode");
            ExternalErrorMessage = worldPayResult.GetOrDefault("Message");
            ExternalCvnMessageCode = worldPayResult.GetOrDefault("CVNMessageCode");
        }


        public override string ToString()
        {
            return string.Format("WorldPayException {0} {1} {2} ", ErrorCode, ExternalErrorCode, ExternalErrorMessage);
        }


        public string ErrorCode { get; set; }
        public string ExternalErrorCode { get; set; }
        public string ExternalErrorMessage { get; set; }
        public string ExternalCvnMessageCode { get; set; }
    }

    public class WorldPaySettings //: ISettings
    {
        public List<WorldPayBrandSettings> BrandSettings { get; set; }
    }

    public class WorldPayBrandSettings
    {
        public string BrandId { get; set; }
        public string BaseAddress { get; set; }
        public string MerchantId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string StoreId { get; set; }
        public string StoreIdRecurring { get; set; }
        public string CurrencyId { get; set; }
        public bool IsTest { get; set; }
    }

    public class WorldPayCardInfo
    {
        public string CustomerId { get; set; }
        public string CardId { get; set; }
        public string OrderNumber { get; set; }
        public string AcctNumber { get; set; }
        public string AcctName { get; set; }
        public string CreditCardType { get; set; }
        public string ExpDate { get; set; }
    }

    public class WorldPayOttFormInfo
    {
        public string Ott { get; set; }
        public string Url { get; set; }
    }

    public class WorldPayPaymentResult
    {
        public string AuthCode { get; set; }
    }
}
