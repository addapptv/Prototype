using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireItem : MonoBehaviour, IInteractable
{
    public ItemObject item;
    public InventoryObject targetInventory;
    [SerializeField]
    private string _onPrompt, _offPrompt;
    private string _prompt;
    public GameObject _fire;
    public GameObject _smoke;
    private bool _detectable = true;
    private bool _triggered = false;

    public string InteractionPrompt => _prompt;
    public bool IsTriggered => _triggered;
    public bool IsDetectable => _detectable;
    public bool IsInteractable { get { return _fire != null; } }

    private void Update()
    {
        if (!_fire.activeInHierarchy)
        {
            _prompt = _onPrompt;
        }
        else
        {
            _prompt = _offPrompt;
        }
    }

    public bool Interact(Interactor interactor)
    {
        if (!_fire.activeInHierarchy)
        {
            Debug.Log("Campfire started");
            _fire.SetActive(true);
            _smoke.SetActive(true);
            StartCoroutine(InteractCooloff());
            return true;
        }
        else
        {
            _triggered = false;
            Debug.Log("Campfire extinguished");
            _fire.SetActive(false);
            StartCoroutine(InteractCooloff());
            return true;
        }

    }

    IEnumerator InteractCooloff()
    {
        _triggered = true;
        _detectable = false;
        yield return new WaitForSeconds(3);
        _triggered = false;
        _detectable = true;
    }
}
