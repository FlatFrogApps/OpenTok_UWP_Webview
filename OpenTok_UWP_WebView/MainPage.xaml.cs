using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace OpenTok_UWP_WebView
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            OpenTok.Settings.IsJavaScriptEnabled = true;
            OpenTok.Navigate(new Uri("https://whitter-test.flatfrog.com/ot.html"));
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            await OpenTok.InvokeScriptAsync("ShowVideo", new string[] { ApiKey.Text, SessionId.Text, Token.Text });
        }

        private void OpenTok_OnPermissionRequested(WebView sender, WebViewPermissionRequestedEventArgs args)
        {
            if (args.PermissionRequest.PermissionType == WebViewPermissionType.Media)
            {
                args.PermissionRequest.Allow();
            }
        }

        private async void OpenTok_OnNavigationFailed(object sender, WebViewNavigationFailedEventArgs e)
        {
            var errorDialog = new MessageDialog(e.WebErrorStatus.ToString(), "Navigation failed");
            errorDialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
            errorDialog.Commands.Add(new UICommand { Label = "Cancel", Id = 1 });
            var result = await errorDialog.ShowAsync();

            if ((int)result.Id == 0) // OK
            {
            }
        }
    }
}
