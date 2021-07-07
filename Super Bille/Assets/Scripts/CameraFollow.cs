using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Transform sphere;

    [SerializeField] private float smoothSpeed;
    [SerializeField] private Vector3 offset;

    private Vector3 desiredPosition, smoothedPosition;

    private void FixedUpdate()
    {
        desiredPosition = sphere.position + offset;
        smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        this.transform.position = smoothedPosition;

        transform.LookAt(sphere);
    }

}
