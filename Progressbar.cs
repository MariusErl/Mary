using System;

namespace Mary
{
    class Progressbar
    {
        public static string Progress(double value, double maximum, int bars)
        {
            return Progress(value, maximum, bars, false);
        }
        public static string Progress(double value, double maximum, int bars, bool useWarningColors)
        {
            var progress = value / maximum;
            if (progress > 1)
            {
                progress = 1;
            }
            if (progress < 0)
            {
                progress = 0;
            }
            var barsFilled = (int)Math.Round(progress * bars);
            var color = "^2";
            if (useWarningColors)
            {
                if (progress < 0.2)
                {
                    color = "^1";
                }
                else if (progress < 0.4)
                {
                    color = "^3";
                }
            }
            var barAsString = color;
            for (var i = 0; i < barsFilled; i++)
            {
                barAsString += "l";
            }
            if (barsFilled < bars)
            {
                barAsString += "^8";
                for (var i = barsFilled; i < bars; i++)
                {
                    barAsString += "l";//l
                }
                var length = barAsString.Length;
            }
            return barAsString;
        }
    }
}