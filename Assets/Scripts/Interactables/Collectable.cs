using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Should be added to a child object of the item gameobject together with an interact controller and a collider set as trigger//

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(InteractableController))]
public class Collectable : MonoBehaviour
{
    public ItemObject item;
    [HideInInspector]
    public InventoryObject targetInventory;

    private void Awake()
    {
        GetComponentInParent<InteractableController>().prompt = item.itemName;
        targetInventory = item.targetInventory;
    }

    public void CollectItem()
    {
        GameEventsManager.instance.itemEvents.CollectItem(item.itemName);
        targetInventory.AddItem(new Item(item));
            Debug.Log(item.itemName + " picked up");
            Destroy(this.gameObject);
    }

}
