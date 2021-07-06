using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    #region variables

    [SerializeField] private float baseHorizontalRotateSpeed, baseVerticalRotateSpeed;
    private float horizontalRotateSpeed, verticalRotateSpeed;
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

    private void Update()
    {
        Vector3 rot = this.transform.eulerAngles;
        rot.y = 0;
        this.transform.eulerAngles = rot;
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

        if (vertical != 0 || horizontal != 0)
        Rotation();
    }

    private void Rotation()
    {

        sphereVelocity = sphereRB.velocity;
        sphereVelocity.x /= sphereSlowDown;
        sphereVelocity.z /= sphereSlowDown;

        sphereRB.velocity = sphereVelocity;     // améliore la maniabilité en ralentissant légère la bille, sans toucher à sa gravité
        
        inputRotation.z = horizontal * baseHorizontalRotateSpeed;
        horizontalRotateSpeed = baseHorizontalRotateSpeed * horizontal;
        
      //  rotateSpeed = baseVerticalRotateSpeed / (this.transform.position.z + sphereRB.transform.position.z);
        inputRotation.x = vertical * baseVerticalRotateSpeed;
        verticalRotateSpeed = baseVerticalRotateSpeed * vertical;
        
        deltaRotation = Quaternion.Euler(inputRotation * Time.fixedDeltaTime);

        //trayBody.MoveRotation(ClampRotation(trayBody.rotation * deltaRotation, rotationClamp));     // tourne le plateau, avec une certaine limite
        this.transform.RotateAround(
            sphereRB.transform.position, 
            trayBody.rotation * deltaRotation.eulerAngles, 
            horizontalRotateSpeed * Time.deltaTime
            );

        this.transform.RotateAround(
            sphereRB.transform.position,
            trayBody.rotation * deltaRotation.eulerAngles, 
            verticalRotateSpeed * Time.deltaTime
            );

        horizontal = 0;
        vertical = 0;
    }
}
