using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ConciseMinecarftLauncherBasic.Models.Downloads;

class GetGameVersion
{
    public string AllGameVersionsJson { get; set; }
    //游戏显示选择框
    public bool showRelease { get; set; } = true;
    public bool showYRenJie { get; set; } = true;
    public bool showPre { get; set; } = true;
    public bool showOld { get; set; } = false;
    public Dictionary<string, List<string>> AllGame { get; set; }
        = new Dictionary<string, List<string>>();
    public Dictionary<string,List<string>> AllShowGameVersions { get; set; }

    public async Task GetVersionJson()
    {
        var httpClient = new HttpClient();

        try
        {
            // 发送 GET 请求并获取 JSON 数据
            var jsonUrl = "https://piston-meta.mojang.com/mc/game/version_manifest.json";
            AllGameVersionsJson = await httpClient.GetStringAsync(jsonUrl);
            FixGamesJson();
        }
        catch (Exception ex)
        {
            AllGameVersionsJson = $"失败！{ex}";
        }
        finally
        {
            // 最后记得释放 HttpClient 资源
            httpClient.Dispose();
        }
        
    }
    public async Task FixGamesJson()
    {
        JObject data = JsonConvert.DeserializeObject<JObject>(AllGameVersionsJson);
        JArray versionsArray = data["version"] as JArray;
        bool passtheR = true;
        while(passtheR) 
        {
            if(versionsArray!=null)
                passtheR=false;
            else
                await Task.Delay(500);
        }
        foreach (JObject versionObject in versionsArray)
        {
            string id = versionObject.Value<string>("id");
            string type = versionObject.Value<string>("type");
            string time = versionObject.Value<string>("time");
            string releaseTime = versionObject.Value<string>("releaseTime");

            List<string> propertiesList = new List<string>();

            if (type == "release")
            {
                propertiesList.Add($"{id},{type},{time},{releaseTime},Release,gass.png");
            }
            else if (type == "snapshot" && (id.Contains("pre") || id.Contains("rc")) && id != "15w14a")
            {
                propertiesList.Add($"{id},{type},{time},{releaseTime},Preview,gassRoute.png");
            }
            else if (type == "old_alpha")
            {
                propertiesList.Add($"{id},{type},{time},{releaseTime},OldVersion,mud");
            }
            else
            {
                propertiesList.Add($"{id},{type},{time},{releaseTime},AprilVersion,mud");
            }
            string idKey;
            if (propertiesList.Count > 0)
            {
                idKey = propertiesList[0];
                AllGame.Add(idKey, propertiesList);//向字典添加键值对
            }
        }
        GetEndShowVersion();
    }
    public void GetEndShowVersion()
    {
        foreach (KeyValuePair<string, List<string>> kvp in AllGame)
        {
            string key = kvp.Key;
            List<string> valueList = kvp.Value;
            string versionR = valueList[4];
            //由于.NET版本限制，不能使用switch语句
            if (versionR == "Release" && showRelease)
                AllShowGameVersions.Add(key, valueList);
            else if (versionR == "Preview" && showPre)
                AllShowGameVersions.Add(key, valueList);
            else if (versionR == "OldVersion" && showOld)
                AllShowGameVersions.Add(key, valueList);
            else if(versionR == "AprilVersion"&&showYRenJie)
                AllShowGameVersions.Add(key,valueList);
        }
    }
}
