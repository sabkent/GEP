using LoanBook.IdentityServer.Views.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LoanBook.IdentityServer.Services
{
    public interface IViewService
    {
        Task<Stream> Login(IDictionary<string, object> env, LoginViewModel model);
        Task<Stream> Error(IDictionary<string, object> environment, ErrorViewModel errorViewModel);
    }
}