using ConciseMinecarftLauncherBasic.Models.DownloadandInstall;

namespace ConciseMinecarftLauncherBasic.ViewModels.DownloadandInstall;

public partial class DownloadGame : ContentPage
{
	public DownloadGame()
	{
		InitializeComponent();
        // 保存原始内容
    }
    private void UpdateChooseOrgameContent()
    {
        ChooseOrGame.Content = new Label
		{ Text = "New Content", HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };

    }
    bool openChooseGame = false;
	private void ShowOpChoseGame(object sender, EventArgs e)
	{
		GetGameVersion getGameVersion = new GetGameVersion();
		if(openChooseGame) 
		{
			getGameVersion.ShowOpChoseGame(false);
			openChooseGame = false;
		}
		else
		{
			getGameVersion.ShowOpChoseGame(true);
			openChooseGame = true;
		}
		UpdateChooseOrgameContent();
	}
}