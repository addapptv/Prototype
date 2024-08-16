using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class InventorySystem : MonoBehaviour, IDataPersistence
{
    public InputHandler inputHandler;
    public InventoryObject backpackInventory;
    public GameObject inventoryScreen;
    public CinemachineFreeLook TPcam;
    public string backpackSaveData;

    private void Awake()
    {
        backpackSaveData = null;
    }

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.OnMenuPressed += ShowInventory;

    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.OnMenuPressed -= ShowInventory;

    }

    public void ShowInventory()
    {

        if (InputHandler.instance.menusControl.ShowBackpack.IsPressed())
        {
            inventoryScreen.SetActive(true);
            Cursor.visible = true;
            TPcam.m_YAxis.m_MaxSpeed = 0;
            TPcam.m_XAxis.m_MaxSpeed = 0;

        }
        else if (inventoryScreen.GetComponent<DisplayInventory>().actionsComplete == true)
        {
            inventoryScreen.SetActive(false);
            Cursor.visible = false;
            TPcam.m_YAxis.m_MaxSpeed = 2;
            TPcam.m_XAxis.m_MaxSpeed = 300;
        }
        
    }

    private void OnApplicationQuit()
    {
        //CLEAR INVENTORY AFTER EACH QUIT//
        
        backpackInventory.Container.Items = new InventorySlot[32];

    }

    public void LoadData(GameData data)
    {
        backpackSaveData = data.backpackSaveData;
        JsonUtility.FromJsonOverwrite(backpackSaveData, backpackInventory);
        Debug.Log("Backpack loaded");
    }

    public void SaveData(GameData data)
    {
        backpackSaveData = JsonUtility.ToJson(backpackInventory, true);
        data.backpackSaveData = backpackSaveData; 
        Debug.Log("Backpack saved");
    }
}
