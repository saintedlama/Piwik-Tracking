using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Piwik.Tracking
{
    public class LocalTime
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        public static LocalTime Create(DateTime time)
        {
            return new LocalTime
            {
                Hours = time.Hour,
                Minutes = time.Minute,
                Seconds = time.Second
            };
        }

        public static LocalTime Now()
        {
            return Create(DateTime.Now);
        }
    }
}
