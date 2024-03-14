#if ENABLED_CINEMACHINE
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UKnack;
using UKnack.Attributes;
using UKnack.Common;
using UKnack.Events;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(CinemachineFreeLook))]
public class FreeLook2DeltaFromInputSystem : MonoBehaviour, ISubscriberToEvent<CallbackContext>//, AxisState.IInputAxisProvider
{
    [SerializeField] [Range(0f, 5f)] private float LookSpeed = 2f;
    [SerializeField] [Range(0.000000001f, 0.2f)] private float IgnoreSqrMagnitudeThreshold = 0.000001f;
    [SerializeField] private bool InvertY = false;
    [SerializeField] [ValidReference(typeof(SOEvent<CallbackContext>))] private SOEvent<CallbackContext> _rotateEvent;

    public Vector2 mouseAcceleration = new Vector2(2, 1);

    private CinemachineFreeLook _freeLookComponent;

    private Vector2 lookMovement;

    private bool _mouse = false;
    private Vector2 _mouseNeedToMove;

    private string _description = string.Empty;
    private bool _notInitialized = true;
    public string Description => _description;

    public void OnEventNotification(CallbackContext t) => 
        OnInputSystemRotation(t);
    private void OnInputSystemRotation(InputAction.CallbackContext context)
    {
        Vector2 newRawMovement = context.ReadValue<Vector2>();
        Vector2 newMovement = new Vector2(newRawMovement.x * 180f, InvertY ? -newRawMovement.y : newRawMovement.y);

        if (context.canceled)
            Debug.Log("canceled");

        Debug.Log($"{newMovement} {context.started} {context.performed} {context.canceled} {context.control.IsActuated()} {context.control.IsPressed()}");
        var currentDevice = context.control.device;
        if (currentDevice == Mouse.current || Touchscreen.current == currentDevice || currentDevice == Pointer.current)
        {
            _mouse = true;
            lookMovement = lookMovement + (newMovement*mouseAcceleration);
            return;
        }

        lookMovement = newMovement;
    }

    public void OnEnable()
    {
        _freeLookComponent = GetComponent<CinemachineFreeLook>();
        if (_freeLookComponent == null)
            throw new System.NullReferenceException(nameof(_freeLookComponent));
        if (_rotateEvent == null)
            throw new System.NullReferenceException(nameof(_rotateEvent));
        _description = $"{CommonStatic.GetFullPath_Recursive(gameObject)}/{nameof(FreeLook2DeltaFromInputSystem)}";
        _rotateEvent.Subscribe(this);
        _notInitialized= false;
    }
    public void OnDisable()
    {
        if (_notInitialized)
            return;
        _rotateEvent.UnsubscribeNullSafe(this);
        _notInitialized = true;
    }

    private void FixedUpdate()
    {
        if (_notInitialized)
            return;
        if (lookMovement.sqrMagnitude > IgnoreSqrMagnitudeThreshold)
        {
            //Ajust axis values using look speed and Time.deltaTime so the look doesn't go faster if there is more FPS
            _freeLookComponent.m_XAxis.Value += lookMovement.x * LookSpeed * Time.deltaTime;
            _freeLookComponent.m_YAxis.Value += lookMovement.y * LookSpeed * Time.deltaTime;
        }
        if (_mouse)
        {
            lookMovement = Vector2.zero;
            _mouse = false;
        }

    }

    /*
    public float GetAxisValue(int axis)
    {
        if (lookMovement.sqrMagnitude < IgnoreSqrMagnitudeThreshold)
            return 0;
        switch (axis)
        {
            case 0: return lookMovement.x * LookSpeed;
            case 1: return lookMovement.y * LookSpeed;
            case 2: return 0;
        }
        return 0;
    }*/
}

#endif