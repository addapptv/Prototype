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
    

    float currentEnergy = 100.9f;
    float maxEnergy = 100.9f;
    float energyMultiplier = 1f;
    [SerializeField]
    float moveBurnRate = 0.5f;
    [SerializeField]
    float stillBurnRate= 0.05f;
    [SerializeField]
    float restoreEnergyEffect = 0.2f;

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
            UseEnergyStill();
        }

    }

    public void UseEnergyOnMove()
    {
        currentEnergy -= moveBurnRate * energyMultiplier * Time.deltaTime;
    }

    public void UseEnergyStill()
    {
        currentEnergy -= stillBurnRate * energyMultiplier * Time.deltaTime;
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
