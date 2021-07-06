using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    #region variables

    [SerializeField] private float rotateSpeed;
    [SerializeField] private Vector3 rotationClamp;

    [SerializeField] private Rigidbody trayBody;
    
    private float horizontal, vertical;

    private Vector3 inputRotation;
    private Quaternion deltaRotation;

    [SerializeField] private float sphereSlowDown;
    [SerializeField] private GameObject sphereGO;
    private Rigidbody sphereRB;
    private Vector3 sphereVelocity;

    #endregion

    void Start()
    {
        init();
    }

    private void init()
    {
        if(trayBody == null)
            trayBody = this.gameObject.GetComponent<Rigidbody>();
        if(sphereGO == null)
            sphereGO = GameObject.FindGameObjectWithTag("sphere");

        sphereRB = sphereGO.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Inputs();
    }

    private void Inputs()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
            horizontal = 1;
        else if(Input.GetKey(KeyCode.RightArrow))
            horizontal = -1;

        if(Input.GetKey(KeyCode.UpArrow))
            vertical = 1;
        else if(Input.GetKey(KeyCode.DownArrow))
            vertical = -1;

        if(horizontal != 0 || vertical != 0)
            Rotation();
    }

    private void Rotation()
    {
        sphereVelocity = sphereRB.velocity;
        sphereVelocity.x /= sphereSlowDown;
        sphereVelocity.z /= sphereSlowDown;

        sphereRB.velocity = sphereVelocity;     // améliore la maniabilité en ralentissant légère la bille, sans toucher à sa gravité

        inputRotation.x = vertical * rotateSpeed;
        inputRotation.z = horizontal * rotateSpeed;

        deltaRotation = Quaternion.Euler(inputRotation * Time.fixedDeltaTime);
        trayBody.MoveRotation(ClampRotation(trayBody.rotation * deltaRotation, rotationClamp));     // tourne le plateau, avec une certaine limite


        horizontal = 0;
        vertical = 0;
    }

    private Quaternion ClampRotation(Quaternion q, Vector3 bounds)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);      // la joie des angles
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
