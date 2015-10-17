using Microsoft.Advertising.WinRT.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Store;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace VideoAds
{
    
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        InterstitialAd MyVideoAd = new InterstitialAd();

        public LicenseInformation AppLicenseInformation { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            var MyAppId = "85a2b1b7-73ac-4d11-884f-323f6fa58aa0";
            var MyAdUnitId = "251474";

            MyVideoAd.AdReady += MyVideoAd_AdReady;
            MyVideoAd.RequestAd(AdType.Video, MyAppId, MyAdUnitId);

        }
        private void MyVideoAd_AdReady(object sender, object e)
        {
            if ((InterstitialAdState.Ready) == (MyVideoAd.State))
            {
                MyVideoAd.Show();
            }
        }
        private async
           void Show_Click(object sender, RoutedEventArgs e)
        {
            // await new Windows.UI.Popups.MessageDialog("Hello World").ShowAsync();

            if (!AppLicenseInformation.ProductLicenses["RemoveAds"].IsActive)
            {
                try
                {
                    // The customer doesn't own this feature, so 
                    // show the purchase dialog.

                    PurchaseResults results = await CurrentAppSimulator.RequestProductPurchaseAsync("RemoveAds");

                    //Check the license state to determine if the in-app purchase was successful.

                    if (results.Status == ProductPurchaseStatus.Succeeded)
                    {
                        AdMediator_4BF1A7.Visibility = Visibility.Collapsed;
                        appBar.Visibility = Visibility.Collapsed;
                    }
                }
                catch (Exception ex)
                {
                    // The in-app purchase was not completed because 
                    // an error occurred.
                    throw ex;
                }
            }
            else
            {
                await new Windows.UI.Popups.MessageDialog("You already Purchase Remove Ads ").ShowAsync();
            }
        }
    }
}
