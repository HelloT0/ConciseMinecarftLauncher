using ConciseMinecarftLauncherBasic.Models.Downloads.ChooseVersion;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ConciseMinecarftLauncherBasic.Models.Downloads;

public partial class DownloadGame : ContentPage
{

    public DownloadGame()
	{
		InitializeComponent();
    }
    public void ChangeList(object sender, CheckedChangedEventArgs e)
    {
    }

    private async void RefreshVersion(object sender, EventArgs e)
    {
        GetGameVersion getGameVersion = new GetGameVersion();
        await getGameVersion.GetVersionJson(); 
    }
    private async void ChooseGameVersion(object sender, EventArgs e) 
    {
        Navigation.PushAsync(new ChooseGame());
    }

}