using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public bool gameStarted;

    public InputHandler inputHandler;

    [Header("UI Elements")]
    public Text energyText;

    [Header("Inventories")]
    public InventoryObject backpackInventory;

    //Components//

    public PlayerAttribs playerAttribs;
    EnergyManager energyManager;


    private void Awake()
    {
        StartGame();
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

        if (inputHandler.gameControl.Save.IsPressed())
        {
            backpackInventory.SaveInventory();
            Debug.Log("Backpack saved");
        }
        
        if (inputHandler.gameControl.Load.IsPressed())
        {
            backpackInventory.LoadInventory();
            Debug.Log("Backpack loaded");
        }

    }


    public void UpdateUI()
    {
       energyText.text = "Energy : " + ((int)playerAttribs.Energy).ToString();
    }


    public void RestartGame()
    {
        energyManager.ResetEnergy();
        energyText.text = "Energy : " + ((int)playerAttribs.Energy).ToString();
    }




}
