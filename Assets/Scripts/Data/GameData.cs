using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TO DO - ADD TIME AND DATE//
//ADD PLAYER ROTATION//

[System.Serializable]
public class GameData
{
    public long lastUpdated;

    //Time system//

    public float minute, hour, day, second, month, year;


    //Player attribs//

    public float currentEnergy;
    public float maxWalkSpeed;
    public float maxClimbAngle;
    public Vector3 playerPosition;
/*    public Vector3 playerRotation;*/
    public float currentMoney = 0;
    public float currentExperience = 0;
    public float currentProgress = 0;

    //Quest manager//

    public SerializableDictionary<string, QuestData> questSaveData;

    //Inventory system//

    public string backpackSaveData;



    // the values defined in this constructor will be the default values
    // the game starts with when there's no data to load
    public GameData()
    {
        //Time system//

        minute = 0;
        hour = 0;
        day = 1;
        second = 0;
        month = 1;
        year = 1;


        //Player attribs//

        currentEnergy = 100.9f;
        maxWalkSpeed = 6f;
        maxClimbAngle = 40f;
        playerPosition = Vector3.zero;
/*        playerRotation = Vector3.zero;*/
        currentMoney = 0;
        currentExperience = 0;
        currentProgress = 0;

        //Quest data//

        questSaveData = new SerializableDictionary<string, QuestData>();

        //Inventory data//

        backpackSaveData = null;
    }

    public int GetPercentageComplete()
    {
/*        // figure out how many coins we've collected
        int totalCollected = 0;
        foreach (bool collected in coinsCollected.Values)
        {
            if (collected)
            {
                totalCollected++;
            }
        }

        // ensure we don't divide by 0 when calculating the percentage*/
        int percentageCompleted = -1;
/*        if ()
        {

        }*/
        return percentageCompleted;
    }
}