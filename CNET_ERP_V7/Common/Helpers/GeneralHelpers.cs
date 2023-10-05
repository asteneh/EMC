using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CNET_ERP_V7.Common.Helpers
{
    public static class GeneralHelpers
    {
        public static string FirstCharacterToLower(this string str)
        {
            if (String.IsNullOrEmpty(str) || Char.IsLower(str, 0))
            {
                return str;
            }

            return Char.ToLowerInvariant(str[0]) + str.Substring(1);
        }

    }
}
