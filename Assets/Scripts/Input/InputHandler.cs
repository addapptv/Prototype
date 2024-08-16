using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

    [SerializeField] TopDownController tDMovement;
    [SerializeField] ThirdPersonController tPMovement;
    [SerializeField] CamSwitcher camSwitch;

    private PlayerControls controls;
    PlayerControls.MovementActions playerMovement;
    PlayerControls.CameraActions cameraControl;
    public PlayerControls.InteractActions playerInteract;

    public Vector2 _moveInput;
    Vector2 _mousePosition;
    float _freeLookToggle;


    private void Awake()
    {
        controls = new PlayerControls();

        playerMovement = controls.Movement;
        cameraControl = controls.Camera;
        playerInteract = controls.Interact;

        //Movement
        playerMovement.Move.started += ctx => _moveInput = ctx.ReadValue<Vector2>();
        playerMovement.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        playerMovement.Move.canceled += ctx => _moveInput = ctx.ReadValue<Vector2>();

        cameraControl.FreeLook.performed += ctx => _freeLookToggle = ctx.ReadValue<float>();
        cameraControl.FreeLook.canceled += ctx => _freeLookToggle = ctx.ReadValue<float>();

        //Camera
        cameraControl.CameraSwitch.performed += _ => camSwitch.SwitchCam();

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