using ConciseMinecarftLauncherBasic.Models.Downloads;

namespace ConciseMinecarftLauncherBasic;

public partial class App : Application
{
    string homepagetitle;
	public App()
	{
		InitializeComponent();
        MainPage mainPage = new MainPage();
        homepagetitle = mainPage.HomePageTitle;
        MainPage = new NavigationPage(new MainPage())
        {
            Title = homepagetitle,
        }; 
    }
	public async Task StartRun() 
	{
        GetGameVersion getGameVersion = new();
        await getGameVersion.GetVersionJson();
    }
}
