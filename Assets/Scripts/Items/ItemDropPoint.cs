using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ItemDropPoint : MonoBehaviour
{

    //Re-factor to be a drop point for specific items e.g. things you sell etc//


    public string validItemType;

    private void OnTriggerEnter(Collider Collider)
    {
        if (Collider.CompareTag("Collectable"))
        {
            string droppedItem = Collider.GetComponent<Collectable>().item.itemName;


            if (Collider.GetComponent<Collectable>() != null && droppedItem == validItemType)
            {
                Debug.Log(Collider.GetComponent<Collectable>().item.itemName + " dropped");
                Destroy(Collider.gameObject);
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
    }
}
