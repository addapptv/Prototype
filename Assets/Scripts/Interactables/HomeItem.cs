using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour, IInteractable
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
        Debug.Log("Home interacted with");
        StartCoroutine(InteractCooloff());
        _restSystem.StartSleep();
        return true;
    }

    IEnumerator InteractCooloff()
    {
        _triggered = true;
        _detectable = false;
        yield return new WaitForSeconds(10);
        _triggered = false;
        _detectable = true;

    }
}
