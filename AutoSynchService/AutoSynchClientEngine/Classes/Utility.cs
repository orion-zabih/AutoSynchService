using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchClientEngine.Classes
{
    public class Utility
    {
        public static List<string> ConverCommaSeparatedToList(string css)
        {
            List<string> list = new List<string>();
            string[] arr= css.Split(',');
            list.AddRange(arr);
            return list;
        }
        public static DateTime GetNextDayMorningDateTime()
        {
            DateTime now = DateTime.Now;
            now = now.AddDays(1);
            now=new DateTime(now.Year, now.Month, now.Day,0,0,1);
            return now;
        }
        public static DateTime GetOldDateTime(int days)
        {
            DateTime now = DateTime.Now;
            now = now.AddDays(-1*days);
            now = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            return now;
        }
        public static string GetDateTimeStringDDMMYYYY(DateTime dt)
        {
            return dt.Year+"-"+dt.Month+"-" + dt.Day;
        }
        public static DateTime GetNextWeekMorningDateTime()
        {
            DateTime now = DateTime.Now;
            now = now.AddDays(7);
            now = new DateTime(now.Year, now.Month, now.Day, 0, 0, 1);
            return now;
        }
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
