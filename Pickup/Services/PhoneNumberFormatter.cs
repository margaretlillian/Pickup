using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pickup.Services
{
    public class PhoneNumberFormatter
    {
        public static string FormatPhoneNumber(string value)
        {
            if (String.IsNullOrEmpty(value))
                return null;
            value = new System.Text.RegularExpressions.Regex(@"\D")
                .Replace(value, string.Empty);
            value = value.TrimStart('1');
            if (value.Length == 7)
                return Convert.ToInt64(value).ToString("727-###-####");
            if (value.Length == 10)
                return Convert.ToInt64(value).ToString("###-###-####");
            if (value.Length > 10)
                return Convert.ToInt64(value).ToString("###-###-#### " + 'x' + new String('#', (value.Length - 10)));
            return value;
        }
        
    }
}
