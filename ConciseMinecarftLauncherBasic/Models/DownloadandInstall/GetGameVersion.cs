using Newtonsoft.Json.Linq;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;

namespace ConciseMinecarftLauncherBasic.Models.DownloadandInstall;

class GetGameVersion
{
    public List<string> OriginalGameVersionList { get; set; }//原版游戏版本信息
    public string OriginalGameJson { get; set; }
    public int ChooseGameFramHeig { get; set; } =35;//Farme控件的高度 
    //游戏选择选项的数据 
    public string GameIcon { get; set; }
    public string GameName { get; set; }
    public string GameInfo { get; set; }
    public string GameR { get; set; }
    public void ShowOpChoseGame(bool open)
    {
        if (open)
            ChooseGameFramHeig = 128;
        else
            ChooseGameFramHeig = 35;
    }
    static async Task<string> GetOriginalGameJson() //获取版本清单文件  
    {
        string url =
            "https://piston-meta.mojang.com/mc/game/version_manifest.json";
        using(var httpClient=new  HttpClient()) 
        {
            try 
            {
                return await httpClient.GetStringAsync(url);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
    public async void AnalyseOriginalGAmeJson()
    {
        bool waitForGet = true;
        OriginalGameJson = await GetOriginalGameJson();
        while (waitForGet)//等待获取版本清单文件
        {
            if (OriginalGameJson != null)
                waitForGet = false;
            else
            {
                waitForGet = true;
                await Task.Delay(1000);
            }
        }
        JObject jsonObj = JObject.Parse(OriginalGameJson);
        JArray versionsArray = (JArray)jsonObj["version"];
        waitForGet = true;
        while(waitForGet)//等待是否已解析完JSON文件 
        {
            if (jsonObj != null && versionsArray != null)
                waitForGet = false;
            else
            {
                waitForGet = true;
                await Task.Delay(500);
            }
        }
        foreach(JToken versionToken in versionsArray)
        {
            string id = (string)versionToken["id"];
            string type = (string)versionToken["type"];
            string url = (string)versionToken["url"];
            string releaseTime = (string)versionToken["releaseTime"];
            OriginalGameVersionList.Add("");
            OriginalGameVersionList.Add(id);
            OriginalGameVersionList.Add(type);
            OriginalGameVersionList.Add(url);
            OriginalGameVersionList.Add(releaseTime);
        }
    }
    public void ShowInfoOrigGame()
    {
        for (int i = 0; i < Convert.ToInt32(OriginalGameVersionList.Count); i = i + 5)
        {
            GameName = OriginalGameVersionList[i+1];
            GameR = OriginalGameVersionList[i + 2];
            if (GameR == "release")
            {
                GameIcon = "grass.png";
            }
            else if (GameR == "snapshot" && Regex.IsMatch(GameName, @"^\d+[a-zA-Z]\d+[a-zA-Z]$")
                || GameName.Contains("-rc") || GameName.Contains("pre"))
            { GameIcon = "commandblock.gif"; }
            else if (GameR == "old_alpha")
            {
                GameIcon = "cobblestone.png";
                GameInfo = "";
            }
            else
            {
                GameIcon = "gold.png";

            }
        }
    }
}
