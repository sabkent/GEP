namespace LoanBook.Origination.Core
{
    using System;

    public interface IApplicationRepository
    {
        Application GetById(Guid id);

        void Save(Application application);
    }
}
