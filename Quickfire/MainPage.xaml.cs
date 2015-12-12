using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Quickfire
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            Application.Current.Suspending += Application_Suspending;
            Application.Current.Resuming += Application_Resuming;
        }

        private async void Application_Suspending(object sender, SuspendingEventArgs e)
        {
            // Handle global application events only if this page is active
            if (Frame.CurrentSourcePageType == typeof(MainPage))
            {
                var deferral = e.SuspendingOperation.GetDeferral();

                await CapturePhotoVideoControl.Stop();

                await ShowPhoneStatusBarIfPresent();

                deferral.Complete();
            }
        }

        private async void Application_Resuming(object sender, object o)
        {
            // Handle global application events only if this page is active
            if (Frame.CurrentSourcePageType == typeof(MainPage))
            {
                await HidePhoneStatusBarIfPresent();
                await CapturePhotoVideoControl.Initialize(DisplayInformation.GetForCurrentView());
            }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await HidePhoneStatusBarIfPresent();
            await CapturePhotoVideoControl.Initialize(DisplayInformation.GetForCurrentView());
        }

        protected override async void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            await CapturePhotoVideoControl.Stop();
            await ShowPhoneStatusBarIfPresent();
        }

        private async Task HidePhoneStatusBarIfPresent()
        {
            // Hide the status bar
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                await Windows.UI.ViewManagement.StatusBar.GetForCurrentView().HideAsync();
            }
        }

        private async Task ShowPhoneStatusBarIfPresent()
        {
            // Show the status bar
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                await Windows.UI.ViewManagement.StatusBar.GetForCurrentView().ShowAsync();
            }
        }

        private void CapturePhotoVideoControl_Holding(object sender, HoldingRoutedEventArgs e)
        {

        }

        private void CapturePhotoVideoControl_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
    }
}
