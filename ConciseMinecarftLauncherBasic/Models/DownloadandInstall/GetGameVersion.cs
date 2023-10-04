using Newtonsoft.Json.Linq;

namespace ConciseMinecarftLauncherBasic.Models.DownloadandInstall;

class GetGameVersion
{
    List<string> OriginalGameVersionList;//原版游戏版本信息
    public string OriginalGameJson { get; set; }
    public async Task<string> GetOriginalGameJson() //获取版本清单  
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
            string url = (string)versionToken["url"];
            string releaseTime = (string)versionToken["releasetime"];
        }
    }
}
