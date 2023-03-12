using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchPosService.Classes
{
    public class Utility
    {
        public static Int32 CalculateBackoffTime(string unit,int timeLength)
        {
            Int32 calculatedTime = 10000;
            switch (unit)
            {
                case "hr":
                    {
                        calculatedTime= (Int32)TimeSpan.FromHours(timeLength).TotalMilliseconds;
                    }
                    break;
                case "min":
                    {
                        calculatedTime = (Int32)TimeSpan.FromMinutes(timeLength).TotalMilliseconds;
                    }
                    break;
                case "sec":
                    {
                        calculatedTime = (Int32)TimeSpan.FromSeconds(timeLength).TotalMilliseconds;
                    }
                    break;
                default:
                    break;
            }
            return calculatedTime;
        } 
    }
}
