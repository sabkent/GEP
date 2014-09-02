using LoanBook.IdentityServer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoanBook.IdentityServer.Services
{
    public interface IScopeStore
    {
        Task<IEnumerable<Scope>> GetScopesAsync();
    }
}