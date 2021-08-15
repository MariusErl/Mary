using System;

namespace Mary
{
    class Progressbar
    {
        public static string Progress(double value, double maximum, int bars)
        {
            return Progress(value, maximum, bars, false, false);
        }
        public static string Progress(double value, double maximum, int bars, bool useWarningColors, bool reverseColors)
        {
            var progress = value / maximum;
            if (progress > 1) { progress = 1; }
            if (progress < 0) { progress = 0; }

            var barsFilled = (int)Math.Round(progress * bars);
            var color = "^2";
            if (useWarningColors)
            {
                if (!reverseColors)
                {
                    if (progress < 0.1)
                    {
                        color = "^1";
                    }
                    else if (progress < 0.3)
                    {
                        color = "^8";
                    }
                    else if (progress < 0.5)
                    {
                        color = "^3";
                    }
                }
                else
                {
                    if (progress < 0.5)
                    {
                        color = "^2";
                    }
                    else if (progress < 0.7)
                    {
                        color = "^8";
                    }
                    else if (progress < 0.9)
                    {
                        color = "^3";
                    }
                    else
                    {
                        color = "^1";
                    }
                }
            }
            var barAsString = color;
            for (var i = 0; i < barsFilled; i++)
            {
                barAsString += "l";
            }
            if (barsFilled < bars)
            {
                barAsString += "^7";
                for (var i = barsFilled; i < bars; i++)
                {
                    barAsString += "l";
                }

                _ = barAsString.Length;
            }
            return barAsString;
        }
    }
}