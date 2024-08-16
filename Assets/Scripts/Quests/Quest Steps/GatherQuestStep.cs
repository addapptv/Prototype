using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherQuestStep : QuestStep
{
    public string itemTypeRequired;
    public int itemsRequired = 2;
    public int itemsCollected = 0;

    private void OnEnable()
    {
/*        GameEventsManager.instance.questEvents.OnQuestItemCollected += QuestItemCollected;*/
        GameEventsManager.instance.itemEvents.OnCollectItem += CheckItemCollected;
    }

    private void OnDisable()
    {
/*        GameEventsManager.instance.questEvents.OnQuestItemCollected -= QuestItemCollected;*/
        GameEventsManager.instance.itemEvents.OnCollectItem -= CheckItemCollected;
    }

/*    private void QuestItemCollected(string itemType)
    {
        if (itemType == itemTypeRequired && itemsCollected < itemsRequired)
        {

            itemsCollected++;
            CheckComplete();
            UpdateState();

        }

    }*/
    private void CheckItemCollected(string itemType)
    {
        if (itemType == itemTypeRequired && itemsCollected < itemsRequired)
        {

            itemsCollected++;
            CheckComplete();
            UpdateState();

        }

    }

    private void CheckComplete()
    {
        if (itemsCollected >= itemsRequired)
        {
            FinishQuestStep();
        }
        else return;
    }

    private void UpdateState()
    {
        string state = itemsCollected.ToString();
        ChangeState(state);
    }

    protected override void SetQuestStepState(string state)
    {
        this.itemsCollected = System.Int32.Parse(state);
        UpdateState();
    }

}
