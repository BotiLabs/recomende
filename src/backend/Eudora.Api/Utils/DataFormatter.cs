using System;
using System.Threading;

namespace Eudora.Api.Utils
{
    public class DataFormatter
    {
        public static decimal AdjustDecimalPlaces(string value)
        {
            if (value != null)
            {
                var tempValue = value;

                if (tempValue.Length >= 3)
                {
                    var separator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                    tempValue = tempValue.Insert(tempValue.Length - 2, separator);
                }

                if (Decimal.TryParse(tempValue, out decimal decimalValue))
                {
                    return decimalValue;
                }
            }

            return 0;
        }
    }
}
