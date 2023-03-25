using System;

namespace Jhipster.Helpers
{
    public static class ShortIdHelper
    {

        public static string ShortDate()
        {
            return DateTime.Now.ToString("yyMM");
        }
        public class ObjectConstant
        {
            public const string order = "ĐFH";
            public const string cus = "CUS";
            public const string product = "PROD";
        }

        public static string GenerateCode(string typeo, int yearnow, int number)
        {
            var stt = String.Format("{0:D5}", number);
            return $"{typeo}{yearnow}_{stt}";
        }
    }
}
