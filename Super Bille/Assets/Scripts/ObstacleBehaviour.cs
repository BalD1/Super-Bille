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
        positionRay.y += 5;
        Debug.DrawRay(transform.position,transform.forward * 10, Color.red);
        Debug.DrawRay(transform.position,-transform.forward * 10, Color.red);
        Debug.DrawRay(transform.position,transform.right * 10, Color.red);
        Debug.DrawRay(transform.position,-transform.right * 10, Color.red);
        if(Physics.Raycast(transform.position, transform.forward, out hit,10,layerMask))
        {
            force.y = Mathf.Abs(force.z);
        }
        if (Physics.Raycast(transform.position, -transform.forward, out hit, 10, layerMask))
        {
            force.z *= -1;
        }
        if (Physics.Raycast(transform.position, transform.right, out hit, 10, layerMask))
        {
            force.y = Mathf.Abs(force.x);
        }
        if (Physics.Raycast(transform.position, -transform.right, out hit, 10, layerMask))
        {
            force.x *= -1;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Rigidbody>().AddForce(force);
    }
}
