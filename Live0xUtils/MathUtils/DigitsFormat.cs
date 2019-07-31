using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Live0xUtils.MathUtils
{
    public class DigitsFormat
    {

        /// <summary>
        /// 把小数转换为指定位数的字符串
        /// </summary>
        /// <param name="o"></param>
        /// <param name="digits">小数位数</param>
        /// <returns></returns>
        public static string  FormatDigitsToString(object o, int digits)
        {
            decimal d;
            if (o != null && decimal.TryParse(Convert.ToString(o), out d))
            {
                System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo();

                nfi.NumberDecimalDigits = digits;

                return d.ToString("N", nfi);
            }
            return Convert.ToString(o);
        }
    }
}
