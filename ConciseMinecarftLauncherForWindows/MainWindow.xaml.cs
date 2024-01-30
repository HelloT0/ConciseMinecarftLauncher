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

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            /*
             * Part of the code is borrowed from 
             * https://github.com/microsoft/WinUI-Gallery/blob/main/WinUIGallery/ControlPages/NavigationViewPage.xaml.cs
             */
            if (args.IsSettingsSelected)
            {
                AllContent.Navigate(typeof(SettingsPage));
                sender.Header = "Settings";
            }
            else
            {
                var selectedItem = (Microsoft.UI.Xaml.Controls.NavigationViewItem)args.SelectedItem;
                string selectedContent = ((string)selectedItem.Content);
                sender.Header = selectedContent;
                string selectedTag = ((string)selectedItem.Tag);
                string pageName = "ConciseMinecarftLauncherForWindows.Pages." + selectedTag+"Page";
                Type pageType = Type.GetType(pageName);
                AllContent.Navigate(pageType);
            }
        }

        private void NavigationView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (AllContent.CanGoBack)
            {
                AllContent.GoBack();
            }
        }
    }
}
