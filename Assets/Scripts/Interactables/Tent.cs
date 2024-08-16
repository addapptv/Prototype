using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractableController))]
public class Tent : MonoBehaviour
{
    public ItemObject item;
    [HideInInspector]
    public InventoryObject targetInventory;
    [SerializeField]
    private string _usePrompt, _pickupPrompt;
    public float energyRestoreEffect;
    [HideInInspector]
    public int interactCooloffTime = 5;
    public int campCooloffTime = 15;

    private void Awake()
    {
        targetInventory = item.targetInventory;
        GetComponentInParent<InteractableController>().cooloffTime = interactCooloffTime;
        GetComponentInParent<InteractableController>().prompt = _usePrompt;

    }

    public void UseTent()
    {
        if (GetComponentInParent<InteractableController>()._triggered == false)
        {

            Debug.Log("Slept in tent");
            StartCamp();
            StartCoroutine(CampCoolOff());

            // To do  - Delay change in prompt //
            
            GetComponentInParent<InteractableController>().prompt = _pickupPrompt;
            return;

        }
        else
        {
            Debug.Log("Tent picked up");
            PickupTent();
            GetComponentInParent<InteractableController>().prompt = _usePrompt;
            return;

        }
    }

    private void StartCamp()
    {

        Debug.Log("Tent interacted with");
        GameEventsManager.instance.restEvents.StartCamp(energyRestoreEffect);
        return;
    }

    private void PickupTent()
    {
        targetInventory.AddItem(new Item(item));
        Debug.Log("Tent picked up");
        Destroy(this.gameObject);
    }


    IEnumerator CampCoolOff()
    {
        GetComponentInParent<InteractableController>()._triggered = true;
        yield return new WaitForSeconds(campCooloffTime);
        GetComponentInParent<InteractableController>()._triggered = false;
    }
}
