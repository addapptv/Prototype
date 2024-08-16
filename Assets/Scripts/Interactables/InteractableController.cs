using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class InteractableController : MonoBehaviour, IInteractable
{


    [HideInInspector]
    public string prompt;
    public bool oneTimeInteract = false;
    public int cooloffTime;
    [HideInInspector]
    public bool _detectable = true;
    [HideInInspector]
    public bool _triggered = false;

    public UnityEvent interacted;
    [HideInInspector]
    public string interactionName;

    public string InteractionPrompt => prompt;
    public bool IsTriggered => _triggered;
    public bool IsDetectable => _detectable;


    public bool Interact(Interactor interactor)
    {
        interacted.Invoke();
        _detectable = false;
        GameEventsManager.instance.miscEvents.Interact(interactionName);
        if (oneTimeInteract == false)
        {
            StartCoroutine(InteractCooloff());
        }
        return false;
    }

    IEnumerator InteractCooloff()
    {
        yield return new WaitForSeconds(cooloffTime);
        _detectable = true;
    }

}
