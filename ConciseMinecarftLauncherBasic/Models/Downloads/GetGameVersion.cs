using Newtonsoft.Json;
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
    string AllGameVersions;
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
            AllGameVersions = await httpClient.GetStringAsync(jsonUrl);
        }
        catch (Exception ex)
        {
            AllGameVersions = $"失败！{ex}";
        }
        finally
        {
            // 最后记得释放 HttpClient 资源
            httpClient.Dispose();
        }

    }
    public void FixGamesJson()
    {

    }
}
