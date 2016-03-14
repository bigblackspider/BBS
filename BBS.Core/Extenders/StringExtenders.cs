using System;
using System.Text.RegularExpressions;

namespace BBS.Core.Extenders
{
    public static class StringExtenders
    {
        private const string MatchEmailPattern =
            @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
            + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
            + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
            + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$";

        private const string MatchPhoneNoPattern =
            @"^\({0,1}((0|\+61)(2|4|3|7|8)){0,1}\){0,1}(\ |-){0,1}[0-9]{2}(\ |-){0,1}[0-9]{2}(\ |-){0,1}[0-9]{1}(\ |-){0,1}[0-9]{3}$";


        public static bool IsValidPhoneNo(this string value)
        {
            try
            {
                return Regex.IsMatch(value, MatchPhoneNoPattern);
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidEmail(this string value)
        {
            try
            {
                return Regex.IsMatch(value, MatchEmailPattern);
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidUrl(this string value)
        {
            try
            {
                Uri uri = null;
                return Uri.TryCreate(value, UriKind.Absolute, out uri)
                       && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
            }
            catch
            {
                return false;
            }
        }

        public static string Fmt(this string text, params object[] args)
        {
            return string.Format(text, args);
        }
    }
}