using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;

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
    public Dictionary<string, List<string>> AllShowGameVersions { get; set; }

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
        AllGame.Clear();
        JObject data = JsonConvert.DeserializeObject<JObject>(AllGameVersionsJson);
        JArray versionsArray = data["version"] as JArray;
        bool passtheR = true;
        while (passtheR)
        {
            if (versionsArray != null)
                passtheR = false;
            else
                await Task.Delay(500);
        }
        foreach (JObject versionObject in versionsArray)
        {
            string id = versionObject.Value<string>("id");
            string type = versionObject.Value<string>("type");
            string url = versionObject.Value<string>("url");
            string time = versionObject.Value<string>("time");
            string releaseTime = versionObject.Value<string>("releaseTime");

            try
            {
                DateTimeOffset dateTimeOffset = DateTimeOffset.ParseExact(time, "yyyy-MM-dd'T'HH:mm:ssK", CultureInfo.InvariantCulture);

                // 转换为北京时间
                DateTimeOffset beijingDateTimeOffset = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTimeOffset, "China Standard Time");

                // 将时间转换为年月日格式
                releaseTime = beijingDateTimeOffset.ToString("yyyy年MM月dd日");

            }
            catch
            {
                releaseTime = "未知";
            }
            List<string> propertiesList = new List<string>();

            if (type == "release")
            {
                propertiesList.Add($"{id},{type},{url},{time},{releaseTime},Release,gass.png");
            }
            else if (type == "snapshot" && (id.Contains("pre") || id.Contains("rc")) && id != "15w14a")
            {
                propertiesList.Add($"{id},{type},{url},{time},{releaseTime},Preview,CommandBlock.gif");
            }
            else if (type == "old_alpha")
            {
                propertiesList.Add($"{id},{type},{url},{time},{releaseTime},远古版,Store.png");
            }
            else
            {
                propertiesList.Add($"{id},{type},{url},{time},{releaseTime},愚人节版,Gold.png");
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
            else if (versionR == "AprilVersion" && showYRenJie)
                AllShowGameVersions.Add(key, valueList);
            LastShowGame();
        }
    }
    //用于储存拆分自检后信息的列表  
    public List<string> ShowGameurl { get; set; }
    public List<string> ShowGameReleaseTime { get; set; }
    public List<string> ShowGameID { get; set; }
    public List<string> ShowGameInfo { get; set; }
    public List<string> ShowGameIcon { get; set; }
    public List<string> ShowGame { get; set; }
    public int SelectGameItem {get;set;}
    public void LastShowGame()
    {
        ShowGameID.Clear();
        ShowGameurl.Clear();
        ShowGameInfo.Clear();
        ShowGameReleaseTime.Clear();
        ShowGameIcon.Clear();
        foreach (List<string> valueList in AllShowGameVersions.Values)
        {
            string data1 = valueList[0];
            string data2 = valueList[2];
            string data3 = valueList[4];
            string data4 = valueList[6];
            string data5 = valueList[7];
            ShowGameID.Add(data1);
            ShowGameurl.Add(data2);
            ShowGameReleaseTime.Add(data3);
            ShowGameInfo.Add(data4);
            ShowGameIcon.Add(data5);
            ShowGame.Add("");
        }
    }
    //选择的游戏版本  
    public string selGame { get; set; }
    public string selGameUrl { get; set; }
    public string selGamrIcon { get; set; }
    public string selGamrInfo { get; set; }
    public void ChoiceGame()
    {
        selGame = ShowGameID[SelectGameItem].ToString();
        selGameUrl = ShowGameurl[SelectGameItem].ToString();
        selGamrIcon = ShowGameIcon[SelectGameItem].ToString();
        selGamrInfo = ShowGameInfo[SelectGameItem].ToString() +
            "，发布于"+ ShowGameReleaseTime[SelectGameItem].ToString();
    }
}
