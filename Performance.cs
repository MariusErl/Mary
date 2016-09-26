using System;
using System.Diagnostics;

namespace Mary
{
    class Performance
    {
        static public PerformanceCounter cpuCounter;
        static public PerformanceCounter ramCounter;
        static public void a()
        {
            cpuCounter = new PerformanceCounter();

            cpuCounter.CategoryName = "Processor";
            cpuCounter.CounterName = "% Processor Time";
            cpuCounter.InstanceName = "_Total";

            ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }
        static public string getCurrentCpuUsage()
        {
            return Math.Round(cpuCounter.NextValue(), 0) + "%";
        }

        static public string getAvailableRAM()
        {
            return ramCounter.NextValue() + "MB";
        } 
    }
}
