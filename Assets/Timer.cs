using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public static double timer;  // in minutes
    public Text timerText;
    public static double timeSpeed;
	// Use this for initialization
	void Start () {
        timer = 0;
        timeSpeed = 60;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime * timeSpeed;

        string prettyTime = FormatTime((float)timer);
        timerText.text = prettyTime;
    }

    public static string FormatTime(float t) {
        // Figure out what year it is
        int hours = Mathf.FloorToInt(t / 60F);
        int minutes = Mathf.FloorToInt(t - (hours * 60));
        int days = Mathf.FloorToInt(hours / 24F);
        int years = TimePicker.startYear;
        // Remember that at this point days indexes from 0
        while (days > 365)
        {
            if (years % 4 == 0 )
            {
                days -= 366;
            }
            else
            {
                days -= 365;
            }
            years += 1;
        }
        bool leapYear = (years % 4 == 0);   // leap year is a pain in the buns
        if (days == 365)
        {
            // Either last day of a leap year or first day of a non-leap year
            if (!leapYear)
            {
                days -= 365;
                years += 1;
            }
        }
        int leapYearOffset = 0;
        if (leapYear)
        {
            leapYearOffset = 1;
        }

        string month = "Month Not Found";

        if (days < 31)
        {
            // January
            month = "January";
        } else if (days < 59+leapYearOffset)
        {
            // February
            month = "February";
            days = days - 31;
        } else if (days < 90+leapYearOffset)
        {
            // March
            month = "March";
            days = days - 59 - leapYearOffset;
        }
        else if (days < 120+leapYearOffset)
        {
            // April
            month = "April";
            days = days - 90 - leapYearOffset;
        } else if (days < 151+leapYearOffset)
        {
            // May
            month = "May";
            days = days - 120 - leapYearOffset;
        } else if (days < 181+leapYearOffset)
        {
            // June
            month = "June";
            days = days - 151 - leapYearOffset;
        } else if (days < 212+leapYearOffset)
        {
            // July
            month = "July";
            days = days - 181 - leapYearOffset;
        } else if (days < 243+leapYearOffset)
        {
            // August
            month = "August";
            days = days - 212 - leapYearOffset;
        } else if (days < 273+leapYearOffset)
        {
            // September
            month = "September";
            days = days - 243 - leapYearOffset;
        } else if (days < 304+leapYearOffset)
        {
            // October
            month = "October";
            days = days - 273 - leapYearOffset;
        } else if (days < 334+leapYearOffset)
        {
            // November
            month = "November";
            days = days - 304 - leapYearOffset;
        } else if (days < 365+leapYearOffset)
        {
            // December
            month = "December";
            days = days - 334 - leapYearOffset;
        }
        days++; // Days starts from zero, want to display from 1
        hours = hours % 24;
        string prettyTime = string.Format("{0:00}:{1:00}:00 {2} {3}, {4}", hours, minutes, month, days, years);

        return prettyTime;
    }

    public static string GetYear(float t)
    {
        string month = "";
        int hours = Mathf.FloorToInt(t / 60F);
        int days = Mathf.FloorToInt(hours / 24F);
        int years = TimePicker.startYear;

        while (days > 365)
        {
            if (years % 4 == 0)
            {
                days -= 366;
            }
            else
            {
                days -= 365;
            }
            years += 1;
        }
        bool leapYear = (years % 4 == 0);   // leap year is a pain in the buns
        if (days == 365)
        {
            // Either last day of a leap year or first day of a non-leap year
            if (!leapYear)
            {
                days -= 365;
                years += 1;
            }
        }

        int leapYearOffset = 0;
        if (leapYear)
        {
            leapYearOffset = 1;
        }
        if (days < 31)
        {
            // January
            month = "0";
        }
        else if (days < 59 + leapYearOffset)
        {
            // February
            month = "1";
        }
        else if (days < 90 + leapYearOffset)
        {
            // March
            month = "2";
        }
        else if (days < 120 + leapYearOffset)
        {
            // April
            month = "3";
        }
        else if (days < 151 + leapYearOffset)
        {
            // May
            month = "4";
        }
        else if (days < 181 + leapYearOffset)
        {
            // June
            month = "5";
        }
        else if (days < 212 + leapYearOffset)
        {
            // July
            month = "6";
        }
        else if (days < 243 + leapYearOffset)
        {
            // August
            month = "7";
        }
        else if (days < 273 + leapYearOffset)
        {
            // September
            month = "8";
        }
        else if (days < 304 + leapYearOffset)
        {
            // October
            month = "9";
        }
        else if (days < 334 + leapYearOffset)
        {
            // November
            month = "10";
        }
        else if (days < 365 + leapYearOffset)
        {
            // December
            month = "11";
        }
        return years.ToString() + month;
    }
}
