using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerlinClock
{
    public static class Extensions
    {
        public static int ValidateHour(this ITimeConverter timeConverter, string aTime)
        {
            if (string.IsNullOrEmpty(aTime))
                throw new ArgumentNullException(nameof(aTime));

            string[] time = aTime.Split(':');

            int hours;
            if (int.TryParse(time[0], out hours))
            {
                if (hours < 0 || hours > 24)
                    throw new ArgumentOutOfRangeException(nameof(hours), hours, "No valid hours range (0 - 24)");
            }
            else { throw new ArgumentException("No valid hours time value", nameof(hours)); }

            return hours;
        }

        public static int ValidateMinutes(this ITimeConverter timeConverter, string aTime)
        {
            string[] time = aTime.Split(':');

            int minutes;
            if (int.TryParse(time[1], out minutes))
            {
                if (minutes < 0 || minutes > 59)
                    throw new ArgumentOutOfRangeException(nameof(minutes), minutes, "No valid minutes range range (0 - 59)");
            }
            else { throw new ArgumentException("No valid minutes value", nameof(minutes)); }

            return minutes;
        }
        public static int ValidateSeconds(this ITimeConverter timeConverter, string aTime)
        {
            string[] time = aTime.Split(':');

            int seconds;
            if (int.TryParse(time[2], out seconds))
            {
                if (seconds < 0 || seconds > 59)
                    throw new ArgumentOutOfRangeException(nameof(seconds), seconds, "No valid seconds range (0 - 59)");
            }
            else { throw new ArgumentException("No valid seconds value", nameof(seconds)); }

            return seconds;
        }
    }
}
