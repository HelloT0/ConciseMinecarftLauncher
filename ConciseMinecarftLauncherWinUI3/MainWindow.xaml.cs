using ConciseMinecarftLauncherWinUI3.Pages.DownInstall;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using System.Collections.Generic;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ConciseMinecarftLauncherWinUI3
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            showPage.Navigate(typeof(HomePage));
        }

        private void OpeClo_basicSp(object sender, RoutedEventArgs e)
        {
            if(basicSplit.IsPaneOpen)
            {
                basicSplit.IsPaneOpen = false;
            }
            else
            {
                basicSplit.IsPaneOpen = true;
            }
        }

        private void ToPages(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = navListView.SelectedIndex;
            // 根据所选项目的索引执行不同的操作
            switch (selectedIndex)
            {
                case 0:
                    showPage.Navigate(typeof(HomePage));
                    conGrid.RowDefinitions[1].Height = new GridLength(0);
                    break;
                case 1:
                    conGrid.RowDefinitions[1].Height = new GridLength(60);
                    barsGrid.RowDefinitions[0].Height = new GridLength(60);
                    showPage.Navigate(typeof(DownloadGame));
                    break;
            }
        }

        private void Window_SizeChanged(object sender, WindowSizeChangedEventArgs args)
        {
            double windowWidth = args.Size.Width;
            if (windowWidth >= 750)
            {
                basicSplit.DisplayMode = SplitViewDisplayMode.Inline;
                basicSplit.IsPaneOpen = true;
                showListV.Height = 0;
            }
            else
            {
                showListV.Height = 40;
                basicSplit.DisplayMode = SplitViewDisplayMode.Overlay;
            }
        }

        private void NaV1_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem is NavigationViewItem selectedItem)
            {
                int selectedIndex = sender.MenuItems.IndexOf(selectedItem);
                // 根据索引值进行不同的操作
                switch (selectedIndex)
                {
                    case 0:
                        showPage.Navigate(typeof(DownloadGame));
                        break;
                    case 1:
                        // 处理选中了索引为 1 的情况
                        break;
                    case 2:
                        // 处理选中了索引为 2 的情况
                        break;
                    case 3:
                        showPage.Navigate(typeof(DownloadingList));
                        break;
                }
            }
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            if (showPage.CanGoBack)
            {
                showPage.GoBack();
            }
        }

    }
}
