using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConciseMinecarftLauncher.Models;

class LanguageandTime
{
    public string DateFormat { get; set; }
    public string TimeFormat { get; set; }
    public int TimeZone { get; set; }
    public string ChangTimeFormat(string input,string output)
    {
        //找到时分秒在input的索引值 
        int yearIndex= input.IndexOf("y");
        int monthIndex= input.IndexOf("m");
        int dayIndex= input.IndexOf("d");
        int hourIndex= input.IndexOf("h");
        int minuteIndex= input.IndexOf("m");
        int secondIndex= input.IndexOf("s");
        //获取到对应的输入数值 
        int year;
        int month;
        int day;
        int hour;
        int minute;
        int second;
        bool changeSu=int.TryParse(input.Substring(yearIndex,4),out year);
        changeSu = int.TryParse(input.Substring(monthIndex, 2), out month);
        changeSu = int.TryParse(input.Substring(dayIndex, 2), out day);
        changeSu = int.TryParse(input.Substring(hourIndex, 2), out hour);
        changeSu = int.TryParse(input.Substring(minuteIndex, 2), out minute);
        changeSu = int.TryParse(input.Substring(secondIndex, 2), out second);
        hour = hour + TimeZone;
        
        //检查小时是否正确 
        if(hour<0)
        {
            hour = 23 - Math.Abs(hour);
            day--;
        }
        else if(hour>23)
        {
            hour = hour-23;
            day++;
        }

        //检查天数是否正确 
        if (month == 2)
        {
            if (year % 4 == 0)
            {
                if (day > 29)
                {
                    day = day - 29;
                    month++;
                }
                else if (day < 1)
                {
                    day = 31 - Math.Abs(day);
                    month--;
                }
            }
            else
            {
                if (day > 28)
                {
                    day = day - 28;
                    month++;
                }
                else if (day < 1)
                {
                    day = 31 - Math.Abs(day);
                    month--;
                }

            }
        }
        else if (month==5||month==7||
            month==10||month==12)
        {
            if(day>31)
            {
                day = day - 31;
                month++;
            }
            else if(day<1)
            {
                day = 30 - Math.Abs(day);
                month--;
            }
        }
        else if(month==4||month==6||month==9||month==11)
        {
            if(day>30)
            {
                day = day - 30;
                month++;
            }
            else if(day<1)
            {
                day = 31 - Math.Abs(day);
            }
        }
        else if(month==1)
        {
            if (day > 31)
            { day = day - 31; month++; }
            else if (day < 1)
            { day = 31 - Math.Abs(day); month--; }
        }
        else if(month==3)
        {

        }
    }
}
