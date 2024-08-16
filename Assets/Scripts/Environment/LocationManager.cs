using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Add events to update UI
//First time location discovered UI

public class LocationManager : MonoBehaviour
{
    [HideInInspector]
    public string currentLocation;
    public GameObject locationUI;
    [HideInInspector]
    public Transform droppedItemParent;
    public Transform defaultItemParent;

    private void Awake()
    {
        droppedItemParent = defaultItemParent;
    }

}
