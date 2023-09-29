using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConciseMinecarftLauncherBasic.Models.Downloads;

class GetGameVersion
{
    string AllGameVersionsJson;
    //游戏显示选择框
    public bool showRelease { get; set; } = true;
    public bool showSnapshot { get; set; } = true;
    public bool showYRenJie { get; set; } = true;
    public bool showPre { get; set; } = true;
    public bool showOld { get; set; } = false;

    public Dictionary<string, List<string>> AllShowGame { get; set; }
        = new Dictionary<string, List<string>>();

    public async void GetVersionJson()
    {
        var httpClient = new HttpClient();

        try
        {
            // 发送 GET 请求并获取 JSON 数据
            var jsonUrl = "https://piston-meta.mojang.com/mc/game/version_manifest.json";
            AllGameVersionsJson = await httpClient.GetStringAsync(jsonUrl);
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
        FixGamesJson();
    }
    public void FixGamesJson()
    {
        JObject data = JsonConvert.DeserializeObject<JObject>(AllGameVersionsJson); // 将字符串反序列化为 JObject 对象
        Dictionary<string, List<string[]>> resultDict = new Dictionary<string, List<string[]>>(); // 创建结果字典

        List<string[]> resultList = new List<string[]>(); // 创建结果列表

        if (data.TryGetValue("versions", out JToken versions) && versions is JArray versionArray)
        {
            foreach (JObject versionObject in versionArray)
            {
                string id = versionObject.Value<string>("id");
                string type = versionObject.Value<string>("type");
                string url = versionObject.Value<string>("url");
                string time = versionObject.Value<string>("time");
                string releaseTime = versionObject.Value<string>("releaseTime");

                string[] resultItem = null; // 在此处将 resultItem 初始化为 null

                if (type == "release" && showRelease)
                {
                    resultItem = new string[] { id, url, time, releaseTime, "正式版" };
                }
                else if (type == "snapshot" && Regex.IsMatch(id, "^w\\d+a$") && showSnapshot)
                {
                    resultItem = new string[] { id, url, time, releaseTime, "快照版" };
                }
                else if (type == "snapshot" && (id.Contains("pre") || id.Contains("rc")) && showPre) // 需要用括号将两个条件括起来
                {
                    resultItem = new string[] { id, url, time, releaseTime, "测试版" };
                }
                else if (showYRenJie)
                {
                    resultItem = new string[] { id, url, time, releaseTime, "愚人节版" };
                }

                resultList.Add(resultItem);

                // 判断字典中是否已存在以id为键的列表，如果不存在则创建新列表
                if (!resultDict.ContainsKey(id))
                {
                    resultDict[id] = new List<string[]>();
                }

                // 将当前列表添加到字典中对应的键值，并清空当前列表
                resultDict[id].AddRange(resultList);
                resultList.Clear();
            }
        }
    }
}
