using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    //Move into different managers
    //Move UI Update to UI event and UI manager


    [HideInInspector]
    public bool gameStarted;

    public InputHandler inputHandler;

    [Header("UI Elements")]
    public Text energyText;

    [Header("Inventories")]
    public InventoryObject backpackInventory;

    //Components//

    public PlayerAttribs playerAttribs;


    private void Awake()
    {
        StartGame();
    }
    private void OnEnable()
    {
        GameEventsManager.instance.saveEvents.OnNewGame += NewGame;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.saveEvents.OnNewGame -= NewGame;
    }

    public void StartGame()
    {
        gameStarted = true;
    }

    private void Update()
    {
        if (gameStarted)
        {
            UpdateUI();
        }

    }

    //Replcae with main menu commands//
    public void NewGame()
    {
        Debug.Log("New game started");
        DataPersistenceManager.instance.NewGame();
        DataPersistenceManager.instance.SaveGame();
    }

    public void ContinueGame()
    {
        Debug.Log("Game continued - data loaded");
        DataPersistenceManager.instance.LoadGame();
    }

/*    public void RestartGame()
    {
        NewGame();
        energyManager.ResetEnergy();
        energyText.text = "Energy : " + ((int)playerAttribs.energy).ToString();
    }*/



    //Replace Update UI this with a method in the to update every second instead of every frame

    public void UpdateUI()
    {
       energyText.text = "Energy : " + ((int)EnergyManager.instance.currentEnergy).ToString();
    }




}
