namespace LoanBook.Origination.Endpoint.ProcessManagers
{
    using System;

    using LoanBook.CustomerRelationshipManagement.Messaging.Commands;
    using LoanBook.CustomerRelationshipManagement.Messaging.Events;
    using LoanBook.Fraud.Messaging.Commands;
    using LoanBook.Fraud.Messaging.Events;
    using LoanBook.Origination.Core;
    using LoanBook.Origination.Messaging.Commands;
    using LoanBook.Origination.Messaging.Events;

    using NServiceBus;
    using NServiceBus.Saga;

    public sealed class LoanApplicationProcessManager : Saga<ApplicationWorkflow>, 
        IAmStartedByMessages<SubmitApplication>,
        IHandleMessages<DuplicationCheckComplete>,
        IHandleMessages<FraudChecksHaveBeenRun>,
        IHandleMessages<SignAgreement>,
        IHandleTimeouts<NoSignatureReceived>
    {
        //--Sagas can not have non-default constructors.. use setter injection (https://groups.yahoo.com/neo/groups/nservicebus/conversations/topics/3119)
        public IApplicationRepository ApplicationRepository { get;  set; }

        public void Handle(SubmitApplication submitApplication)
        {
            Console.WriteLine("application submitted");
            var application = new Application { Id = submitApplication.ApplicationId, Amount = 100, FirstName = "first name", LastName = "last name"};
            this.ApplicationRepository.Save(application);

            //this saga expects fraud checks. so we command them to be run. if fraud was run in response to LoanSubmitted event the sematic of events is there could be no subscribers

            this.Bus.Send(new CheckDuplication { ApplicationId = submitApplication.ApplicationId });
        }

        public void Handle(DuplicationCheckComplete duplicationCheckComplete)
        {
            Console.WriteLine("duplicate check complete");

            this.Bus.Send(new RunFraudChecks { ApplicationId = duplicationCheckComplete.ApplicationId });
        }

        public void Handle(FraudChecksHaveBeenRun fraudChecksHaveBeenRun)
        {
            Console.WriteLine("fraud checks complete");
            RequestTimeout(TimeSpan.FromDays(2), new NoSignatureReceived { ApplicationId = fraudChecksHaveBeenRun.ApplicationId });
            this.Bus.Publish(new ApplicationSubmitted{ApplicationId = fraudChecksHaveBeenRun.ApplicationId});
        }

        public void Handle(SignAgreement signAgreement)
        {
            Console.WriteLine("sign loan agreement");
            this.Bus.Publish(new ApplicationAccepted{ApplicationId = signAgreement.ApplicationId });
        }

        public void Timeout(NoSignatureReceived noSignatureReceived)
        {
            Console.WriteLine("no signature received:{0}", noSignatureReceived.ApplicationId);
        }
        
        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<SubmitApplication>(submitApplication => submitApplication.ApplicationId)
                .ToSaga(applicationWorkflow => applicationWorkflow.ApplicationId);

            this.ConfigureMapping<FraudChecksHaveBeenRun>(fraudChecksHaveBeenRun => fraudChecksHaveBeenRun.ApplicationId)
                .ToSaga(applicationWorkflow => applicationWorkflow.ApplicationId);

            this.ConfigureMapping<DuplicationCheckComplete>(duplicationCheckComplete => duplicationCheckComplete.ApplicationId)
                .ToSaga(applicationWorkflow => applicationWorkflow.ApplicationId);

            this.ConfigureMapping<SignAgreement>(signAgreement => signAgreement.ApplicationId)
                .ToSaga(appllicationWorkflow => appllicationWorkflow.ApplicationId);
        }
    }
}
