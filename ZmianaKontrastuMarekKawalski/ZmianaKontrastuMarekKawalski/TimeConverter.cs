using System;
using System.Collections.Generic;
using System.Text;
using System.Management;

namespace ChangeContrastMarekKawalski
{
    /// <summary>
    /// Class reads current cpu clock and converts cpu tics to milliseconds
    /// </summary>
    public class TimeConverter
    {
        private uint currentFrequencyMhz = 0;

        /// <summary>
        /// Establish current cpu speed in mHz
        /// </summary>
        private void EstablishCPUSpeed()
        {
            using ManagementObject Mo = new ManagementObject("Win32_Processor.DeviceID='CPU0'");
            currentFrequencyMhz = (uint)Mo["CurrentClockSpeed"];
        }

        /// <summary>
        /// Convert cpu tics to milliseconds
        /// </summary>
        /// <param name="tics">number of tics</param>
        /// <returns>execution time in milliseconds</returns>
        public float ConvertTicsToMiliseconds(long tics)
        {
            EstablishCPUSpeed();
            return tics / (float)(currentFrequencyMhz * 1000);
        }
    }
}