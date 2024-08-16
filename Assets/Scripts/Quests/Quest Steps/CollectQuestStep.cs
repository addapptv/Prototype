using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collectable))]
public class CollectQuestStep : QuestStep
{
    private string itemTypeRequired;


    private void OnEnable()
    {
        GameEventsManager.instance.itemEvents.OnCollectItem += CheckItemCollected;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.itemEvents.OnCollectItem -= CheckItemCollected;
    }

    public void CheckItemCollected(string itemType)
    {
        itemTypeRequired = GetComponent<Collectable>().item.itemName;
        if (itemType == itemTypeRequired)
        {
            FinishQuestStep();
        }
        else return;
    }

    protected override void SetQuestStepState(string state)
    {

    }

}
