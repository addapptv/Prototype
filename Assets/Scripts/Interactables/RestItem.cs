using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestItem : MonoBehaviour, IInteractable
{
    public RestSystem _restSystem;
    [SerializeField]
    private string _prompt;
    private bool _detectable = true;
    private bool _triggered = false;

    public string InteractionPrompt => _prompt;
    public bool IsTriggered => _triggered;
    public bool IsDetectable => _detectable;

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Rest interacted with");
        StartCoroutine(InteractCooloff());
        _restSystem.StartRest();
        return true;
    }

    public bool Rest(RestSystem restSystem)
    {
        restSystem.StartRest();
        return true;
    }

    IEnumerator InteractCooloff()
    {
        _triggered = true;
        _detectable = false;
        yield return new WaitForSeconds(120);
        _triggered = false;
        _detectable = true;
    }
}
