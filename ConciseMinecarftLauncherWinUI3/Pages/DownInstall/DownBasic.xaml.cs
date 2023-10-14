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

namespace ConciseMinecarftLauncherWinUI3.Pages.DownInstall
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DownBasic : Page
    {
        public DownBasic()
        {
            this.InitializeComponent();
        }


        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
                NavigationViewItem item = args.SelectedItem as NavigationViewItem;

                switch (item.Content.ToString())
                {
                    case "下载游戏":
                        DownFoladerPage.Navigate(typeof(DownGame));
                        break;
                    case "本地安装游戏":

                        DownFoladerPage.Navigate(typeof(LocalInstall));
                        break;
                case "安装模组":

                    DownFoladerPage.Navigate(typeof(DownMod));
                    break;
            }
        }
    }
}
