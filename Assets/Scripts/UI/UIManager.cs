using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class UIManager : MonoBehaviour
{
    // Add the inventory screen to this script
    // Add a listener to the event to freeze the TP & TD camera movement whilst a menu is open

/*    public InputHandler inputHandler;*/
    public GameObject inventoryPanel;
    public GameObject questListPanel;
    public ScreenFader screenFader;
    public CinemachineFreeLook TPcam;


    private void Start()
    {
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.OnMenuPressed += ShowQuestList;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.OnMenuPressed -= ShowQuestList;
    }

    public void ShowQuestList()
    {
        if (InputHandler.instance.menusControl.ShowQuestList.IsPressed())
        {
            questListPanel.SetActive(true);
            Cursor.visible = true;
            TPcam.m_YAxis.m_MaxSpeed = 0;
            TPcam.m_XAxis.m_MaxSpeed = 0;
        }
        else
        {
            questListPanel.SetActive(false);
            Cursor.visible = false;
            TPcam.m_YAxis.m_MaxSpeed = 2;
            TPcam.m_XAxis.m_MaxSpeed = 300;
        }
    }
}
