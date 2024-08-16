using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunSystem : MonoBehaviour
{
    //TO DO
    // Change sun on change location/rest/save/load or reduced interval not constant update
    // Light intensity season multiplier - lower in winter
    // Add time of day description - Dawn, Morning, Midday, Afternoon, Evening, Dusk, Night
    // Change sun settings based on time of day - color of light vs day phase
    // Change fog colour with time of day - black at night moving to grey and white as day progresses

    public Light sun;
    public TimeSystem timeSystem;

    [Header("Time settings")]

    [Range(0f, 24f)]
    public float timeOfDay;

    [Range(0.0f, 1.0f)]
    private float dayAlpha;
    [Range(0.0f, 1.0f)]
    private float nightAlpha;

    private float dayInYear;
    [Range(0.0f, 1.0f)]
    private float dayInYearMultiplier;

    public AnimationCurve processionCurve;

    [Header("Day settings")]

    private bool isNight;
    public float winterSolsticeSunrise = 8f;
    public float winterSolsticeSunset = 16f;    
    public float summerSolsticeSunrise = 5f;
    public float summerSolsticeSunset = 21f;

    private float sunriseTime;
    private float sunsetTime;
    private float dayLength;
    private float nightLength;

    [Header("Sun position settings")]

    private float sunHeight;
    public float nightMultiplier;

    public float winterSolsticeAngle = 20;
    public float summerSolsticeAngle = 60;

    public AnimationCurve sunHeightCurve;

    private Vector3 sunPosition;

    [Header("Environment settings")]

    public Gradient sunColor;
    public float sunIntensity;
    public AnimationCurve ambientIntensity;
    public Gradient fogColor;

    void Start()
    {

        Invoke("SetTime", 0.01f);

    }

    void Update()
    {

        ProgressTime();
        CheckDayNight();
        CalculateDayNightAlpha();
        CalculateSunHeight();
        ChangeSun();

    }

    private void SetTime()
    {
        dayInYear = timeSystem.dayInYear;
        timeOfDay = timeSystem.timeOfDay;
        dayInYearMultiplier = dayInYear / 365;

        if (timeOfDay > sunsetTime || timeOfDay < sunriseTime)
        {
            isNight = true;
        }
        else
        {
            isNight = false;
        }

        StartNewDay();
    }

    public void StartNewDay()
    {
        sunriseTime = winterSolsticeSunrise - ((winterSolsticeSunrise - summerSolsticeSunrise) * processionCurve.Evaluate(dayInYearMultiplier));
        sunsetTime = winterSolsticeSunset + ((summerSolsticeSunset - winterSolsticeSunset) * processionCurve.Evaluate(dayInYearMultiplier));

        dayLength = sunsetTime - sunriseTime;
        nightLength = 24.0f - dayLength;
    }

    private void ProgressTime()
    {

        timeOfDay = timeSystem.timeOfDay;

        if (timeOfDay >= 24.0f)
        {
            StartNewDay();
        }

    }

    private void CheckDayNight()
    {
        if (timeOfDay > sunriseTime && timeOfDay < sunsetTime)
        {
            isNight = false;
        }
        else
        {
            isNight = true;
        }
    }

    private void CalculateDayNightAlpha()
    {
        if (isNight)
        {
            dayAlpha = 0f;
            if (timeOfDay > sunsetTime)
            {
                nightAlpha = (timeOfDay - sunsetTime) / nightLength;
            }
            else
            {
                nightAlpha = ((24 - sunsetTime) + timeOfDay) / nightLength;
            }
        }
        else
        {
            nightAlpha = 0f;
            dayAlpha = (timeOfDay-sunriseTime) / dayLength;
        }
    }

    private void CalculateSunHeight()
    {
        float sunMaxHeight = winterSolsticeAngle + ((summerSolsticeAngle - winterSolsticeAngle) * processionCurve.Evaluate(dayInYearMultiplier));

        if (isNight)
        {
            sunHeight = (-sunMaxHeight * sunHeightCurve.Evaluate(nightAlpha)) * nightMultiplier;
        }
        else
        {
            sunHeight = sunMaxHeight * sunHeightCurve.Evaluate(dayAlpha);
        }

        if (sunHeight < -30f)
        {
            sunHeight = -30f;
        }
    }

    private void ChangeSun()
    {
        //Sun position
        if (isNight)
        {
            sunPosition.y = 270 + (180 * nightAlpha);
        }
        else
        {
            sunPosition.y = 90 + (180 * dayAlpha);
        }

        nightMultiplier = 1 + (4 * (1 - processionCurve.Evaluate(dayInYearMultiplier)));
        sunPosition.x = sunHeight;
        sunPosition.z = 0;

        sun.transform.rotation = Quaternion.Euler(sunPosition.x, sunPosition.y, sunPosition.z);

        //Sunlight settings
        sun.intensity = sunIntensity;
        sun.color = sunColor.Evaluate(dayAlpha);
        RenderSettings.ambientIntensity = ambientIntensity.Evaluate(dayAlpha);
        RenderSettings.reflectionIntensity = ambientIntensity.Evaluate(dayAlpha);
        RenderSettings.fogColor = fogColor.Evaluate(dayAlpha);
    }
    
    //TO DO - Day, dusk, dawn settings 

    //private void StartDay()
    //{
        //sun.shadows = LightShadows.Soft;
        //moon.shadows = LightShadows.None;
    //}

    //private void StartNight()
    //{
        //moon.shadows = LightShadows.Soft;
        //sun.shadows = LightShadows.None;
    //}
}
