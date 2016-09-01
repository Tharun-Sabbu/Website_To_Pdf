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
using System.Device.Location;
using Microsoft.Advertising.Mobile.UI;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.IO;
using Microsoft.Phone.Shell;

namespace WEBSITE_TO_PDF
{
    public partial class Page2 : PhoneApplicationPage
    {
        private GeoCoordinateWatcher gcw = null;
        private const string APPLICATION_ID = "";
        private const string AD_UNIT_ID = "";
        private const string _key = "url";

        public Page2()
        {
            InitializeComponent();
            this.gcw = new GeoCoordinateWatcher();
            this.gcw.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(gcw_PositionChanged);
            this.gcw.Start();
            webBrowser1.Navigating +=new EventHandler<NavigatingEventArgs>(webBrowser1_Navigating);
            webBrowser1.Navigated += new EventHandler<System.Windows.Navigation.NavigationEventArgs>(webBrowser1_Navigated);
            this.Loaded += new RoutedEventHandler(Page2_Loaded);

        }

        void gcw_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            this.gcw.Stop();

            adControl1.Latitude = e.Position.Location.Latitude;
            adControl1.Longitude = e.Position.Location.Longitude;

            Debug.WriteLine("Device lat/long: " + e.Position.Location.Latitude + ", " + e.Position.Location.Longitude);
        }

        void Page2_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(APPLICATION_ID) && !string.IsNullOrEmpty(AD_UNIT_ID))
                {
                    adControl1 = new Microsoft.Advertising.Mobile.UI.AdControl(APPLICATION_ID, AD_UNIT_ID, true);
                }
                roundToggleButton1.Background = new SolidColorBrush(Colors.Transparent);
                roundToggleButton1.BorderBrush = new SolidColorBrush(Colors.Transparent);
                webBrowser1.Margin = new Thickness(-7, -35, 0, 0);
                if (ink1.Visibility == Visibility.Collapsed)
                {
                    webBrowser1.Margin = new Thickness(-7, -92, 0, 0);
                    webBrowser1.Height = 669;
                }
                IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();
                IsolatedStorageFileStream fileStream = myIsolatedStorage.OpenFile("myFile.txt", FileMode.Open, FileAccess.Read);
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    this.phoneTextBox1.Text = reader.ReadLine();
                }
            }
            catch
            {
            }
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (phoneTextBox1.Text == string.Empty)
                {
                    MessageBox.Show("Please enter your keyword for searching");
          
                }
                else
                {
                    string site;
                    site = phoneTextBox1.Text;
                    webBrowser1.Navigate(new Uri("http://www.google.com/search?q=" + site, UriKind.RelativeOrAbsolute));
                    webBrowser1.Navigating += webBrowser1_Navigating;
                 
                }

            }
            catch
            {
            }
        }
        private void webBrowser1_Navigating(object sender, NavigatingEventArgs e)
        {
            string site;
            site = phoneTextBox1.Text;
            phoneTextBox1.Text = e.Uri.ToString();
            progress.Visibility = Visibility.Visible;
           
        }

       

      
        void webBrowser1_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            progress.Visibility = Visibility.Collapsed;
        }

      

        private void BackBtn_Click(object sender, System.EventArgs e)
        {
            // TODO: Add event handler implementation here.
            try
            {
                webBrowser1.InvokeScript("eval", "history.go(-1)");
               
            }
            catch
            {
            }
        }

        private void SaveBtn_Click(object sender, System.EventArgs e)
        {
        	// TODO: Add event handler implementation here.
            try
            {
                if (phoneTextBox1.Text == string.Empty)
                {
                    MessageBox.Show("empty page");
                    progress.Visibility = Visibility.Collapsed;
                }
                else
                {
                    string site = phoneTextBox1.Text;
                    string url = HttpUtility.UrlEncode(site);
                    WebBrowserTask webBrowserTask = new WebBrowserTask();
                    webBrowserTask.Uri = new Uri("http://pdfmyurl.com?url=" + site, UriKind.RelativeOrAbsolute);
                    webBrowserTask.Show();
                }
            }
            catch
            {
            }
        }

        private void ForwardBtn_Click(object sender, System.EventArgs e)
        {
        	// TODO: Add event handler implementation here.
            try
            {
                webBrowser1.InvokeScript("eval", "history.go(1)");
                progress.Visibility = Visibility.Visible;
            }
            catch
            {
            }
        }

        private void share_click(object sender, System.EventArgs e)
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

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                webBrowser1.InvokeScript("eval", "history.go()");
                progress.Visibility = Visibility.Visible;
        
              
            }
            catch
            {
            }
        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {


                    string site;
                    site = phoneTextBox1.Text;
                    webBrowser1.Navigate(new Uri("http://www.google.com/search?q=" + site, UriKind.RelativeOrAbsolute));
                    webBrowser1.Navigating += webBrowser1_Navigating;
                    progress.Visibility = Visibility.Visible;
                    this.Focus();
                }

                else
                {
                    return;
                }
            }
            catch
            {
            }
        }

        private void phoneTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            string site;
            site = phoneTextBox1.Text;
            IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();
            using (StreamWriter writeFile = new StreamWriter(new IsolatedStorageFileStream("mymailto.txt", FileMode.Create, FileAccess.Write, myIsolatedStorage)))
            {
                string someTextDat = phoneTextBox1.Text;
                writeFile.WriteLine(someTextDat);
                writeFile.Close();
            }
        }

        private void orienon(object sender, System.EventArgs e)
        {
        	// TODO: Add event handler implementation here.
            try
            {

                SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;
            }
            catch
            {
            }
        }

        private void oreintationoff(object sender, System.EventArgs e)
        {
            try
            {

                SupportedOrientations = SupportedPageOrientation.Portrait;
            }
            catch
            {
            }
        }

       

        private void adControl1_AdRefreshed(object sender, EventArgs e)
        {
            Debug.WriteLine("AdControl new ad received");
        }

        private void adControl1_ErrorOccurred(object sender, Microsoft.Advertising.AdErrorEventArgs e)
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

        private void ie_CLick(object sender, System.EventArgs e)
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

        private void phonetextbox_focus(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                phoneTextBox1.Background = new SolidColorBrush(Colors.White);
                phoneTextBox1.BorderBrush = new SolidColorBrush(Colors.Transparent);

            }
            catch
            {
            }
        }
        #endregion

       

        private void phoneclick_focus(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                roundToggleButton1.Background = new SolidColorBrush(Colors.Transparent);
                roundToggleButton1.BorderBrush = new SolidColorBrush(Colors.Transparent);
            }
            catch
            {
            }
        }

        private void roundToggleButton1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                phoneTextBox1.Text = "";
                
            }
            catch
            {
            }
        }

        private void GestureListener_Flick(object sender, FlickGestureEventArgs e)
        {
            try
            {
                if (e.HorizontalVelocity < 0)
                {

                    ink1.Visibility = Visibility.Collapsed;
                    if (ink1.Visibility == Visibility.Collapsed)
                    {
                        webBrowser1.Margin = new Thickness(-7, -92, 0, 0);
                        webBrowser1.Height = 669;
                    }
                }
                if (e.HorizontalVelocity > 0)
                {
                    ink1.Visibility = Visibility.Visible;
                    webBrowser1.Margin = new Thickness(-7, -35, 0, 0);
                    webBrowser1.Height = 606;
                    if (ink1.Visibility == Visibility.Collapsed)
                    {
                        webBrowser1.Margin = new Thickness(-7, -92, 0, 0);
                        webBrowser1.Height = 669;
                    }
                }
            }
            catch
            {
            }
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            
            if (NavigationContext.QueryString.ContainsKey(_key))
            {
                string url = NavigationContext.QueryString[_key];
                webBrowser1.Navigate(new Uri(url,UriKind.RelativeOrAbsolute));
            }

        }

        private void pintostart_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (phoneTextBox1.Text != string.Empty)
                {

                    string site = phoneTextBox1.Text;

                    ShellTile TileToFind = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("url=" + site));


                    // Create the Tile if we didn't find that it already exists.
                    if (TileToFind == null)
                    {
                        StandardTileData NewTileData = new StandardTileData
                        {
                            BackgroundImage = new Uri("moon.png", UriKind.Relative),

                            Title = "Website to Pdf",
                            BackTitle = "Website to pdf",
                            BackContent = (string)site,
                            BackBackgroundImage = new Uri("moon.png", UriKind.Relative)
                        };

                        // Create the Tile and pin it to Start. This will cause a navigation to Start and a deactivation of our application.
                        ShellTile.Create(new Uri("/Page4.xaml?" + _key + "=" + site, UriKind.Relative), NewTileData);


                    }



                    else
                    {
                        MessageBox.Show("Tile already exists");
                    }

                }
                else
                {
                    MessageBox.Show("No link to pin");
                }
            }
            catch
            {
                MessageBox.Show("No link to pin");
            }

        }
        }
    }
