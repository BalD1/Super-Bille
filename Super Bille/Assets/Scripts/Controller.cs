using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    [SerializeField] private float rotateSpeed;
    [SerializeField] private Vector3 rotationClamp;
    [SerializeField] private Rigidbody trayBody;

    private float horizontal, vertical;

    private Vector3 inputRotation;
    private Quaternion deltaRotation;

    void Start()
    {
        if(trayBody == null)
            trayBody = this.gameObject.GetComponent<Rigidbody>();
    }


    void Update()
    {

    }

    private void FixedUpdate()
    {
        Rotation();
    }

    /*
    private void Rotation()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
            horizontal = 1;
        else if(Input.GetKey(KeyCode.RightArrow))
            horizontal = -1;

        if(Input.GetKey(KeyCode.UpArrow))
            vertical = 1;
        else if(Input.GetKey(KeyCode.DownArrow))
            vertical = -1;

        objRotation.x = vertical * rotateSpeed;
        objRotation.y = horizontal * rotateSpeed;

        this.gameObject.transform.Rotate(objRotation);

        horizontal = 0;
        vertical = 0;
    }
    */

    private void Rotation()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
            horizontal = 1;
        else if(Input.GetKey(KeyCode.RightArrow))
            horizontal = -1;

        if(Input.GetKey(KeyCode.UpArrow))
            vertical = 1;
        else if(Input.GetKey(KeyCode.DownArrow))
            vertical = -1;

        inputRotation.x = vertical * rotateSpeed;
        inputRotation.z = horizontal * rotateSpeed;


        deltaRotation = Quaternion.Euler(inputRotation * Time.fixedDeltaTime);
        trayBody.MoveRotation(ClampRotation(trayBody.rotation * deltaRotation, rotationClamp));


        horizontal = 0;
        vertical = 0;
    }

    private Quaternion ClampRotation(Quaternion q, Vector3 bounds)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
        angleX = Mathf.Clamp(angleX, -bounds.x, bounds.x);
        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        float angleY = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.y);
        angleY = Mathf.Clamp(angleY, -bounds.y, bounds.y);
        q.y = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleY);

        float angleZ = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.z);
        angleZ = Mathf.Clamp(angleZ, -bounds.z, bounds.z);
        q.z = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleZ);

        return q.normalized;
    }
}
