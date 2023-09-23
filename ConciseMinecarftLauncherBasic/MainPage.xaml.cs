using ConciseMinecarftLauncherBasic.Models.Downloads;

namespace ConciseMinecarftLauncherBasic;

public partial class MainPage : ContentPage
{
    public string HomePageTitle { get; set; }
	public MainPage()
	{
        HomePageTitle = "1145145";
		InitializeComponent();

	}
    //以下代码为前往各个页面的代码
    //以下为前往所有与下载有关页面的代码
	private void GoToDGame(object sender, EventArgs e)
	{
        Navigation.PushAsync(new DownloadGame());
    }
    private void GoToLocalInstall(object sender, EventArgs e)
    {
        Navigation.PushAsync(new LocalInstallGame());
    }
    private void GoToDMod(object sender, EventArgs e)
    {
        Navigation.PushAsync(new DownloadMod());
    }
    private void GoToInstalingPage(object sender, EventArgs e)
    {
        Navigation.PushAsync(new InstallingGame());
    }
    private void LookMyDownload(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MyDownloads());
    }

}

