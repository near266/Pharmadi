// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

namespace BFF.Web.Constants
{
    public static class Constant
    {
        // Regex for acceptable logins
        public const string LoginRegex = "^[_.@A-Za-z0-9-]*$";
        public const string SystemAccount = "system";
        public const string AnonymousUser = "anonymoususer";
        public const string DefaultLangKey = "en";
    }
}
