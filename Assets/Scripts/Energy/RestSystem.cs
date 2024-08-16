using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestSystem : MonoBehaviour
{
    //TO DO
    // Use events to change energy level and invoke screen fade UI
    // Rotate player 180 degs after sleeping in home door 
    // Add partial restore energy based on energymanager
    // Add first person camera view on rest spots - can look around in 180 degree field
    // Randomise wake hour and minute within % range of wake hour

    public GameObject UIManager;
    public TimeSystem timeSystem;
    public EnergyManager energyManager;
    public int wakeHour = 6;
    public int wakeMinute = 30;

    private void OnEnable()
    {
        GameEventsManager.instance.restEvents.OnStartRest += StartRest;
        GameEventsManager.instance.restEvents.OnStartCamp += StartCamp;
        GameEventsManager.instance.restEvents.OnStartSleep += StartSleep;

    }

    private void OnDisable()
    {
        GameEventsManager.instance.restEvents.OnStartRest -= StartRest;
        GameEventsManager.instance.restEvents.OnStartCamp -= StartCamp;
        GameEventsManager.instance.restEvents.OnStartSleep -= StartSleep;

    }

    public void StartSleep()
    {
        UIManager.GetComponent<ScreenFader>().ScreenFadeOutIn();
        DataPersistenceManager.instance.SaveGame();
        StartCoroutine(SleepEffect());
    }

    public void StartCamp(float energyRestoreEffect)
    {
        Debug.Log("Camp started");
        UIManager.GetComponent<ScreenFader>().ScreenFadeOutIn();
/*        GetComponent<EnergyManager>().restoreEnergyEffect = energyRestoreEffect;*/
        StartCoroutine(CampEffect(energyRestoreEffect));
    }

    public void StartRest(int restMins, float energyRestoreEffect)
    {
        Debug.Log("Rest started");
        UIManager.GetComponent<ScreenFader>().ScreenFadeOutIn();
/*        GetComponent<EnergyManager>().restoreEnergyEffect = energyRestoreEffect;*/
        StartCoroutine(RestEffect(restMins, energyRestoreEffect));
    }

    private IEnumerator SleepEffect()
    {
        yield return new WaitForSeconds(1);
        energyManager.ResetEnergy();
        timeSystem.day++;
        timeSystem.hour = wakeHour;
        timeSystem.minute = wakeMinute;
    }

    private IEnumerator CampEffect(float energyRestoreEffect)
    {
        yield return new WaitForSeconds(1);
        EnergyManager.instance.RestoreEnergy(energyRestoreEffect);
        timeSystem.day++;
        timeSystem.hour = wakeHour;
        timeSystem.minute = wakeMinute;
    }

    private IEnumerator RestEffect(int restMins, float energyRestoreEffect)
    {
        yield return new WaitForSeconds(1);
        EnergyManager.instance.RestoreEnergy(energyRestoreEffect);
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
