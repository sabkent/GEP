namespace LoanBook.Financial.Endpoint.EventHandlers
{
    using System;

    using LoanBook.Financial.Core;
    using LoanBook.Origination.Messaging.Events;
    using NServiceBus;

    internal sealed class CreateAccountsForAcceptedApplication : IHandleMessages<ApplicationAccepted>
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly IAccountRepository accountRepository;

        public CreateAccountsForAcceptedApplication(IPaymentRepository paymentRepository, IAccountRepository accountRepository)
        {
            this.paymentRepository = paymentRepository;
            this.accountRepository = accountRepository;
        }

        public void Handle(ApplicationAccepted applicationAccepted)
        {
            var account = new Account();

            account.AddPrincipal(applicationAccepted.Amount);
            account.AddInterest(applicationAccepted.Interest);

            this.accountRepository.Add(account);

            var payment = new Payment() {Id = applicationAccepted.ApplicationId, Amount = account.GetBalance()};
            this.paymentRepository.Add(payment);

            Console.WriteLine("creating accounts for application");
        }
    }
}
