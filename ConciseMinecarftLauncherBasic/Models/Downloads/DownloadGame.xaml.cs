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
        GetGameVersion getGameVersion = new();
        //getGameVersion.FxiGameJson();
    }
}