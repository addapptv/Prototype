using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camp : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string _prompt;
    public RestSystem _restSystem;

    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Camp interacted with");
        _restSystem.StartCamp();
        return true;
    }
}
