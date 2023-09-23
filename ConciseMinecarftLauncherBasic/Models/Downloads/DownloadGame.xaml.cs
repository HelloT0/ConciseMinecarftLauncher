using Newtonsoft.Json;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ConciseMinecarftLauncherBasic.Models.Downloads;

public partial class DownloadGame : ContentPage
{
    string AllGameVersions;
    //��Ϸ��ʾѡ���
    public bool showRelease { get; set; } = true;
    public bool showSnapshot { get; set; } = true;
    public bool showYRenJie { get; set; } = true;
    public bool showPre { get; set; } = true;
    public bool showOld { get; set; } = false;

    public Dictionary<string, List<string>> AllShowGame { get; set; }
        = new Dictionary<string, List<string>>();

    public DownloadGame()
	{
		InitializeComponent();
        GetVersionJson();
	}
    public async void GetVersionJson()
    {
        var httpClient = new HttpClient();

        try
        {
            // ���� GET ���󲢻�ȡ JSON ����
            var jsonUrl = "https://piston-meta.mojang.com/mc/game/version_manifest.json";
            AllGameVersions = await httpClient.GetStringAsync(jsonUrl);
        }
        catch (Exception ex)
        {
            AllGameVersions = $"ʧ�ܣ�{ex}";
        }
        finally
        {
            // ���ǵ��ͷ� HttpClient ��Դ
            httpClient.Dispose();
        }
        Tips(AllGameVersions);
        
    }
    public async void Tips(string dispay)
    {
        await DisplayAlert("��ʾ", dispay, "ȷ��");
    }
    public void FxiGameJson()
    {
        var document = JsonDocument.Parse(AllGameVersions);
        if (showRelease)
            AllRelease();
        if (showSnapshot)
            AllSanpshot();
        if (showOld)
            AllOldGame();
        if (showPre)
            AllPreVersion();
        void AllRelease()
        {
            dynamic jObject = JsonConvert.DeserializeObject(AllGameVersions);
            // ����version�����е����ж��󣬲�����Ϣ��ӵ������б���

            List<string> everInfo = new List<string>();
            foreach (var item in jObject.version)
            {
                if (item["type"]=="release")
                {
                    string id = item["id".ToString()];
                    everInfo.Add(item["id".ToString()]);
                    everInfo.Add(item["type".ToString()]);
                    everInfo.Add(item["url"].ToString());
                    everInfo.Add(item["time".ToString()]);
                    everInfo.Add(item["releaseTime".ToString()]);
                    AllShowGame.Add(id,everInfo); 
                }
                everInfo.Clear();
            }
        }
        void AllSanpshot()
        {
            dynamic jObject = JsonConvert.DeserializeObject(AllGameVersions);
            // ����version�����е����ж��󣬲�����Ϣ��ӵ������б���
            List<string> everInfo = new List<string>();
            foreach (var item in jObject.version)
            {
                string Checkid = item["id".ToString()];
                if (item["type"] == "sanpshot"
                    &&Regex.IsMatch(Checkid, @"^\d+w\d+a$") && Checkid != "15w14a")
                {
                    string id = item["id".ToString()];
                    everInfo.Add(item["id".ToString()]);
                    everInfo.Add(item["type".ToString()]);
                    everInfo.Add(item["url"].ToString());
                    everInfo.Add(item["time".ToString()]);
                    everInfo.Add(item["releaseTime".ToString()]);
                    AllShowGame.Add(id, everInfo);
                }
                Checkid = "";
                everInfo.Clear();
            }

        }
        void AllPreVersion()
        {
            dynamic jObject = JsonConvert.DeserializeObject(AllGameVersions);
            // ����version�����е����ж��󣬲�����Ϣ��ӵ������б���
            List<string> everInfo = new List<string>();
            foreach (var item in jObject.version)
            {
                string Checkid = item["id".ToString()];
                if (item["type"] == "sanpshot"&&Checkid.Contains("pre")||Checkid.Contains("rc"))
                {
                    string id = item["id".ToString()];
                    everInfo.Add(item["id".ToString()]);
                    everInfo.Add(item["type".ToString()]);
                    everInfo.Add(item["url"].ToString());
                    everInfo.Add(item["time".ToString()]);
                    everInfo.Add(item["releaseTime".ToString()]);
                    AllShowGame.Add(id, everInfo);
                }
                Checkid = "";
                everInfo.Clear();

            }
        }
        void AllOldGame()
        {
            {
                dynamic jObject = JsonConvert.DeserializeObject(AllGameVersions);
                // ����version�����е����ж��󣬲�����Ϣ��ӵ������б���
                List<string> everInfo = new List<string>();
                foreach (var item in jObject.version)
                {
                    string Checkid = item["id".ToString()];
                    if (item["type"] == "old_alpha")
                    {
                        string id = item["id".ToString()];
                        everInfo.Add(item["id".ToString()]);
                        everInfo.Add(item["type".ToString()]);
                        everInfo.Add(item["url"].ToString());
                        everInfo.Add(item["time".ToString()]);
                        everInfo.Add(item["releaseTime".ToString()]);
                        AllShowGame.Add(id, everInfo);
                    }
                    Checkid = "";
                    everInfo.Clear();
                }

            }
        }
    }

    public void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        FxiGameJson();
    }
}