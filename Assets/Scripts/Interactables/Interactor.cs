using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField]
    private LayerMask _interactableMask;
    public GameObject _interactUI;
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private InputHandler _inputHandler;
    public TextMeshProUGUI _interactPrompt;
    private bool _isDetected = false;
    RaycastHit hit;
    IInteractable _interactable;


    void Update()
    {
        DetectInteractable();
        
        if (_isDetected)
        {
            InteractWith();
        }
    }

    void DetectInteractable()
    {
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, 7, _interactableMask))
        {
            _interactable = hit.collider.GetComponent<IInteractable>();
            if (_interactable.IsDetectable)
            {
                _isDetected = true;
                _interactPrompt.text = hit.collider.GetComponent<IInteractable>().InteractionPrompt.ToString();
                _interactUI.SetActive(true);
            }
        }
        else
        {
            _isDetected = false;
            _interactUI.SetActive(false);
        }
    }

    void InteractWith()
    {
        if (_interactable != null && _inputHandler.playerInteract.Interact.WasPressedThisFrame())
        {
            _interactUI.SetActive(false);
            _interactable.Interact(this);
        }
    }

    public void CollectItem()
    {
        var item = hit.collider.GetComponent<CollectibleItem>();
        var camp = hit.collider.GetComponent<CampItem>();
        if (item)
        {
            item.targetInventory.AddItem(new Item(item.item));
            Debug.Log("Item picked up");
        }
        else if (camp)
        {
            camp.targetInventory.AddItem(new Item(camp.item));
            Debug.Log("Camp picked up");
        }
    }

}