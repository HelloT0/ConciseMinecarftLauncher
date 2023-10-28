using ConciseMinecarftLauncherWinUI3.Models.DwnInsall;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ConciseMinecarftLauncherWinUI3.Pages.DownInstall;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class DownloadGame : Page
{
    public DownloadGame()
    {
        this.InitializeComponent();
    }

    public List<string> OfficalVer { get; set; } = new List<string>();

    private void Page_Loading(FrameworkElement sender, object args)
    {
        
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        DownGame downGame = new();
        downGame.GetOfficial();
    }
}
