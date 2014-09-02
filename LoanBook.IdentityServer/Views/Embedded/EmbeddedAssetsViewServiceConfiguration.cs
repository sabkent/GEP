using System.Collections.Generic;

namespace LoanBook.IdentityServer.Views.Embedded
{
    public class EmbeddedAssetsViewServiceConfiguration
    {
        public EmbeddedAssetsViewServiceConfiguration()
        {
            Stylesheets = new HashSet<string>();
            // adding default CSS here so hosting application can choose to remove it
            Stylesheets.Add("~/assets/styles.min.css");

            Scripts = new HashSet<string>();
        }

        public ICollection<string> Stylesheets { get; set; }
        public ICollection<string> Scripts { get; set; }
    }
}
