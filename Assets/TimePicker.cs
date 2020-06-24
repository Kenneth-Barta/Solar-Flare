using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimePicker : MonoBehaviour {
    public Dropdown monthPicker, yearPicker;
    public InputField dayPicker;
    public Button confirm;
    public static int startYear = 1975;
	// Use this for initialization
	void Start () {
        confirm.onClick.AddListener(SelectTime);
	}
	
	// Update is called once per frame
	void Update () {

	}

    void SelectTime()
    {
        int month = monthPicker.value;
        int year = int.Parse(yearPicker.options[yearPicker.value].text);
        int day = int.Parse(dayPicker.text);

        float newTime = MDYToFloatTime(month, day, year);

        Timer.timer = FindNearestFlareTime(year.ToString() + month.ToString(), newTime) - 5;
    }

    public static float MDYToFloatTime(int month, int day, int year)
    {
        int secondsInYear = 31536000;
        int secondsInDay = 86400;
        day -= 1;  // Index days from 0, but passed in from 1

        float newTime = 0;
        // Add month to time
        for (int i = 0; i < month; i++)
        {
            switch (i)
            {
                case 0:
                    newTime += 31 * secondsInDay;
                    break;
                case 1:
                    newTime += 28 * secondsInDay;   // Leap year accounted for below
                    break;
                case 2:
                    newTime += 31 * secondsInDay;
                    break;
                case 3:
                    newTime += 30 * secondsInDay;
                    break;
                case 4:
                    newTime += 31 * secondsInDay;
                    break;
                case 5:
                    newTime += 30 * secondsInDay;
                    break;
                case 6:
                    newTime += 31 * secondsInDay;
                    break;
                case 7:
                    newTime += 31 * secondsInDay;
                    break;
                case 8:
                    newTime += 30 * secondsInDay;
                    break;
                case 9:
                    newTime += 31 * secondsInDay;
                    break;
                case 10:
                    newTime += 30 * secondsInDay;
                    break;
                case 11:
                    // i should never get here
                    Debug.Log("oops");
                    newTime += 31 * secondsInDay;
                    break;
            }
        }

        // Add day to time
        newTime += day * secondsInDay;

        // Add year to time
        newTime += (year - startYear) * secondsInYear;

        // Account for leap years
        newTime += LeapYearsSince(year) * secondsInDay; // This many extra days
        if (month > 1 && year % 4 == 0)
        {
            // After Feb, if this year is a leap year, add an extra day.  Else don't
            newTime += secondsInDay;
        }

        return newTime/60f;     // newTime in seconds, want in minutes
    }

    private static int LeapYearsSince(int year)
    {
        int counter = 0;
        for (int i = startYear; i < year; i++)
        {
            if (i % 4 == 0)
            {
                // Technically should check if year divisible by 100 and not 400 as well but that isn't relevant for our dataset.
                counter += 1;
            }
        }
        return counter;
    }

    private static float FindNearestFlareTime(string year, float t)
    {
        for (int i = 0; i < FlareDetail.flares[year].Count; i++)
        {
            FlareDetail.Flare flare = FlareDetail.flares[year][i];
            float flareTime = MDYToFloatTime(flare.month - 1, flare.day, flare.year);
            float flareMinutes = flare.startTime % 100;     // 0000 time format
            float flareHours = (flare.startTime - flareMinutes) / 100;
            flareTime += flareMinutes;
            flareTime += flareHours * 60;
            if (flareTime >= t)
            {
                // This is closest flare in future since flares are sorted
                return flareTime;
            }
            // Else keep searching
        }
        // This shouldn't happen often but if it does just spit back t
        Debug.Log("No flare found");
        return t;
    }
}
