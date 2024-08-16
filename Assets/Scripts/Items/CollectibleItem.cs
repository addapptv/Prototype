using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class CollectibleItem : MonoBehaviour, IInteractable
{
    public ItemObject item;
    public string _prompt;
    public InventoryObject targetInventory;
    private bool _detectable = true;
    private bool _triggered = false;

    public string InteractionPrompt => _prompt;
    public bool IsTriggered => _triggered;
    public bool IsDetectable => _detectable;

    public bool Interact(Interactor interactor)
    {
        var _item = this.GetComponent<CollectibleItem>();
        if (_item == null) return false;
        interactor.CollectItem();

        Destroy(gameObject);
        return true;
    }

}
