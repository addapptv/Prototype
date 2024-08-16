using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampItem : MonoBehaviour, IInteractable
{
    public ItemObject item;
    public InventoryObject targetInventory;
    [SerializeField]
    private string _usePrompt, _pickupPrompt;
    private string _prompt;
    public bool _detectable = true;
    public bool _triggered = false;

    public string InteractionPrompt => _prompt;
    public bool IsTriggered => _triggered;
    public bool IsDetectable => _detectable;

    private void Update()
    {
        if (!_triggered)
        {
            _prompt = _usePrompt;
        }
        else
        {
            _prompt = _pickupPrompt;
        }
    }

    public bool Interact(Interactor interactor)
    {
        if (!_triggered)
        {
            Debug.Log("Camp used");
            StartCoroutine(InteractCooloff());
            FindObjectOfType<RestSystem>().StartCamp();
            return true;

        }
        else
        {
            var _item = this.GetComponent<CampItem>();
            if (_item == null) return false;
            interactor.CollectItem();

            Destroy(gameObject);
            return true;
        }


    }

    IEnumerator InteractCooloff()
    {
        _triggered = true;
        _detectable = false;
        yield return new WaitForSeconds(5);
        _detectable = true;
        yield return new WaitForSeconds(600);
        _triggered = false;
    }
}
