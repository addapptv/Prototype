using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepSystem : MonoBehaviour
{
    //TO DO
        // Rotate player 180 degs after sleeping in home door 
        // Add partial restore energy based on energymanager 

    public GameObject UIControls;

   public void StartSleep()
    {
        StartCoroutine(ResetEnergy()); 
        UIControls.GetComponent<ScreenFader>().ScreenFadeOutIn();
    }

    public void StartRest()
    {
        StartCoroutine(RestoreEnergy());
        UIControls.GetComponent<ScreenFader>().ScreenFadeOutIn();
    }

    IEnumerator ResetEnergy()
    {
        yield return new WaitForSeconds(1);
        GetComponent<EnergyManager>().ResetEnergy();
    }

    IEnumerator RestoreEnergy()
    {
        yield return new WaitForSeconds(1);
        GetComponent<EnergyManager>().RestoreEnergy();
    }
}
