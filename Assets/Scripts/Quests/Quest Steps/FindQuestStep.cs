using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Find quest step completes when an interactable with the required name is interacted with//

public class FindQuestStep : QuestStep
{
    public string requiredInteractable;

    private void OnEnable()
    {
        GameEventsManager.instance.miscEvents.OnInteract += CheckInteract;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.miscEvents.OnInteract -= CheckInteract;
    }

    private void CheckInteract(string interactName)
    {
        if (interactName == requiredInteractable)
        {
            FinishQuestStep();
        }

    }

    protected override void SetQuestStepState(string state)
    {

    }
}
