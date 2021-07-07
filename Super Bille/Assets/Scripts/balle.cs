using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balle : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float speedAcceleration;
    [SerializeField] private float maxSpeed;
    [SerializeField] private Rigidbody rb;

    private void Update()
    {
        if(speed < maxSpeed)
            speed += speedAcceleration * Time.deltaTime;

        if(Input.GetKey(KeyCode.UpArrow))
            rb.AddForce(Vector3.forward * speed);
        else if(Input.GetKey(KeyCode.DownArrow))
            rb.AddForce(Vector3.back * speed);
    }

}
