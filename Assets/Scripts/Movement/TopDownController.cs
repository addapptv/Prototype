using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{

    //TO DO
    //1. Rotate to movement direction defaults to up when no input pressed - fix

    [SerializeField]
    CharacterController _controller;

    //Camera
    [SerializeField]
    private Camera _camera;

    //Movement inputs
    [SerializeField]
    private bool RotateTowardMouse = true;
    Vector3 _moveInput;   //Vector of input
    public Vector3 _moveVector = new Vector3(0, 0, 0);  //Vector of planned movement
    [SerializeField]
    float _moveSpeed = 9f;
    [SerializeField]
    float _gravityForce = -7.8f;
    [SerializeField] 

    //Input Smoothing
    Vector3 _currentInputVector;
    Vector3 _smoothInputVelocity;
    [SerializeField]
    float smoothInputSpeed = 0.2f;

    //Player rotation
    Vector2 _mousePosition;
    public Vector3 _lookVector;
    public Vector3 _lookTarget;
    [SerializeField]
    private float _lookSpeed = 5f;


    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }
    void Update()
    {

        if (!RotateTowardMouse)
        {
            RotatePlayerToMovement();
        }
        if (RotateTowardMouse)
        {
            RotatePlayerToMouse();
        }
    }

    //Rotate player to current mouse position
    private void RotatePlayerToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(_mousePosition);
        

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            _lookTarget = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            Quaternion rotation = Quaternion.LookRotation(_lookTarget - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _lookSpeed);
        }
        _currentInputVector = Vector3.SmoothDamp(_currentInputVector, _moveInput, ref _smoothInputVelocity, smoothInputSpeed);
        _moveVector = new Vector3(_currentInputVector.x, 0, _currentInputVector.y);

        if (_controller.isGrounded)
        {
            _moveVector.y = 0;
        }
        else {
            _moveVector.y = _gravityForce;
        }

        _moveVector = this.transform.TransformDirection(_moveVector);
        
        _controller.Move(Time.deltaTime * _moveSpeed * _moveVector);

    }

    //Rotate player to player movement direction
    private void RotatePlayerToMovement()
    {

        _currentInputVector = Vector3.SmoothDamp(_currentInputVector, _moveInput, ref _smoothInputVelocity, smoothInputSpeed);
        _moveVector = new Vector3(_currentInputVector.x, 0, _currentInputVector.y);

        if (_controller.isGrounded)
        {
        _moveVector.y = 0.0f;
        }

        else
        {
        _moveVector.y = _gravityForce;
        }

        //Move
        _controller.Move(Time.deltaTime * _moveSpeed * _moveVector);



        //Rotate

        _lookTarget = transform.position + _lookVector;
        Quaternion rotation = Quaternion.LookRotation(_lookTarget - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _lookSpeed);
        _lookVector = new Vector3(_currentInputVector.x, 0, _currentInputVector.y);  //Resetting to 0, 0, 0 when no input pressed - FIX

  
    }

    //Input receivers
    public void ReceiveMoveInput(Vector2 input)
    {
        _moveInput = input;
    }

    public void ReceiveMousePosition(Vector2 input)
    {
        _mousePosition = input;
    }

}
