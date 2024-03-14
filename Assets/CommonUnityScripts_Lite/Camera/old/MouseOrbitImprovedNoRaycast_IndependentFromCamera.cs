using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class MouseOrbitImprovedNoRaycast_IndependentFromCamera : MonoBehaviour
{
    public Transform satelite;
    public Transform target;
    public float distance = 5.0f;
    public float distanceStep = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    public float flyOffdistance = 4.0f;
    public float flyOffSpeed = 1.0f;

    public bool rotateOnlyWhenRightMouseButtonPressed = true;

    private Rigidbody _rigidbody;

    float x = 0.0f;
    float y = 0.0f;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = satelite.eulerAngles;
        x = angles.y;
        y = angles.x;

        _rigidbody = GetComponent<Rigidbody>();

        //P commented
        // Make the rigid body not change rotation
        //if (_rigidbody != null)
        //{
        //    _rigidbody.freezeRotation = true;
        //}
    }


    float DistanceToTarget() => Vector3.Distance(target.transform.position, satelite.position);
    void LateUpdate()
    {
        Quaternion rotation = satelite.rotation;

        if (rotateOnlyWhenRightMouseButtonPressed && !Input.GetMouseButton(1))
        { }
        else
        {
            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            rotation = Quaternion.Euler(y, x, 0);
        }
        //return;
        if (target)
        {


            distance = DistanceToTarget();

            if (distance < flyOffdistance)
            {
                //RaycastHit hitback;
                //if (! Physics.Linecast(satelite.position,satelite.position-(Vector3.forward* (flyOffSpeed * Time.deltaTime *2)), out hitback))
                //{
                //distance -= hitback.distance; //TODO: use real distance to calsulate how far camera can move
                distance += flyOffSpeed * Time.deltaTime; //P: slowly fly off from tracked object
                //}

            }

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * distanceStep, distanceMin, distanceMax);

            //RaycastHit hit;
            //if (Physics.Linecast(target.position, satelite.position, out hit))
            //{
            Vector3 negDistance;
            if (distance > distanceMax)
                negDistance = new Vector3(0.0f, 0.0f, -(DistanceToTarget() - distance + flyOffdistance));
            else
                negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            satelite.rotation = rotation;
            satelite.position = position;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F; //P possible bug should be MOD of angle be used?
        if (angle > 360F)
            angle -= 360F; //P possible bug should be MOD of angle be used?
        return Mathf.Clamp(angle, min, max);
    }
}
