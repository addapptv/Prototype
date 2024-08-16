using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{

    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private LayerMask _interactableMask;

    //Refactor to send event to UI
    public GameObject _interactUI;


    //Re-factor this InteractWith method to avoid constantly checking if input was pressed - replace with events//
    [SerializeField]
    private InputHandler _inputHandler;

    //Refactor to send event to UI

    public TextMeshProUGUI _interactPrompt;
    private bool _interactableDetected = false;
    RaycastHit hit;
    IInteractable _interactable;


    //Add secondary interact (A & B buttons) which change depending on state of interactable//

    void Update()
    {
        DetectInteractable();
    }

    void DetectInteractable()
    {
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, 7, _interactableMask))
        {
            _interactable = hit.collider.GetComponent<IInteractable>();
            if (_interactable.IsDetectable)
            {
                _interactableDetected = true;
                _interactPrompt.text = hit.collider.GetComponent<IInteractable>().InteractionPrompt.ToString();
                _interactUI.SetActive(true);
            }
            
            if (_interactableDetected)
            {
                InteractWith();
            }
        }
        else
        {
            _interactable = null;
            _interactableDetected = false;
            _interactUI.SetActive(false);
        }
    }

    //Replace below with input event//

    void InteractWith()
    {
        if (_interactable != null && _inputHandler.playerInteract.Interact.WasPressedThisFrame())
        {
            _interactUI.SetActive(false);
            _interactable.Interact(this);
        }
    }

}