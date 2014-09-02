using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using LoanBook.IdentityServer.Extensions;
using Microsoft.Owin;

namespace LoanBook.IdentityServer.Hosting
{
    public class AutofacContainerMiddleware
    {
        private readonly Func<IDictionary<string, object>, Task> _next;
        private readonly IContainer _container;

        public AutofacContainerMiddleware(Func<IDictionary<string, object>, Task> next, IContainer container)
        {
            _next = next;
            _container = container;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            var context = new OwinContext(env);

            using (var scope = _container.BeginLifetimeScope(b => b.RegisterInstance(context).As<IOwinContext>()))
            {
                env.SetLifetimeScope(scope);
                await _next(env);
            }
        }
    }
}