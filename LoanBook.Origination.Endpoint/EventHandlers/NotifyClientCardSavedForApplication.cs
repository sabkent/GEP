using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LoanBook.Messaging;
using LoanBook.PaymentGateway.Messaging.Events;
using Newtonsoft.Json;

namespace LoanBook.Origination.Endpoint.EventHandlers
{
    public sealed class NotifyClientCardSavedForApplication : ISubscribeToEvent<CardSavedForApplication>
    {
        public async void Notify(CardSavedForApplication cardSavedForApplication)
        {
            using (var httpClient = new HttpClient())
            {
                var url = String.Format("http://localhost:22177/api/application/{0}/card", cardSavedForApplication.ApplicationId);

                var payLoad = JsonConvert.SerializeObject(cardSavedForApplication);
                var response = await httpClient.PostAsync(url, new StringContent(payLoad, Encoding.UTF8, "application/json"));
            }
        }
    }
}
