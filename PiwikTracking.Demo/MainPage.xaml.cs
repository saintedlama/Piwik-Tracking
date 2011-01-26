using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Piwik.Tracking;

namespace PiwikTracking.Demo
{
    public partial class MainPage : UserControl
    {
        private PiwikTracker _tracker = new PiwikTracker(1, "http://localhost:8080/piwik");

        public MainPage()
        {
            InitializeComponent();
        }

        private void BtnTrackAction2_Click(object sender, RoutedEventArgs e)
        {
            _tracker.UrlReferer = "http://www.orf.at/index.php?piwik_campaign=Adwords-CPC";
            _tracker.PageUrl = "http://derstandard.at/index.php?piwik_campaign=Adwords-CPC&piwik_kwd=My killer keyword";
            _tracker.Resolution = new Resolution { Height = 800, Width = 600 };
            _tracker.LocalTime = LocalTime.Now();
            _tracker.TrackDownload("somedownload");
        }

        private void BtnTrackGoal2_Click(object sender, RoutedEventArgs e)
        {
            _tracker.UrlReferer = "http://www.orf.at/index.php?piwik_campaign=Adwords-CPC";
            _tracker.PageUrl = "http://derstandard.at/index.php?piwik_campaign=Adwords-CPC&piwik_kwd=My killer keyword";
            _tracker.Resolution = new Resolution { Height = 800, Width = 600 };
            _tracker.LocalTime = LocalTime.Now();
            _tracker.TrackGoal(1);
        }

        private void BtnTrackPage2_Click(object sender, RoutedEventArgs e)
        {
            _tracker.UrlReferer = "http://www.orf.at/index.php?piwik_campaign=Adwords-CPC";
            _tracker.PageUrl = "http://derstandard.at/index.php?piwik_campaign=Adwords-CPC&piwik_kwd=My killer keyword";
            _tracker.Resolution = new Resolution { Height = 800, Width = 600 };
            _tracker.LocalTime = LocalTime.Now();
            _tracker.TrackPageView("franz");
        }

        private void BtnTrackPage1_Click(object sender, RoutedEventArgs e)
        {
            _tracker.UrlReferer = "http://www.twitter.com/index.php?piwik_campaign=Bing-CPC";
            _tracker.PageUrl = "http://facebook.com/index.php?piwik_campaign=Twitter-CPC&piwik_kwd=FailWhale";
            _tracker.Resolution = new Resolution { Height = 1900, Width = 1200 };
            _tracker.LocalTime = LocalTime.Now();
            _tracker.TrackPageView("liesbeth");
        }

        private void BtnTrackGoal1_Click(object sender, RoutedEventArgs e)
        {
            _tracker.UrlReferer = "http://www.orf.at/index.php?piwik_campaign=Adwords-CPC";
            _tracker.PageUrl = "http://derstandard.at/index.php?piwik_campaign=Adwords-CPC&piwik_kwd=My killer keyword";
            _tracker.Resolution = new Resolution { Height = 800, Width = 600 };
            _tracker.LocalTime = LocalTime.Now();
            _tracker.TrackGoal(2);
        }

        private void BtnTrackAction1_Click(object sender, RoutedEventArgs e)
        {
            _tracker.UrlReferer = "http://www.orf.at/index.php?piwik_campaign=Adwords-CPC";
            _tracker.PageUrl = "http://derstandard.at/index.php?piwik_campaign=Adwords-CPC&piwik_kwd=My killer keyword";
            _tracker.Resolution = new Resolution { Height = 800, Width = 600 };
            _tracker.LocalTime = LocalTime.Now();
            _tracker.TrackLink("somelink");
        }
    }
}
