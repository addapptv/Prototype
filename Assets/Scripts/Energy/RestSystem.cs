using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestSystem : MonoBehaviour
{
    //TO DO
    // Rotate player 180 degs after sleeping in home door 
    // Add partial restore energy based on energymanager
    // Add first person camera view on rest spots - can look around in 180 degree field

    public GameObject UI;
    public TimeSystem timeSystem;
    public float campEffect = 0.8f;
    public float restEffect = 0.2f;
    public float restMins = 30;
    public float wakeHour = 6;
    public float wakeMinute = 30;

    public void StartSleep()
    {
        UI.GetComponent<ScreenFader>().ScreenFadeOutIn();
        StartCoroutine(SleepEffect());
    }

    public void StartCamp()
    {
        UI.GetComponent<ScreenFader>().ScreenFadeOutIn();
        GetComponent<EnergyManager>().restoreEnergyEffect = campEffect;
        StartCoroutine(SleepEffect());
    }

    public void StartRest()
    {
        UI.GetComponent<ScreenFader>().ScreenFadeOutIn();
        GetComponent<EnergyManager>().restoreEnergyEffect = restEffect;
        StartCoroutine(RestEffect());
    }

    private IEnumerator SleepEffect()
    {
        yield return new WaitForSeconds(1);
        GetComponent<EnergyManager>().ResetEnergy();
        timeSystem.day++;
        timeSystem.hour = wakeHour;
        timeSystem.minute = wakeMinute;
    }

    private IEnumerator RestEffect()
    {
        yield return new WaitForSeconds(1);
        GetComponent<EnergyManager>().RestoreEnergy();
        if ((timeSystem.minute + restMins) >= 60)
        {
            timeSystem.hour++;
            timeSystem.minute = (timeSystem.minute + restMins) - 60;
        }
        else
        {
            timeSystem.minute = timeSystem.minute + restMins;
        }
    }
}
