using System;
using System.Diagnostics;

namespace Mary
{
    class Performance
    {
        static public PerformanceCounter cpuCounter;
        static public PerformanceCounter ramCounter;
        static public void InitiatePerformanceGadgets()
        {
            cpuCounter = new PerformanceCounter
            {
                CategoryName = "Processor",
                CounterName = "% Processor Time",
                InstanceName = "_Total"
            };

            ramCounter = new PerformanceCounter
            {
                CategoryName = "Memory",
                CounterName = "Available MBytes",
                InstanceName = ""
            };
        }
        public static string GetCurrentCpuUsage()
        {
            return Math.Round(cpuCounter.NextValue(), 1) + "%";
        }

        public static string GetAvailableRAM()
        {
            return Math.Round(ramCounter.NextValue() / 1000, 1) + "GB";
        }
    }
}
