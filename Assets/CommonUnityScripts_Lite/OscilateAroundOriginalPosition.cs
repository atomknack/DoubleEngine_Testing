using System;
using UnityEngine;
using UnityEngine.Serialization;

public class OscilateAroundOriginalPosition : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Vector of translation oscilation")]
    private Vector3 _amplitude = new Vector3(1f,0.5f,0);
    [SerializeField]
    [Tooltip("How fast oscilation is going")]
    private Vector3 _speed = Vector3.one;
    [SerializeField]
    [Tooltip("Offset of sin for every coord, to make look more random")]
    private Vector3 _sinOffset= Vector3.zero;

    private Vector3 _origin;

    private void Awake()
    {
        _origin = transform.localPosition;
    }
    private void Update()
    {
        //var position = defaultTranslation + (_oscilation*(float)Math.Sin(Speed * Time.timeAsDouble));

        transform.localPosition = Oscilate(_origin, _amplitude, _speed, _sinOffset, Time.timeAsDouble);
    }

    public static Vector3 Oscilate(Vector3 origin, Vector3 amplitude, Vector3 speed, Vector3 sinOffset, double time)
    {
        return new Vector3(
            Oscilate(origin.x, amplitude.x, speed.x, sinOffset.x, time),
            Oscilate(origin.y, amplitude.y, speed.y, sinOffset.y, time),
            Oscilate(origin.z, amplitude.z, speed.z, sinOffset.z, time)
            );
    }
    public static float Oscilate(float origin, float amplitude, float speed, float sinOffset, double time)
    {
        return origin + (amplitude * (float)Math.Sin((speed * time) + sinOffset));
    }
}
