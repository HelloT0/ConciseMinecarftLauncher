using System;
using System.Net.Http;

namespace ConciseMinecarftLauncherWinUI3.Models;

class NetworkResources
{
    public string GetJsonFromUrl(string url)
    {
        try
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    return json;
                }
                else
                {
                    return $"Error: Failed to get JSON from {url}. Status code: {response.StatusCode}";
                }
            }
        }
        catch (HttpRequestException ex)
        {
            return $"Error: Failed to make a network request. Check your internet connection. Details: {ex.Message}";
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }
}
