using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Core.Utilities
{
    public static class Formatting
    {
        public static string FormatTurkLirasi(decimal pPrice)
        {
            return pPrice.ToString("C", new System.Globalization.CultureInfo("tr-TR"));
        }

        public static string FormatIndirimsizFiyat(decimal pPrice,int pOran)
        {
            var fiyat = Convert.ToDouble(pPrice) * (100 + pOran) / 100;

            return FormatTurkLirasi(Convert.ToDecimal(fiyat));
        }

        public static string FormatMetniİkiyeBolme(string pName)
        {
            var metin = pName;
            int ortaIndex = metin.Length / 2;

            int boslukIndex = metin.IndexOf(" ", ortaIndex);

            if (boslukIndex == -1)
            {
                boslukIndex = metin.Length;
            }

            var ilkParca = metin.Substring(0, boslukIndex);

            var ikinciParca = metin.Substring(boslukIndex).TrimStart();

            return $" {ilkParca} <br /> {ikinciParca} ";
        }

        public static string FormatGizliKartNumarasi(string pName)
        {
            return pName.Substring(0, 4) + "****" + pName.Substring(12, 4);
        }

        public static string AfterPointDigits(decimal pValue, int pDigit)
        {
            return pValue.ToString("F" + pDigit.ToString());
        }

        public static string ConvertBytetoMegaByte(decimal pValue)
        {
            string type = " MB";
            decimal val = pValue / (1024 * 1024);
            if (val > 1024)
            {
                val = val / 1024;
                type = " GB";
            }

            return AfterPointDigits(val, 2) + type;
        }
    }
}
