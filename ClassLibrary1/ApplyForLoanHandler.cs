using System;

namespace ClassLibrary1
{
    class ApplyForLoanHandler
    {
        private ApplicantRepository _applicantRepository;
        private LoanAgreementRepository _loanAgreementRepository;
        
        public ApplyForLoanHandler(ApplicantRepository applicantRepository)
        {
            _applicantRepository = applicantRepository;
        }

        public void Handle(ApplyForLoan applyForLoan)
        {
            var applicant = applyForLoan.ToApplicant();
            _applicantRepository.Add(applicant);

            var loanAgreement = new LoanAgreement();
            _loanAgreementRepository.Add(loanAgreement);
        }
    }

    static class ApplyForLoanExtensions
    {
        public static Applicant ToApplicant(this ApplyForLoan applyForLoan)
        {
            return new Applicant();
        }
    }
}
