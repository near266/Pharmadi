//namespace Jhipster.Helpers
//{
//    public static class ShortIdHelper
//    {
//        public static string Generate(string province, int number)
//        {
//            string firstC = firstC = ShortProvince(province);
//            //var stt = String.Format("{0:D5}", number);
//            return $"{firstC}{number}";
//        }

//        public static string ShortDate()
//        {
//            return DateTime.Now.ToString("yyMM");
//        }
//        public static string ShortProvince(string province)
//        {
//            string firstC = "";
//            if (province == "Đồng Nai") firstC = "DNI";
//            else if (province == "Đắk Nông") firstC = "DNO";
//            else if (province == "Bình Dương") firstC = "BDG";
//            else if (province == "Bến Tre") firstC = "BTR";
//            else if (province == "Hậu Giang") firstC = "HAG";
//            else if (province == "Hà Nam") firstC = "HNA";
//            else if (province == "Lai Châu") firstC = "LCH";
//            else if (province == "Quảng Nam") firstC = "QNA";
//            else if (province == "Quảng Ngãi") firstC = "QNG";
//            else if (province == "Thái Nguyên") firstC = "TNG";
//            else if (province == "Thừa Thiên Huế") firstC = "HUE";
//            else
//            {
//                string[] words = province.Split(' ');
//                foreach (string word in words)
//                    firstC += word[0];
//            }
//            firstC = firstC.Replace('Đ', 'D');
//            return firstC;
//        }
//        public class OrderConstant
//        {
//            public const string order = "ĐFH";
//            public const string sale = "BFH";
//            public const string returns = "FBTL";
//        }
//        public class ProviceConstant
//        {
//            public const string city = "Thành phố ";
//            public const string province = "Tỉnh ";
///*            public const string DN = "Đà Nẵng";
//            public const string BT = "Bình Thuận";
//            public const string province = "Tỉnh ";*/
//        }

//        public static string GenerateCode(string typeo, int yearnow, int number)
//        {
//            var stt = String.Format("{0:D5}", number);
//            return $"{typeo}{yearnow}_{stt}";
//        }
//    }
//}
