using System;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }

    public InputEvents inputEvents;
    /*    public PlayerEvents playerEvents;
        public GoldEvents goldEvents;*/
    public MiscEvents miscEvents;
    public CameraEvents cameraEvents;
    public ItemEvents itemEvents;
    public QuestEvents questEvents;
    public RestEvents restEvents;
    public SaveEvents saveEvents;
    public UIEvents uiEvents;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Game Events Manager in the scene.");
        }
        instance = this;

        InitialiseEvents();
    }

    public void InitialiseEvents()
    {
        inputEvents = new InputEvents();
        /*        playerEvents = new PlayerEvents();
                goldEvents = new GoldEvents();*/
        miscEvents = new MiscEvents();
        cameraEvents = new CameraEvents();
        itemEvents = new ItemEvents();
        questEvents = new QuestEvents();
        restEvents = new RestEvents();
        saveEvents = new SaveEvents();
        uiEvents = new UIEvents();
    }
}