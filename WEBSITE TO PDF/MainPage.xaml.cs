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
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Net.NetworkInformation;
using System.Device.Location;
using Microsoft.Advertising.Mobile.UI;
using System.Diagnostics;

namespace WEBSITE_TO_PDF
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        private GeoCoordinateWatcher gcw = null;
        private const string APPLICATION_ID = "";
        private const string AD_UNIT_ID = "";
        public MainPage()
        {
            InitializeComponent();
            this.gcw = new GeoCoordinateWatcher();
            this.gcw.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(gcw_PositionChanged);
            this.gcw.Start();
            if (!IsWirelessEnabled() && !IsCellularEnabled())
            {
                MessageBox.Show("No Network Access");
            }
            Application.Current.UnhandledException += new EventHandler<ApplicationUnhandledExceptionEventArgs>(Current_UnhandledException);
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);

        }

        void gcw_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            this.gcw.Stop();

            adControl1.Latitude = e.Position.Location.Latitude;
            adControl1.Longitude = e.Position.Location.Longitude;
            adControl2.Latitude = e.Position.Location.Latitude;
            adControl2.Longitude = e.Position.Location.Longitude;
            Debug.WriteLine("Device lat/long: " + e.Position.Location.Latitude + ", " + e.Position.Location.Longitude);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(APPLICATION_ID) && !string.IsNullOrEmpty(AD_UNIT_ID))
            {
                adControl1 = new Microsoft.Advertising.Mobile.UI.AdControl(APPLICATION_ID, AD_UNIT_ID, true);
                adControl2 = new Microsoft.Advertising.Mobile.UI.AdControl(APPLICATION_ID, AD_UNIT_ID, true);
            }
        }
       
        void Current_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }
     
        
 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Uri("/Page4.xaml", UriKind.RelativeOrAbsolute));
            }
            catch
            {
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new Uri("/Page2.xaml", UriKind.RelativeOrAbsolute));
            }
            catch
            {
            }

        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MarketplaceReviewTask market = new MarketplaceReviewTask();
                market.Show();
                
            }
            catch
            {
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                string pdf = phoneTextBox1.Text;
                if (phoneTextBox1.Text == string.Empty)
                {
                    MessageBox.Show("First copy your site address inorder to save your favourite site");
                }
                else
                {
                    string url = HttpUtility.UrlEncode(pdf);
                    WebBrowserTask webBrowserTask = new WebBrowserTask();
                    webBrowserTask.Uri = new Uri("http://pdfmyurl.com?url=" + url, UriKind.RelativeOrAbsolute);
                    webBrowserTask.Show();
                }
            }
            catch
            {
            }
        }

        private void phoneTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            string pdf = phoneTextBox1.Text;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                WebBrowserTask webBrowserTask = new WebBrowserTask();
                webBrowserTask.Uri = new Uri(phoneTextBox1.Text, UriKind.RelativeOrAbsolute);
                webBrowserTask.Show();
            }
            catch
            {
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            try
            {
                if (phoneTextBox1.Text == string.Empty)
                {
                    MessageBox.Show("No link to share");
                }
                else
                {
                    EmailComposeTask emailcomposer = new EmailComposeTask();
                    emailcomposer.Body = phoneTextBox1.Text;
                    emailcomposer.Show();
                }
            }
            catch
            {
            }

        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            SystemTray.SetForegroundColor(this, (Color)App.Current.Resources["PhoneForegroundColor"]);
            
        }

        private void HyperlinkButton_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                EmailComposeTask emailcomposer = new EmailComposeTask();
                emailcomposer.To = "mailto:";
                emailcomposer.Show();
            }
            catch
            {
            }
        }
        public void hub_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
          
                try
                {
                    WebBrowserTask webBrowserTask = new WebBrowserTask();
                    webBrowserTask.Uri = new Uri("http://www.windowsphone.com/s?appid=", UriKind.RelativeOrAbsolute);
                    webBrowserTask.Show();
                }

                catch
                {
                }
            }
          private bool IsWirelessEnabled()
        {
            return DeviceNetworkInformation.IsWiFiEnabled;
        }
                  
        private bool IsCellularEnabled()
        {
            return DeviceNetworkInformation.IsCellularDataEnabled;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

            try
            {
                MarketplaceSearchTask market = new MarketplaceSearchTask();
                market.SearchTerms = "Tharun Reddy";
                market.Show();
               
            }
            catch
            {
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WebBrowserTask webBrowserTask = new WebBrowserTask();
                webBrowserTask.Uri = new Uri("http://www.windowsphone.com/s?appid=", UriKind.RelativeOrAbsolute);
                webBrowserTask.Show();
            }

            catch
            {
            }
        }

        private void adControl2_AdRefreshed(object sender, EventArgs e)
        {
            Debug.WriteLine("AdControl new ad received");
        }

        private void adControl2_ErrorOccurred(object sender, Microsoft.Advertising.AdErrorEventArgs e)
        {
            Debug.WriteLine("AdControl error: " + e.Error.Message);
        }
        #region IDisposable Members

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.gcw != null)
                {
                    this.gcw.Dispose();
                    this.gcw = null;
                }
            }
        }

        private void adControl1_ErrorOccurred(object sender, Microsoft.Advertising.AdErrorEventArgs e)
        {
            Debug.WriteLine("AdControl error: " + e.Error.Message);
        }

        private void adControl1_AdRefreshed(object sender, EventArgs e)
        {
            Debug.WriteLine("AdControl new ad received");
        }
        #endregion
    }
}