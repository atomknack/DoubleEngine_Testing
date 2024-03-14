using System;
using System.Collections;
using System.Collections.Generic;
using UKnack;
using UnityEngine;
using UnityEngine.Serialization;

public class RotateOriginalRotationAroundAxis : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Axis of rotation at every frame")]
    private Vector3 _axis;
    [SerializeField]
    [Tooltip("Rotation angle around Axis per second in degrees")]
    private float _rotationDegreesPerSecond;

    private Quaternion _originalRotation;
    private Quaternion _rotation = Quaternion.identity;

    private void Awake()
    {
        _originalRotation = transform.rotation;
    }

    private void Update()
    {
        _rotation = _rotation * Quaternion.AngleAxis(_rotationDegreesPerSecond * Time.deltaTime, _axis);
        transform.rotation = _originalRotation * _rotation;
    }

}
