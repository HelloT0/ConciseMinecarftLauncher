/*
This code file is written in ANSI.
If you are using UTF-8 or other encoding to open this file, please use ANSI encoding to reopen this file.
If you see garbled code in this code file, please use ANSI encoding to reopen this file.
*/

using ConciseMinecarftLauncherForWindows.Pages;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ConciseMinecarftLauncherForWindows
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void NavigationView1_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected == true)
            {
                showllAllPages.Navigate(typeof(SettingsPage), args.RecommendedNavigationTransitionInfo);
                sender.Header = "…Ë÷√";
            }
            else
            {
                var selectedItem = (Microsoft.UI.Xaml.Controls.NavigationViewItem)args.SelectedItem;
                string selectedItemTag = ((string)selectedItem.Tag);
                sender.Header = selectedItem.Content;
                string pageName = "ConciseMinecarftLauncherForWindows.Pages." + selectedItemTag;
                Type pageType = Type.GetType(pageName);
                showllAllPages.Navigate(pageType);
            }
        }

        private void NavigationView1_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (showllAllPages.CanGoBack)
                showllAllPages.GoBack();
        }
    }
}
