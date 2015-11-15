using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace BusinessEntities
{
    public class PhoneValidator
    {
        static Regex countryRegex = new Regex("^[2-9]\\d{2}-\\d{3}-\\d{4}$");

        public static bool IsValidNumber(string phoneNumber)
        {
            return countryRegex.IsMatch(phoneNumber);
        }

        public static bool IsValidCoordinate(string coord)
        {
            float result;
            return float.TryParse(coord, out result);
        }
    }
}