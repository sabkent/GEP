namespace LoanBook.Correspondence.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using LoanBook.Correspondence.Core;
    using LoanBook.Infrastructure;

    public class EmailsController : ApiController
    {
        private readonly IDocumentStore documentStore;

        public EmailsController(IDocumentStore documentStore)
        {
            this.documentStore = documentStore;
        }

        public HttpResponseMessage Get()
        {
            var expression = this.GetExpressionFromQueryString(Request.RequestUri.Query);

            var emails = documentStore.Get(expression).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, emails);
        }

        private Expression<Func<EmailMessage, bool>> GetExpressionFromQueryString(string queryString)
        {
            queryString = queryString.Replace("?locator=", "");
            var queryValues = new Dictionary<string, string>();

            foreach (var part in queryString.Split(new[] { ',' }))
            {
                string[] queryParts = part.Split(new[] { ':' });
                queryValues.Add(queryParts[0], queryParts[1]);
            }

            var query = PredicateBuilder.True<EmailMessage>();

            if (queryValues.ContainsKey("to"))
            {
                query = query.And(e => e.To == queryValues["to"]);
            }

            return query;
        }
    }

    //http://www.albahari.com/nutshell/predicatebuilder.aspx
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }

    
}
