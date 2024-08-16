using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public bool gameStarted;

    [Header("UI Elements")]
    public Text energyText;
    public Text locationText;


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

    }


    public void UpdateUI()
    {
       energyText.text = "Energy : " + ((int)playerAttribs.Energy).ToString();
       locationText.text = "[Unknown Location]";
    }


    public void RestartGame()
    {
        energyManager.ResetEnergy();
        energyText.text = "Energy : " + ((int)playerAttribs.Energy).ToString();
    }

}
