using ConciseMinecarftLauncherWinUI3.Pages.DownInstall;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConciseMinecarftLauncherWinUI3.Models.DwnInsall;

class DownGame
{
    public void GetOfficial()
    {
        NetworkResources networkResources = new();
        string json = networkResources.GetJsonFromUrl(
            "https://piston-meta.mojang.com/mc/game/version_manifest.json");

        DownloadGame downloadGame = new();

        var jsonObject = JsonConvert.DeserializeObject<dynamic>(json);
        var jsonArray = jsonObject["versions"];

        foreach (var item in jsonArray)
        {
            string id = item["id"];
            string type = item["type"];
            string url = item["url"];
            string time = item["releaseTime"];
            string launcherOfType;
            if (type == "release")
                launcherOfType = "release";
            else if (type == "snapshot" &&
                id.Contains("rc") || id.Contains("pre") ||
                System.Text.RegularExpressions.Regex.IsMatch(id, @"\d\d\w\d\d\w"))
                launcherOfType = "preversion";
            else if (type == "old_alpha")
                launcherOfType = "old";
            else
                launcherOfType = "noture";
            downloadGame.OfficalVer.Add(launcherOfType);
            downloadGame.OfficalVer.Add(id);
            downloadGame.OfficalVer.Add(type);
            downloadGame.OfficalVer.Add(url);
            downloadGame.OfficalVer.Add(time);
        }
    }
}
