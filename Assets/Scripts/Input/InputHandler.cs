using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    //TO DO : Consider remapping to Trevor Mock's style using bools instead of contexts//


    public static InputHandler instance { get; private set; }

    [SerializeField] TopDownController tDMovement;
    [SerializeField] ThirdPersonController tPMovement;

    private static PlayerControls controls;
    public PlayerControls.MovementActions playerMovement;
    public PlayerControls.CameraActions cameraControl;
    public PlayerControls.InteractActions playerInteract;
    public PlayerControls.MenusActions menusControl;
    public PlayerControls.GameActions gameControl;

    private Vector2 _moveInput;
    Vector2 _mousePosition;
    float _freeLookToggle;


    private void Awake()
    {
        if (instance != null)
        {
                Debug.LogError("Found more than one Input Manager in the scene.");
        }
        instance = this;

        //Intialise all controls

        controls = new PlayerControls();

        playerMovement = controls.Movement;
        cameraControl = controls.Camera;
        playerInteract = controls.Interact;
        menusControl = controls.Menus;
        gameControl = controls.Game;

        //Movement controls events
        playerMovement.Move.started += ctx => _moveInput = ctx.ReadValue<Vector2>();
        playerMovement.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        playerMovement.Move.canceled += ctx => _moveInput = ctx.ReadValue<Vector2>();

        cameraControl.FreeLook.performed += ctx => _freeLookToggle = ctx.ReadValue<float>();
        cameraControl.FreeLook.canceled += ctx => _freeLookToggle = ctx.ReadValue<float>();

        //Camera
        cameraControl.CameraSwitch.performed += _ => GameEventsManager.instance.inputEvents.SwitchCamPressed();

        //Menus
        menusControl.ShowBackpack.performed += _ => GameEventsManager.instance.inputEvents.MenuPressed();
        menusControl.ShowBackpack.canceled += _ => GameEventsManager.instance.inputEvents.MenuPressed();

        menusControl.ShowQuestList.performed += _ => GameEventsManager.instance.inputEvents.MenuPressed();
        menusControl.ShowQuestList.canceled += _ => GameEventsManager.instance.inputEvents.MenuPressed();

        //Save/Load
        gameControl.NewGame.performed += _ => GameEventsManager.instance.saveEvents.NewGame();

        gameControl.Save.performed += _ => GameEventsManager.instance.saveEvents.SaveGame();
        
        gameControl.Load.performed += _ => GameEventsManager.instance.saveEvents.LoadGame();
    }

    private void Update()
    {
        _mousePosition = playerMovement.MousePos.ReadValue<Vector2>();

        tDMovement.ReceiveMoveInput(_moveInput);
        tDMovement.ReceiveMousePosition(_mousePosition);
   
        tPMovement.ReceiveMoveInput(_moveInput);
        tPMovement.ReceiveFreeLook(_freeLookToggle);

    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

}