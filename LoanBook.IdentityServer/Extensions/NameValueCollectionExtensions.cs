﻿using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace LoanBook.IdentityServer.Extensions
{
    public static class NameValueCollectionExtensions
    {
        public static string ToQueryString(this NameValueCollection nameValueCollection)
        {
            if (nameValueCollection.Count == 0)
            {
                return String.Empty;
            }

            var builder = new StringBuilder();
            bool first = true;
            foreach (string name in nameValueCollection)
            {
                string[] values = nameValueCollection.GetValues(name);
                if (values == null || values.Length == 0)
                {
                    first = AppendNameValuePair(builder, first, true, name, String.Empty);
                }
                else
                {
                    foreach (string value in values)
                    {
                        first = AppendNameValuePair(builder, first, true, name, value);
                    }
                }
            }

            return builder.ToString();
        }

        private static bool AppendNameValuePair(StringBuilder builder, bool first, bool urlEncode, string name, string value)
        {
            string effectiveName = name ?? String.Empty;
            string encodedName = urlEncode ? WebUtility.UrlEncode(effectiveName) : effectiveName;

            string effectiveValue = value ?? String.Empty;
            string encodedValue = urlEncode ? WebUtility.UrlEncode(effectiveValue) : effectiveValue;
            encodedValue = ConvertFormUrlEncodedSpacesToUrlEncodedSpaces(encodedValue);

            if (first)
            {
                first = false;
            }
            else
            {
                builder.Append("&");
            }

            builder.Append(encodedName);
            if (!String.IsNullOrEmpty(encodedValue))
            {
                builder.Append("=");
                builder.Append(encodedValue);
            }
            return first;
        }

        internal static string ConvertFormUrlEncodedSpacesToUrlEncodedSpaces(string str)
        {
            if ((str != null) && (str.IndexOf('+') >= 0))
            {
                str = str.Replace("+", "%20");
            }
            return str;
        }
    }
}
