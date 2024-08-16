using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DeliverQuestStep : QuestStep
{

    public string itemTypeRequired;
    public int requiredItems;
    private int deliveredItems = 0;

    private void OnTriggerEnter(Collider Collider)
    {
        if (Collider.CompareTag("Collectable"))
        {
            string droppedItem = Collider.GetComponent<Collectable>().item.itemName;

            if (Collider.GetComponent<Collectable>() != null && droppedItem == itemTypeRequired)
            {

                Debug.Log(droppedItem + " delivered");
                deliveredItems++;
                Destroy(Collider.gameObject);
                CheckDeliveredItems();
            }
            else
            {
                Debug.Log("Dropped item not of type required");
                return;
            }
        }
        else
        {
            Debug.Log("Dropped item not collectable");
            return;
        }
    }

    private void CheckDeliveredItems()
    {
        if (deliveredItems < requiredItems)
        {
            return; 
        }
        else if (deliveredItems == requiredItems)
        {
            FinishQuestStep();
        }

    }


    protected override void SetQuestStepState(string state)
    {

    }

}
