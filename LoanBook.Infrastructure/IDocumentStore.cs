namespace LoanBook.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IDocumentStore
    {
        IEnumerable<T> Get<T>(Expression<Func<T, bool>> query);

        void Save<T>(T projection);
    }
}
