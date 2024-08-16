using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class InventorySystem : MonoBehaviour
{
    public InventoryObject backpackInventory;
    public InputHandler inputHandler;
    public GameObject inventoryScreen;
    public CinemachineFreeLook TPcam;


    private void Update()
    {
        ShowInventory();
    }

    public void ShowInventory()
    {

        if (inputHandler.menusControl.ShowBackpack.IsPressed())
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
        backpackInventory.Container.Items = new InventorySlot[32];

    }

}
