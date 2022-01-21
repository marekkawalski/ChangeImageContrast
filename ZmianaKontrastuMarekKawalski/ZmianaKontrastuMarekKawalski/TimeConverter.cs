using System;
using System.Collections.Generic;
using System.Text;
using System.Management;

namespace ChangeContrastMarekKawalski
{
    public class TimeConverter
    {
        private uint currentFrequencyMhz = 0;

        private void EstablishCPUSpeed()
        {
            using ManagementObject Mo = new ManagementObject("Win32_Processor.DeviceID='CPU0'");
            currentFrequencyMhz = (uint)Mo["CurrentClockSpeed"];
        }

        public float ConvertTicsToMiliseconds(long tics)
        {
            EstablishCPUSpeed();
            return tics / (float)(currentFrequencyMhz * 1000);
        }
    }
}