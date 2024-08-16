using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{

    //Note : Lookspeed and movespeed set here, mouse maxspeed set in CinemachineFreeLook inspector and InventorySystem scripts

    [SerializeField]
    CharacterController _controller;

    //Camera
    [SerializeField]
    private Camera _camera;

    //Movement
    Vector3 _moveInput;
    Vector3 _moveVector = new Vector3(0, 0, 0);
    Vector3 _moveDirection;
    [SerializeField]
    float _moveSpeed = 6f;
    [SerializeField]
    float _gravityForce = -7.8f;

    //Input Smoothing
    private Vector3 moveInputVector;
    private Vector3 smoothInputVelocity;
    [SerializeField]
    float smoothInputSpeed = 0.2f;

    //Mouse look

    [SerializeField]
    private float _lookSpeed = 8f;
    float _freeLookToggle;


    private void OnEnable()
    {
        GameEventsManager.instance.restEvents.OnStartSleep += FlipPlayer;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.restEvents.OnStartSleep -= FlipPlayer;
    }

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        PlayerLook();
        MovePlayer();
    }

    void PlayerLook()
    {
        Quaternion targetRotation = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0);
        if(_freeLookToggle < 1)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _lookSpeed * Time.deltaTime);
        }
    }

    public void MovePlayer()
    {
        moveInputVector = Vector3.SmoothDamp(moveInputVector, _moveInput, ref smoothInputVelocity, smoothInputSpeed);
        _moveVector = new Vector3(moveInputVector.x, 0.0f, moveInputVector.y);

        _moveDirection = _moveVector.x * transform.right.normalized + _moveVector.z * transform.forward.normalized;
        if (_controller.isGrounded)
        {
            _moveDirection.y = 0.0f;
        }
        else
        {
            _moveDirection.y = _gravityForce;
        }
        
        _controller.Move(_moveDirection * Time.deltaTime * _moveSpeed);
    }

    //Input receivers
    public void ReceiveMoveInput(Vector2 input)
    {
        _moveInput = input;
    }

    public void ReceiveFreeLook(float input)
    {
        _freeLookToggle = input;
    }


    //Turns player 180 degrees to face in opposite direction - NOT WORKING
    private void FlipPlayer()
    {
        float flipTarget = _camera.transform.eulerAngles.y - 180;
        Quaternion targetRotation = Quaternion.Euler(0, flipTarget, 0);
        if (_freeLookToggle < 1)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _lookSpeed * Time.deltaTime);
        }
    }
}