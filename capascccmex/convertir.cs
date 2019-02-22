using System;
using System.Globalization;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace capascccmex
{
    public class convertir
    {

        public static bool log(string valor)
        {
            string pathFile = HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data") + "\\debug.txt";
            using (StreamWriter writer = new StreamWriter(pathFile, true))
                {
                    writer.WriteLine(valor.ToString());       
                }
            return true;
            
        }
        public static Guid? toNGuid(object valor)
        {
            Guid result;
            if (Guid.TryParse(valor.ToString(), out result))
            {
                return result;
            }
            return null;

        }

        public static Boolean toBoolean(object valor)
        {
            Boolean result;
            if (Boolean.TryParse(valor.ToString(), out result))
            {
                return result;
            }
            return false;

        }
        public static Boolean? toNBoolean(object valor)
        {
            Boolean result;
            if (Boolean.TryParse(valor.ToString(), out result))
            {
                return result;
            }
            return null;
        }

        public static Byte? toNByte(object valor)
        {
            Byte result;
            if (Byte.TryParse(valor.ToString(), out result))
            {
                return result;
            }
            return null;
        }

        public static string ToUp(string texto)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase((texto).ToLower());
        }

        public static UInt16 toUInt16(object valor)
        {
            UInt16 result;
            if (UInt16.TryParse(valor.ToString(), out result))
            {
                return result;
            }
            return 0;
        }

        public static Int16 toInt16(object valor)
        {
            Int16 result;
            if (Int16.TryParse(valor.ToString(), out result))
            {
                return result;
            }
            return 0;
        }
        public static Int16? toNInt16(object valor)
        {
            Int16 result;
            if (Int16.TryParse(valor.ToString(), out result))
            {
                return result;
            }
            return null;
        }
        public static Int32 toInt32(object valor)
        {
            Int32 result;
            if (Int32.TryParse(valor.ToString(), out result))
            {
                return result;
            }
            return 0;
        }

        public static UInt32 toUInt32(object valor)
        {
            UInt32 result;
            if (UInt32.TryParse(valor.ToString(), out result))
            {
                return result;
            }
            return 0;
        }
        public static Int32? toNInt32(object valor)
        {
            Int32 result;
            if (Int32.TryParse(valor.ToString(), out result))
            {
                return result;
            }
            return null;
        }

        public static Int64? toNInt64(object valor)
        {

            Int64 result;
            if (Int64.TryParse(valor.ToString(), out result))
            {
                return result;
            }
            return null;
        }

        public static double? toNDouble(object valor)
        {
            Double result;
            if (Double.TryParse(valor.ToString(), out result))
            {
                return result;
            }
            return null;
        }

        public static Single? toNSingle(object valor)
        {
            Single result;
            if (Single.TryParse(valor.ToString(), out result))
            {
                return result;
            }
            return null;
        }

        public static DateTime? toNDateTime(object valor)
        {
            IFormatProvider format = new CultureInfo("es-MX", false);
            DateTime result = DateTime.Today;
            if (DateTime.TryParse(valor.ToString(), format, DateTimeStyles.None, out result))
            {
                return result;
            }
            return null;

        }

        public static DateTimeOffset? toNDateTimeOffset(object valor)
        {
            IFormatProvider format = new CultureInfo("es-MX", false);
            DateTimeOffset result = DateTime.Today;
            if (DateTimeOffset.TryParse(valor.ToString(), format, DateTimeStyles.None, out result))
            {
                return result;
            }
            return null;

        }
        public static Decimal? toNDecimal(object valor)
        {
            Decimal result;

            if (Decimal.TryParse(valor.ToString(), out result))
            {
                return result;
            }
            return null;

        }
    }
}
