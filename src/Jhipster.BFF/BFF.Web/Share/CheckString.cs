using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BFF.Web.Share
{
    public class CheckString
    {
        public static bool CheckValidEmail(string str)
        {
            if (str.Count(c => c == '@') == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool CheckInvalidPhoneNumber(string str)
        {

            if (Regex.IsMatch(str, @"^[0-9]+$") && str.Length == 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool CheckValidPassword(string password)
        {

            //if (Regex.IsMatch(password, @"^(?=.*[!@#$%^&*()])(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}$"))
            if(password.Length()>5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
