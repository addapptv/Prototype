using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour, IDataPersistence
{

    //TO DO
    // Move energy manager to separate gameobjectg and use events
    // Limit restore to simgle time or reducing effect but limit to only a percetage of max based on how much burned before rest
    // Build out matrix of multipliers for each energy impact and different states
    // Add environmental factors impact (day/night, wet, hot, cold etc.)
    // Add gear perks/sweats
    // Add impact of low energy on e.g. moveSpeed @ 20% etc
    // Print messages for status @ 20%/10% etc "Huuuuhaaaa... I'm tired", "My eyes are closing, I'm shattered" etc

    public static EnergyManager instance { get; private set; }

    public float currentEnergy = 100.9f;
    private float maxEnergy = 100.9f;
    private float energyMultiplier = 1f;
    private float movingBurnRate = 0.5f;
    private float stationaryBurnRate= 0.05f;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Energy Manager in the scene.");
        }
        instance = this;
    }

    private void Update()
    {
        if (PlayerAttribs.instance.isMoving)
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

    public void RestoreEnergy(float restoreEnergyEffect)
    {
        currentEnergy += (maxEnergy - currentEnergy) * restoreEnergyEffect;
        
    }

    public void ResetEnergy()
    {
        currentEnergy = maxEnergy;
    }

    public void LoadData(GameData data)
    {
        currentEnergy = data.currentEnergy;
    }

    public void SaveData(GameData data)
    {
        //Energy saved via playerattribs
    }
}
