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
using System.Text;
using Piwik.Tracking.Util;

namespace Piwik.Tracking
{
    public class PiwikTracker
    {
        // TODO: Custom Data
        
        const int VERSION = 1;

        private Random _rand = new Random();
        private string _apiUrl;
        private int _siteId;

        public string PageUrl { get; set; }
        public string UrlReferer { get; set; }

        public LocalTime LocalTime { get; set; }
        public Resolution Resolution { get; set; }
        public Plugins Plugins { get; set; }
        public bool HasCookies { get; set; }

        public PiwikTracker(int siteId, string apiUrl)
        {
            _siteId = siteId;
            _apiUrl = apiUrl;

            Plugins = new Plugins { Silverlight = true }; // sic!
        }

        public void TrackPageView(string documentTitle)
        {
            var builder = BuildRequest(_siteId)
                .Add("action_name", documentTitle);

            SendRequest(builder.ToString());
        }

        public void TrackGoal(int goalId)
        {
            TrackGoal(goalId, null);
        }

        public void TrackGoal(int goalId, int? revenue)
        {
            var builder = BuildRequest(_siteId)
                .Add("idgoal", goalId)
                .Add("revenue", revenue);

            SendRequest(builder.ToString());
        }

        public void TrackLink(string actionUrl)
        {
            TrackAction(actionUrl, "link");
        }

        public void TrackDownload(string actionUrl)
        {
            TrackAction(actionUrl, "download");
        }

        public void TrackAction(string actionUrl, string actionType)
        {
            var builder = BuildRequest(_siteId)
                .Add(actionType, actionUrl)
                .Add("redirect", "0");

            SendRequest(builder.ToString()); 
        }

        private void TrackingCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            var buffer = new byte[e.Result.Length];
            e.Result.Read(buffer, 0, buffer.Length);

            var resultString = UTF8Encoding.UTF8.GetString(buffer, 0, buffer.Length);
        }

        private void SendRequest(string url)
        {
            var client = new WebClient();
            client.OpenReadCompleted += new OpenReadCompletedEventHandler(TrackingCompleted);

            client.OpenReadAsync(new Uri(url));
        }

        private TrackingUrlBuilder BuildRequest(int siteId)
        {
            if (String.IsNullOrEmpty(_apiUrl))
            {
                throw new InvalidOperationException("You must first set the Piwik Tracker URL, where piwik is installed!");
            }

            var builder = new TrackingUrlBuilder(_apiUrl)
                .Add("idsite", _siteId)
                .Add("rec", 1)
                .Add("apiv", VERSION)
                .Add("url", PageUrl)
                .Add("urlref", UrlReferer)
                .Add("rand", _rand.Next());

            if (Resolution != null)
            {
                builder.AddFormat("res", "{0}x{1}", Resolution.Width, Resolution.Height);
            }

            if (LocalTime != null)
            {
                builder.Add("h", LocalTime.Hours).Add("m", LocalTime.Minutes).Add("s", LocalTime.Seconds);
            }

            if (HasCookies)
            {
                builder.Add("cookie", HasCookies);
            }

            if (Plugins != null)
            {
                builder.Add("fla", Plugins.Flash)
                    .Add("java", Plugins.Java)
                    .Add("dir", Plugins.Director)
                    .Add("qt", Plugins.QuickTime)
                    .Add("realp", Plugins.RealPlayer)
                    .Add("pdf", Plugins.Pdf)
                    .Add("wma", Plugins.WindowsMedia)
                    .Add("gears", Plugins.Gears)
                    .Add("ag", Plugins.Silverlight);
            }

            return builder;
        }

        private string AppendPiwikPath(string _apiUrl)
        {
            return _apiUrl.EndsWith("/piwik.php", StringComparison.InvariantCultureIgnoreCase) ? _apiUrl : _apiUrl + "/piwik.php";
        }
    }
}
