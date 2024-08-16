using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeSystem : MonoBehaviour
{
    //TO DO
    // Add language based date i.e. "1st November"
   
    [Header("Time settings")]

    public bool progressTime = true;

    public float gameDayInMins = 0.5f;
    private float timeMultiplier;
    [Range(0f, 24f)]
    public float timeOfDay;
    public float dayInYear;

    [Header("Start settings")]

    public int startYear = 1;
    public int startMonth = 1;
    public int startDay = 1;
    public int startHour = 0;
    public int startMinute = 1;

    [Header("UI components")]

    public TextMeshProUGUI clockText;
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI yearText;
    public TextMeshProUGUI seasonText;

    [HideInInspector]
    public float minute, hour, day, second, month, year;
    [HideInInspector]
    public string season;

    void Start()
    {
        timeMultiplier = 4400 / gameDayInMins;

        hour = startHour;
        minute = startMinute;
        day = startDay;
        month = startMonth;
        year = startYear;
        timeOfDay = startHour + (startMinute / 60);
        
        CalculateDayInYear();
        CalculateSeason();
    }

    void Update()
    {
        CalculateTime();
        CalculateDayInYear();
    }

    void CalculateTime()
    {

        if (progressTime)
        {
            second += Time.deltaTime * timeMultiplier;
        }

        if (second >= 60)
        {
            minute++;
            second = 0;
            CallText();
        }
        else if (minute >= 60)
        {
            hour++;
            minute = 0;
            CallText();
        }
        else if (hour >= 24)
        {
            day++;
            hour = 0;
            CallText();

        }
        else if (day >= 28)
        {
            CalculateMonth();
        }
        else if (month > 12)
        {
            year++;
            month = 1;
            CalculateSeason();
            CallText();
        }

        timeOfDay = hour + (minute / 60) + (second / 3600);
        dayInYear = day;


    }

    void CalculateMonth()
    {
        if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
        {
            if (day >= 32)
            {
                month++;
                day = 1;
                CalculateSeason();
                CallText();
            }
        }

        if (month == 4 || month == 6 || month == 9 || month == 11)
        {
            if (day >= 31)
            {
                month++;
                day = 1;
                CalculateSeason();
                CallText();
            }
        }

        if (month == 2)
        {
            if (day >= 30)
            {
                month++;
                day = 1;
                CalculateSeason();
                CallText();
            }
        }

    }

    void CalculateDayInYear()
    {
        if (month == 1)
        {
            dayInYear = day;
        }
        else if (month == 2)
        {
            dayInYear = 31 + day;
        }
        else if (month == 3)
        {
            dayInYear = 59 + day;
        }
        else if (month == 4)
        {
            dayInYear = 90 + day;
        }
        else if (month == 5)
        {
            dayInYear = 120 + day;
        }
        else if (month == 6)
        {
            dayInYear = 151 + day;
        }
        else if (month == 7)
        {
            dayInYear = 181 + day;
        }
        else if (month == 8)
        {
            dayInYear = 212 + day;
        }
        else if (month == 9)
        {
            dayInYear = 243 + day;
        }
        else if (month == 10)
        {
            dayInYear = 273 + day;
        }
        else if (month == 11)
        {
            dayInYear = 304 + day;
        }
        else if (month == 12)
        {
            dayInYear = 334 + day;
        }

    }

    void CalculateSeason()
    {
        if (month == 12 || month == 1 || month == 2)
        {
            season = "Winter";
        }

        if (month == 3 || month == 4 || month == 5)
        {
            season = "Spring";
        }

        if (month == 6 || month == 7 || month == 8)
        {
            season = "Summer";
        }

        if (month == 9 || month == 10 || month == 11)
        {
            season = "Autumn";
        }
    }

    void CallText()
    {
        dateText.text = string.Format("{0:00}/{1:00}", day, month);
        clockText.text = string.Format("{0:00}:{1:00}", hour, minute);
        yearText.text = "Year " + year;
        seasonText.text = season;
    }

}
