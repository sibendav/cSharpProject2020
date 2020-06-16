using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Configuration
    {
        static int runningNumberForHost = 10000000;
        static int runningNumberForHostingUnit = 10000000;
        static int runningNumberForGuestRequest = 10000000;
        static int runningNumberForOrder = 10000000;

        public static double fee = 10;
        public static int numDaysExpire = 10;

        public static int getRunningNumberForHost()
        {
            return runningNumberForHost++;
        }
        public static int getRunningNumberForHostingUnit()
        {
            return runningNumberForHostingUnit++;
        }
        public static int getRunningNumberForGuestRequest()
        {
            return runningNumberForGuestRequest++;
        }
        public static int getRunningNumberForOrder()
        {
            return runningNumberForOrder++;
        }
    }
}
