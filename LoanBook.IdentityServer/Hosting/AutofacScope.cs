using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;
using Autofac;

namespace LoanBook.IdentityServer.Hosting
{
    public class AutofacScope : IDependencyScope
    {
        private readonly ILifetimeScope _lifetimeScope;

        public AutofacScope(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public object GetService(Type serviceType)
        {
            return _lifetimeScope.ResolveOptional(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (!_lifetimeScope.IsRegistered(serviceType))
            {
                return Enumerable.Empty<object>();
            }

            Type type = typeof(IEnumerable<>).MakeGenericType(new Type[] { serviceType });
            return (IEnumerable<object>)_lifetimeScope.Resolve(type);
        }

        public void Dispose()
        {
        }
    }
}
