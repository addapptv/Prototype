using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{

    //TO DO
        // Limit restore to simgle time or reducing effect but limit to only a percetage of max based on how much burned before rest
        // Build out matrix of multipliers for each energy impact and different states
        // Add environmental factors impact (day/night, wet, hot, cold etc.)
        // Add gear perks/sweats
        // Add impact of low energy on e.g. moveSpeed @ 20% etc
        // Print messages for status @ 20%/10% etc "Huuuuhaaaa... I'm tired", "My eyes are closing, I'm shattered" etc
    

    public float currentEnergy = 100.9f;
    public float maxEnergy = 100.9f;
    public float energyMultiplier = 1f;
    [SerializeField]
    readonly float movingBurnRate = 0.5f;
    [SerializeField]
    readonly float stationaryBurnRate= 0.05f;
    [SerializeField]
    public float restoreEnergyEffect;

    public float Energy
    {
        get { return currentEnergy; }
        set { currentEnergy = value; }
    }

    private void Update()
    {
        if (GetComponent<PlayerAttribs>().isMoving)
        {
            UseEnergyOnMove();
        }
        else
        {
            UseEnergyWhenStationary();
        }

    }

    public void UseEnergyOnMove()
    {
        currentEnergy -= movingBurnRate * energyMultiplier * Time.deltaTime;
    }

    public void UseEnergyWhenStationary()
    {
        currentEnergy -= stationaryBurnRate * energyMultiplier * Time.deltaTime;
    }

    public void RestoreEnergy()
    {
        currentEnergy += (maxEnergy - currentEnergy) * restoreEnergyEffect;
        
    }

    public void ResetEnergy()
    {
        currentEnergy = 100.9f;
    }

}
