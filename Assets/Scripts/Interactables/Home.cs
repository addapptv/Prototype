using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string _prompt;
    [SerializeField]
    public RestSystem _restSystem;

    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Home interacted with");
        _restSystem.StartSleep();
        return true;
    }

}
