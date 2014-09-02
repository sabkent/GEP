using System.Threading.Tasks;
using LoanBook.IdentityServer.Models;

namespace LoanBook.IdentityServer.Services
{
    public interface IClientStore
    {
        Task<Client> FindClientByIdAsync(string clientId);
    }

    public class InMemClientStore : IClientStore
    {
        public Task<Client> FindClientByIdAsync(string clientId)
        {
            return Task.FromResult(new Client());
        }
    }

}
