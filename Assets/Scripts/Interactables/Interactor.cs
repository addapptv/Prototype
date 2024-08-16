using System.Collections;
using UnityEngine;
using TMPro;

public class Interactor : MonoBehaviour
{

    //TO DO
    //1. UI still shows prompt when button pressed - should hide until interactable is moved away from and found again - fix

    public LayerMask _interactableMask;
    public GameObject _interactUI;
    public TextMeshProUGUI _interactPrompt;
    private bool isDetected = false;
    private bool isTriggered = false;
    RaycastHit hit;
    IInteractable interactable;

    void Update()
    {

        if (!isTriggered)
        {
            DetectInteractable();
        }

        if (isDetected)
        {
            InteractWith();
        }

    }

    void DetectInteractable()
    {
        var ray45Down = transform.forward - transform.up;

        if (Physics.Raycast(transform.position, ray45Down, out hit, 2, _interactableMask))
        {
            isDetected = true;
            _interactPrompt.text = hit.collider.GetComponent<IInteractable>().InteractionPrompt.ToString();
            interactable = hit.collider.GetComponent<IInteractable>();
            _interactUI.SetActive(true);
        }
        else
        {
            isDetected = false;
            _interactUI.SetActive(false);
        }

    }

    void InteractWith()
    {
        if (interactable != null && GetComponent<InputHandler>().playerInteract.Interact.WasPressedThisFrame())
        {
            StartCoroutine(InteractTriggered());
        }
    }

    IEnumerator InteractTriggered()
    {
        isTriggered = true;
        _interactUI.SetActive(false);
        interactable.Interact(this);
        yield return new WaitForSeconds(5f);
        isTriggered = false;
    }

}