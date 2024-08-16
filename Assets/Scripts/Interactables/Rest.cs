using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableController))]
public class Rest : MonoBehaviour
{
    public string interactPrompt;
    public int interactCooloffTime;
    [SerializeField]
    private int restMins = 30;
    [SerializeField]
    private float energyRestoreEffect = 0.7f;

    private void OnEnable()
    {
/*        GameEventsManager.instance.restEvents.OnRest += DimScreen;*/

    }

    private void OnDisable()
    {
        /*        GameEventsManager.instance.questEvents.OnStartQuest -= DimScreen;*/

    }

    private void Awake()
    {
        GetComponentInParent<InteractableController>().prompt = interactPrompt;
    }

    public void StartRest()
    {
        GetComponentInParent<InteractableController>().cooloffTime = interactCooloffTime;
        Debug.Log("Rest interacted with");
        GameEventsManager.instance.restEvents.StartRest(restMins, energyRestoreEffect);
        return;
    }

}
