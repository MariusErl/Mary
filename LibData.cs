using System.Collections.Generic;

namespace Mary
{
    class LibData
    {
        private static readonly Dictionary<string, int> FuelMap = new Dictionary<string, int>()
        {
            { "XFG", 45 },
            { "XRG", 65 },
            { "FBM", 42 },
            { "XRT", 75 },
            { "RB4", 75 },
            { "FXO", 75 },
            { "LX4", 40 },
            { "LX6", 40 },
            { "MRT", 20},
            { "UF1", 35 },
            { "RAC", 42 },
            { "FZ5", 90 },
            { "XFR", 70 },
            { "UFR", 60 },
            { "FOX", 38 },
            { "FO8", 125 },
            { "BF1", 95 },
            { "FXR", 100 },
            { "XRR", 100 },
            { "FZR", 100 },
            { "VWS", 0 },
        };

        private static readonly Dictionary<string, int> MaxRPMmap = new Dictionary<string, int>()
        {
            { "XFG", 8000 },
            { "XRG", 7000 },
            { "FBM", 9000 },
            { "XRT", 7500 },
            { "RB4", 7500 },
            { "FXO", 7500 },
            { "LX4", 9000 },
            { "LX6", 9000 },
            { "MRT", 13000},
            { "UF1", 7000 },
            { "RAC", 7000 },
            { "FZ5", 8000 },
            { "XFR", 8000 },
            { "UFR", 9000 },
            { "FOX", 7500 },
            { "FO8", 9500 },
            { "BF1", 20000 },
            { "FXR", 7500 },
            { "XRR", 7500 },
            { "FZR", 8500 },
            { "VWS", 0 },
        };



        public static int GetFueltankSize(string shortCarName)
        {
            if (FuelMap.TryGetValue(shortCarName, out int size))
            {
                return size;
            }
            return 0;
        }

        public static int GetMaxRPM(string shortCarName)
        {
            if (MaxRPMmap.TryGetValue(shortCarName, out int size))
            {
                return size;
            }
            return 0;
        }
    }
}
