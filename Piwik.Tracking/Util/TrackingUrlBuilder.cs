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
using System.Windows.Browser;

namespace Piwik.Tracking.Util
{
    public class TrackingUrlBuilder
    {
        private StringBuilder _builder;
        private bool _firstParam;

        public TrackingUrlBuilder(string apiUrl)
        {
            if (!apiUrl.EndsWith("/piwik.php", StringComparison.InvariantCultureIgnoreCase))
            {
                apiUrl += "/piwik.php";
            }

            _builder = new StringBuilder(apiUrl);

            _firstParam = true;
        }

        public TrackingUrlBuilder Add(string param, string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                AddSeparator();
                _builder.AppendFormat("{0}={1}", param, HttpUtility.UrlEncode(value));
            }

            return this;
        }

        public TrackingUrlBuilder Add(string param, int value)
        {
            AddSeparator();
            _builder.AppendFormat("{0}={1}", param, value);

            return this;
        }

        public TrackingUrlBuilder Add(string param, bool value)
        {
            AddSeparator();
            _builder.AppendFormat("{0}={1}", param, value?1:0);

            return this;
        }

        public TrackingUrlBuilder Add(string param, int? value)
        {
            if (value.HasValue)
            {
                AddSeparator();
                _builder.AppendFormat("{0}={1}", param, value); // TODO: How to format this value - remove any ,?
            }

            return this;
        }

        public TrackingUrlBuilder AddFormat(string param, string format, params object[] values)
        {
            AddSeparator();
            _builder.AppendFormat("{0}={1}", param, String.Format(format, values));

            return this;
        }

        private void AddSeparator()
        {
            if (_firstParam)
            {
                _firstParam = false;
                _builder.Append("?");
            }
            else
            {
                _builder.Append("&");
            }
        }

        public override string ToString()
        {
            return _builder.ToString();
        }


    }
}
