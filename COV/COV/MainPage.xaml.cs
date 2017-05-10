using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using VungleSDK;
using Windows.UI.Core;
using Microsoft.Advertising.WinRT.UI;
using SOMAW81;
using Windows.Storage;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace COV
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        VungleAd sdkInstance;
        InterstitialAd MyVideoAd = new InterstitialAd();

        SomaInterstitialAd interstitialAd = new SomaInterstitialAd();
        public MainPage()
        {
            this.InitializeComponent();

            //Vungle
            var appId = "9nblggh0jk36";
            sdkInstance = AdFactory.GetInstance(appId);
            sdkInstance.OnAdPlayableChanged += SdkInstance_OnAdPlayableChanged;

            //Microsoft Ads
            var MyAppId = "85a2b1b7-73ac-4d11-884f-323f6fa58aa0";
            var MyAdUnitId = "251474";

            MyVideoAd.AdReady += MyVideoAd_AdReady;
            MyVideoAd.RequestAd(AdType.Video, MyAppId, MyAdUnitId);

           

            SystemNavigationManager.GetForCurrentView().BackRequested += SystemNavigationManager_BackRequested;
        }
        void interstitialAd_NewAdAvailable(object sender, EventArgs e)
        {
            // display the ad
            interstitialAd.ShowInterstitial();

            //Smaato
            int AdSpaceId = 130273957;
            int PublisherId = 1100022520;
            try
            {
                AdSpaceId = int.Parse(ApplicationData.Current.LocalSettings.Values["adSpaceId"].ToString());
                PublisherId = int.Parse(ApplicationData.Current.LocalSettings.Values["publisherId"].ToString());
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                System.Diagnostics.Debug.WriteLine(errorMessage);
                AdSpaceId = 0;
                PublisherId = 0;
            }
            interstitialAd.Adspace = AdSpaceId;
            interstitialAd.Pub = PublisherId;

            interstitialAd.NewAdAvailable += interstitialAd_NewAdAvailable;
            interstitialAd.Format = SomaAd.FormatRequested.all;
            interstitialAd.LoadInterstitial();
        }

        private void SystemNavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;

            if (frame.CanGoBack || webView1.CanGoBack)
            {
                e.Handled = true;

                if (webView1.CanGoBack)
                    webView1.GoBack();


                if (frame.CanGoBack)
                    frame.GoBack();
            }
        }

        private void MyVideoAd_AdReady(object sender, object e)
        {
            if ((InterstitialAdState.Ready) == (MyVideoAd.State))
            {
                MyVideoAd.Show();
            }
        }

        private async void SdkInstance_OnAdPlayableChanged(object sender, AdPlayableEventArgs e)
        {
            await sdkInstance.PlayAdAsync(new AdConfig()
            {
                // set any configuration options you like. 
                // For a full description of available options, see the 'Configuration Options' section.
                Incentivized = true,
                SoundEnabled = false
            });

            //AdDuplex
            AdDuplex.InterstitialAd interstitialAd = new AdDuplex.InterstitialAd("206263");
            await interstitialAd.ShowAdAsync();

        }
    }
}
