namespace ConciseMinecarftLauncherBasic.Models.Downloads.ChooseVersion;

public partial class ChooseGame : ContentPage
{
	public ChooseGame()
	{
		InitializeComponent();
		Copy();
	}
    public Dictionary<string, List<string>> AllShowGameVersions { get; set; }
    public void Copy()
    {
        GetGameVersion getGameVersion = new GetGameVersion();
        AllShowGameVersions = new Dictionary<string, List<string>>();
        foreach (KeyValuePair<string, List<string>> kvp in getGameVersion.AllShowGameVersions)
        {
            AllShowGameVersions.Add(kvp.Key, new List<string>(kvp.Value));
        }
        ShowVersion();
    }
    public List<List<string>> Versionitems { get; set; } = new List<List<string>>();

    public void ShowVersion()
    {
        Versionitems.Clear();
        foreach (KeyValuePair<string, List<string>> kvp in AllShowGameVersions)
        {
            List<string> valueList = kvp.Value;
            Versionitems.Add(valueList);
        }
    }
}