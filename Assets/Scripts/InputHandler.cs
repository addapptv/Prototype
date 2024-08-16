using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

    [SerializeField] TopDownController tDMovement;
    [SerializeField] ThirdPersonController tPMovement;
    [SerializeField] CamSwitcher camSwitch;

    //replace BasicMover with other movement script

    private PlayerControls controls;
    PlayerControls.MovementActions playerMovement;
    PlayerControls.CameraActions cameraControls;

    Vector2 _moveInput;
    Vector2 _mousePosition;
    float _freeLookToggle;


    private void Awake()
    {
        controls = new PlayerControls();

        playerMovement = controls.Movement;
        cameraControls = controls.Camera;

        //Player movement
        playerMovement.Move.started += ctx => _moveInput = ctx.ReadValue<Vector2>();
        playerMovement.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        playerMovement.Move.canceled += ctx => _moveInput = ctx.ReadValue<Vector2>();

        cameraControls.FreeLook.performed += ctx => _freeLookToggle = ctx.ReadValue<float>();
        cameraControls.FreeLook.canceled += ctx => _freeLookToggle = ctx.ReadValue<float>();

        //Camera switcher
        cameraControls.CameraSwitch.performed += _ => camSwitch.SwitchCam();
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