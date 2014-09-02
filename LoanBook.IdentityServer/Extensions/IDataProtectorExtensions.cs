using LoanBook.IdentityServer.Configuration;
using System.Text;

// ReSharper disable once CheckNamespace
namespace LoanBook.IdentityServer
{
    // ReSharper disable once InconsistentNaming
    public static class IDataProtectorExtensions
    {
        public static string Protect(this IDataProtector dataProtector, string data, string entropy = Constants.EmptyString)
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            var protectedBytes = dataProtector.Protect(bytes, entropy);
            return Base64Url.Encode(protectedBytes);
        }

        public static string Unprotect(this IDataProtector dataProtector, string data, string entropy = Constants.EmptyString)
        {
            var protectedBytes = Base64Url.Decode(data);
            var bytes = dataProtector.Unprotect(protectedBytes, entropy);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}