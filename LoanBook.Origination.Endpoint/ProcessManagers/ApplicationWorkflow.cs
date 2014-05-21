namespace LoanBook.Origination.Endpoint.ProcessManagers
{
    using System;

    using NServiceBus.Saga;

    public class ApplicationWorkflow : ContainSagaData
    {
        public Guid ApplicationId { get; set; }
    }
}
