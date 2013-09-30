using System;
using System.Globalization;
using System.Linq;

namespace CustomFormatter
{
    class Program
    {
        static void Main(string[] args)
        {
            var formatProvider = new WeekdayFormatProvider();
            
            var formattedMinDate = string.Format(formatProvider, "{0}", DateTime.MinValue);
            Console.WriteLine(formattedMinDate);
            
            var formattedMinDateReversed = string.Format(formatProvider, "{0:R}", DateTime.MinValue);
            Console.WriteLine(formattedMinDateReversed);

            var formattedInteger = string.Format(formatProvider, "{0:R} {1}", DateTime.MinValue, int.MaxValue);
            Console.WriteLine(formattedInteger);
            
            Console.ReadLine();
        }
    }

    public class WeekdayFormatProvider : IFormatProvider
    {
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return new WeekdayFormatter();
            }
            return null;
        }
    }

    public class WeekdayFormatter : ICustomFormatter
    {
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            try
            {
                var dayName = ((DateTime)arg).DayOfWeek.ToString();
                if (format == "R")
                {
                    return new string(dayName.Reverse().ToArray());
                }
                return dayName;
            }
            catch (InvalidCastException)
            {
                if (arg is IFormattable)
                {
                    return ((IFormattable)arg).ToString(format, CultureInfo.CurrentCulture);
                }
                else if (arg != null)
                {
                    return arg.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
