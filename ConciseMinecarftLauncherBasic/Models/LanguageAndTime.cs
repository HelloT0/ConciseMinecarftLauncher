
namespace ConciseMinecarftLauncherBasic.Models;

class LanguageAndTime
{
    static string ChangeTimeFormat(string input,string format, string output)
    {
        DateTime parsedTime;
        if (DateTime.TryParseExact(input, format, null, System.Globalization.DateTimeStyles.None, out parsedTime))
        {
            return parsedTime.ToString(output);
        }
        else
        {
            return "";
        }
    }
}
