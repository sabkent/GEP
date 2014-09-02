using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanBook.IdentityServer.Services;
using LoanBook.IdentityServer.Views.Embedded.Assets;
using LoanBook.IdentityServer.Views.ViewModels;

namespace LoanBook.IdentityServer.Views.Embedded
{
    public class EmbeddedAssetsViewService : IViewService
    {
        private readonly EmbeddedAssetsViewServiceConfiguration _embeddedAssetsViewServiceConfiguration;

        public EmbeddedAssetsViewService(EmbeddedAssetsViewServiceConfiguration embeddedAssetsViewServiceConfiguration)
        {
            _embeddedAssetsViewServiceConfiguration = embeddedAssetsViewServiceConfiguration;
        }

        public Task<Stream> Login(IDictionary<string, object> env, LoginViewModel model)
        {
            return Render(model, "login");
        }

        public Task<System.IO.Stream> Error(IDictionary<string, object> environment, ViewModels.ErrorViewModel errorViewModel)
        {
            throw new NotImplementedException();
        }


        protected virtual Task<Stream> Render(CommonViewModel viewModel, string page)
        {
            string html = AssetManager.GetLayoutHtml(viewModel, page,
                _embeddedAssetsViewServiceConfiguration.Stylesheets, _embeddedAssetsViewServiceConfiguration.Scripts);
            return Task.FromResult(StringToStream(html));
        }

        protected Stream StringToStream(string s)
        {
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);
            sw.Write(s);
            sw.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }
    }
}
