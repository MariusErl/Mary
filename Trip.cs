using System;
using System.Diagnostics;

namespace Mary
{
    class Trip
    {
        static public int TripbtnShow = 1;
        //1=dst, 2=time, 3=avg, 4=top
        static public void UpdateTripBtn()
        {
            switch (TripbtnShow)
            {
                case 1:
                    ButtonFactory.tripInfo.Text = Settings.UNITKPH ? "^7Dst: " + Math.Round(Identification.MySelf.P.tripDst / 1000, 1) + " km" : "^7" + Math.Round(Identification.MySelf.P.tripDst / 1000 * 0.621371192, 1) + " miles";
                    break;
                case 2:
                    ButtonFactory.tripInfo.Text = ("^7Time: " + tripWatch.Elapsed.ToString(@"hh\:mm\:ss"));
                    break;
                case 3:
                    double speedInKilometresPerHour = (Identification.MySelf.P.tripDst / 1000) / TimeSpan.Parse(tripWatch.Elapsed.ToString(@"hh\:mm\:ss")).TotalHours;
                    ButtonFactory.tripInfo.Text = "^7Avg: " + (Settings.UNITKPH ? Math.Round(speedInKilometresPerHour, 1) + " kph" : Math.Round(speedInKilometresPerHour * 0.621371192, 1) + " mph");
                    break;
                case 4:
                    ButtonFactory.tripInfo.Text = "^7Max: " + Identification.MySelf.P.topSpeed + (Settings.UNITKPH ? " kph" : " mph");
                    break;
            }
            ButtonFactory.OpenButton(ButtonFactory.tripInfo);
        }

        static public Stopwatch tripWatch = new Stopwatch();
    }
}