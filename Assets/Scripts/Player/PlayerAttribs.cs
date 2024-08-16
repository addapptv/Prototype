using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttribs : MonoBehaviour, IDataPersistence
{
    //Script to be attached to player gameobject//
    
    //TO DO
    // 1. Move movespeed/rotationspeed etc to playerattribs so they can be varied
    // 2. Add awake/asleep functions
    // 2. Add experience levels (overall performance boost)?
    // 3. Add skills (fitness, climbing)
    // 4. Move Energy updates to events
    // 5. Add player rotation to savedata

    public static PlayerAttribs instance { get; private set; }

    [HideInInspector]
    public bool isMoving;

/*    public float currentEnergy;*/
    public float maxWalkSpeed;
    public float maxClimbAngle;

    public Vector3 playerPosition;
/*    public Vector3 playerRotation;*/
    public float currentMoney = 0;
    public float currentExperience = 0;
    public float currentProgress = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Player Attributes in the scene.");
        }
        instance = this;
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.OnQuestReward += ClaimRewards;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.OnQuestReward -= ClaimRewards;
    }

    void Update()
    {
        CheckIsMoving();
/*        currentEnergy = EnergyManager.instance.Energy;*/
        if (isMoving)
        {
            playerPosition = transform.position;
/*            playerRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);*/
        }
    }

    private void ClaimRewards(float money, float experience, float progress)
    {
        currentMoney += money;
        currentExperience += experience;
        currentProgress += progress;
    }

    private void CheckIsMoving()
    {
        if (GetComponent<CharacterController>().velocity.magnitude > 0.2)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }

    public void UpdatePlayerPosition()
    {
        Debug.Log("Player position updated");
        transform.position = playerPosition;
        /*transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);*/
        Physics.SyncTransforms();
    }

    public void LoadData(GameData data)
    {
        currentMoney = data.currentMoney;
        currentExperience = data.currentExperience;
        currentProgress = data.currentProgress;
        playerPosition = data.playerPosition;
/*        playerRotation = data.playerRotation;*/
        StartCoroutine(UpdatePlayerPos());
    }

    public void SaveData(GameData data)
    {
        data.currentMoney = currentMoney;
        data.currentExperience = currentExperience;
        data.currentProgress = currentProgress;
        data.playerPosition = playerPosition;
/*        data.playerRotation = playerRotation;*/
    }

    IEnumerator UpdatePlayerPos()
    {
        yield return new WaitForSeconds(1);
        UpdatePlayerPosition();
    }
}
