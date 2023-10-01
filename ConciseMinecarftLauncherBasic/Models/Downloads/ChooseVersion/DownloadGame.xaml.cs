
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
    int clickGameTime = 0;
    private async void SelectGameVer(object sender, SelectionChangedEventArgs e)
    {
        int temp = clickGameTime;
        await Task.Delay(800);
        if(temp>0)
        {
            ChooseGame(sender, e);
            clickGameTime = temp = 0;
        }
        else
            clickGameTime=temp = 0;
    }

    private void ChooseGame(object sender, EventArgs e)
    {
        GetGameVersion getGameVersion=new GetGameVersion();
        getGameVersion.ChoiceGame();
    }
}