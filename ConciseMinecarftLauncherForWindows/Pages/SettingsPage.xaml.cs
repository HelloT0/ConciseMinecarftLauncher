using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ConciseMinecarftLauncherForWindows.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string skin = e.AddedItems[0].ToString();
            switch (skin)
            {
                case "Steven":
                    //skinGrid.Children.Clear();
                    
                    break;
                case "Alex":
                    skinGrid.Children.Clear();
                    break;
                case "Online skin":
                    break;
                case "Offline skin":
                    skinGrid.Children.Clear();
                    TextBlock textBlock1 = new()
                    {
                        Text = "Please choose a picture as your player skin."
                    };
                    Grid.SetRow(textBlock1, 0);
                    skinGrid.Children.Add(textBlock1);
                    Button button1 = new()
                    {
                        Content = "Click me to choose a picture."
                    };
                    button1.Click += Button1_Click;
                    Grid.SetRow(button1, 1);
                    skinGrid.Children.Add(button1);
                    TextBlock textBlock2 = new()
                    {
                        Text = "File address:"
                    };
                    Grid.SetRow(textBlock2, 2);
                    skinGrid.Children.Add(textBlock2);
                    break;
            }
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            // Create a file picker
            var openPicker = new Windows.Storage.Pickers.FileOpenPicker();

            

        }
    }
}
