using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanBook.PaymentGateway.Core;
using PayPal;
using PayPal.Api.Payments;

namespace LoanBook.PaymentGateway.Infrastructure.PaymentGateways.Paypal
{
    /// <summary>
    /// https://developer.paypal.com/docs/classic/paypal-payments-pro/gs_PayPalPaymentsPro/  
    /// 
    /// api.sandbox.paypal.com
    /// </summary>
    public sealed class PaypalPaymentGateway : ITakePaymentProvider
    {
        public DirectPaymentResponse DirectPayment(DirectPaymentRequest directPaymentRequest)
        {
            var payment = PaymentFrom(directPaymentRequest);

            var result = payment.Create(GetAPIContext());

            return new DirectPaymentResponse
            {
                Reference = result.id
            };
        }
        
        public static APIContext GetAPIContext()
        {
            APIContext apiContext = new APIContext(GetAccessToken());
            apiContext.Config = GetConfig();
            return apiContext;
        }


        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential
                                (
                                   "ATrruxCCSuSW6nOP70wCw04Dv1jccW4Yhy6PhKUYjQX4WokcFfIyWEdFApyd",
                                    "EK_ybRDH_0k-QIl3q_lRqg6c85vF8cOOs1OUjr-WGzRmMr3VQZRdKhxotxEk",
                                    new Dictionary<string, string> { { "mode", "sandbox" } }
                                ).GetAccessToken();
            return accessToken;
        }

        private static Dictionary<string, string> GetConfig()
        {
            Dictionary<string, string> configMap = new Dictionary<string, string>();
            configMap.Add("mode", "sandbox");
            return configMap;
        }


        private Payment PaymentFrom(DirectPaymentRequest directPaymentRequest)
        {
            var details = new Details
            {
                shipping = "0",
                subtotal = directPaymentRequest.Amount.ToString(),
                tax = "0"
            };

            var amount = new Amount
            {
                currency = "GBP",
                total = directPaymentRequest.Amount.ToString(),
                details = details
            };

            return new Payment
            {
                intent = "sale",
                payer = new Payer
                {
                    payment_method = "credit_card",
                    funding_instruments = new List<FundingInstrument>
                    {
                        new FundingInstrument
                        {
                            credit_card = CreditCardFrom(directPaymentRequest)
                        }
                    }
                },
                transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        amount = amount,
                        description = "loan",
                        item_list = new ItemList
                        {
                            items = new List<Item>
                            {
                                new Item
                                {
                                    name = "installment one",
                                    currency = "GBP",
                                    price = directPaymentRequest.Amount.ToString(),
                                    quantity = "1",
                                    sku = "sku"
                                }
                            }
                        }
                    }
                }
            };
        }

        private PayPal.Api.Payments.Address AddressFrom(DirectPaymentRequest directPaymentRequest)
        {
            var billingAddress = directPaymentRequest.CardHolder.BillingAddress;
            return new PayPal.Api.Payments.Address
            {
                city = billingAddress.City,
                country_code = billingAddress.CountryIsoCode,
                line1 = billingAddress.LineOne,
                postal_code = billingAddress.PostalCode,
                state = billingAddress.RegionState
            };
        }

        private CreditCard CreditCardFrom(DirectPaymentRequest directPaymentRequest)
        {
            var card = directPaymentRequest.CardHolder.PaymentCard;
            return new CreditCard()
            {
                billing_address = AddressFrom(directPaymentRequest),
                cvv2 = card.Cvv2,
                expire_month = card.Expires.Month,
                expire_year = card.Expires.Year,
                number = card.Number,
                type = card.Type,
                first_name = directPaymentRequest.CardHolder.FirstName,
                last_name = directPaymentRequest.CardHolder.LastName
            };
        }
    }
}
