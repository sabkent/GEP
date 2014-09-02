using System;
using System.Collections.Generic;
using System.Web.Http.Services;
using Autofac;
using Microsoft.Owin;

namespace LoanBook.IdentityServer.Extensions
{
    public static class OwinExtensions
    {
        public static string GetBaseUrl(this IDictionary<string, object> env, string host = null)
        {
            var context = new OwinContext(env);
            var request = context.Request;

            if (string.IsNullOrWhiteSpace(host))
                host = "https://" + request.Host.Value;

            var baseUrl = new Uri(new Uri(host), context.Request.PathBase.Value).AbsoluteUri;
            if (baseUrl.EndsWith("/") == false) baseUrl += "/";

            return baseUrl;
        }


        public static string GetIdentityServerBaseUrl(this IDictionary<string, object> env)
        {
            return env[Constants.OwinEnvironment.IdentityServerBaseUrl] as string;
        }

        public static void SetIdentityServerBaseUrl(this IDictionary<string, object> env, string value)
        {
            env[Constants.OwinEnvironment.IdentityServerBaseUrl] = value;
        }

        public static ILifetimeScope GetLifetimeScope(this IDictionary<string, object> env)
        {
            return new OwinContext(env).Get<ILifetimeScope>(Constants.OwinEnvironment.AutofacScope);
        }

        public static void SetLifetimeScope(this IDictionary<string, object> env, ILifetimeScope lifetimeScope)
        {
            new OwinContext(env).Set(Constants.OwinEnvironment.AutofacScope, lifetimeScope);
        }
    }
}