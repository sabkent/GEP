using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LoanBook.Messaging;
using LoanBook.Origination.Messaging.Events;
using Newtonsoft.Json;

namespace LoanBook.Origination.Endpoint.EventHandlers
{
    public sealed class NotifyClientApplicationSubmissionAccepted : ISubscribeToEvent<ApplicationSubmissionAccepted>
    {
        public async void Notify(ApplicationSubmissionAccepted applicationSubmissionAccepted)
        {
            using (var httpClient = new HttpClient())
            {
                var url = String.Format("http://localhost:22177/api/application/{0}/status", applicationSubmissionAccepted.ApplicationId);

                var payLoad = JsonConvert.SerializeObject(applicationSubmissionAccepted);
                var response = await httpClient.PostAsync(url, new StringContent(payLoad, Encoding.UTF8, "application/json"));
            }
        }
    }
}
