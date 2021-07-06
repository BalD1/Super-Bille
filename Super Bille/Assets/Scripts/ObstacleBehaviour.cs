using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    public Vector3 force;
    int layerMask = 1 << 8;
    private void Start()
    {
        layerMask = ~layerMask;
    }
    private void Update()
    {
        RaycastHit hit;
        Vector3 positionRay = transform.position;
        positionRay.z +=45;
        //Cardinal
        Debug.DrawRay(transform.position,transform.forward * 5, Color.red);
        Debug.DrawRay(transform.position,-transform.forward * 5, Color.red);
        Debug.DrawRay(transform.position,transform.right * 5, Color.red);
        Debug.DrawRay(transform.position,-transform.right * 5, Color.red);
        //InterCardinal
        Debug.DrawRay(transform.position, (transform.forward + transform.right) * 5, Color.blue);
        Debug.DrawRay(transform.position, (transform.forward + -transform.right) * 5, Color.blue);
        Debug.DrawRay(transform.position, (-transform.forward + transform.right) * 5, Color.blue);
        Debug.DrawRay(transform.position, (-transform.forward + -transform.right) * 5, Color.blue);
        //Cardinal
        if(Physics.Raycast(transform.position, transform.forward, out hit, 5,layerMask))
        {
            force.z = Mathf.Abs(force.z);
        }
        if (Physics.Raycast(transform.position, -transform.forward, out hit, 5, layerMask))
        {
            force.z *= -1;
        }
        if (Physics.Raycast(transform.position, transform.right, out hit, 5, layerMask))
        {
            force.x = Mathf.Abs(force.x);
        }
        if (Physics.Raycast(transform.position, -transform.right, out hit, 5, layerMask))
        {
            force.x *= -1;
        }
        //InterCardinal
        if (Physics.Raycast(transform.position, transform.forward + transform.right, out hit, 5, layerMask))
        {
            force.z = Mathf.Abs(force.z);
            force.x = Mathf.Abs(force.x);
        }
        if (Physics.Raycast(transform.position, transform.forward + -transform.right, out hit, 5, layerMask))
        {
            force.z = Mathf.Abs(force.z);
            force.x *= -1;
        }
        if (Physics.Raycast(transform.position, -transform.forward + transform.right, out hit, 5, layerMask))
        {
            force.z *= -1;
            force.x = Mathf.Abs(force.x);
        }
        if (Physics.Raycast(transform.position, -transform.forward + -transform.right, out hit, 5, layerMask))
        {
            force.z *= -1;
            force.x *= -1;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Rigidbody>().AddForce(force);
    }
}
