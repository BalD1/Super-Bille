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
        VerifyRotation();
    }

    private void VerifyRotation()
    {
        Vector3 rot = this.transform.eulerAngles;
        rot.y = 0;
        rot.z = Reclamp(rot.z, rotationClamp.z, 1.2f);
        rot.x = Reclamp(rot.x, rotationClamp.x, 1.2f);
        this.transform.eulerAngles = rot;
    }

    private float Reclamp(float val, float clamp, float margin)
    {
        if(val > clamp && val < clamp * margin)
            return clamp;
        if(val < 360 - clamp && val > 360 - clamp * margin)
            return 360 - clamp;

        return val;
    }

    private void FixedUpdate()
    {
        Inputs();
    }

    private void Inputs()
    {
        if(Input.GetKey(KeyCode.LeftArrow) && (this.transform.eulerAngles.z >= 360 - rotationClamp.z || this.transform.eulerAngles.z <= rotationClamp.z || Mathf.Approximately(this.transform.eulerAngles.z, 360 - rotationClamp.z)))
            horizontal = 1;
        else if(Input.GetKey(KeyCode.RightArrow) && (!(this.transform.eulerAngles.z >= rotationClamp.z &&
                                                    this.transform.eulerAngles.z <= 360 - rotationClamp.z) ||
                                                    ((Mathf.Approximately(this.transform.eulerAngles.z, rotationClamp.z)))))
            horizontal = -1;

        if(Input.GetKey(KeyCode.UpArrow) && (this.transform.eulerAngles.x >= 360 - rotationClamp.x || this.transform.eulerAngles.x <= rotationClamp.x || Mathf.Approximately(this.transform.eulerAngles.x, 360 - rotationClamp.x)))
            vertical = 1;
        else if(Input.GetKey(KeyCode.DownArrow) && (!(this.transform.eulerAngles.x >= rotationClamp.x &&
                           this.transform.eulerAngles.x <= 360 - rotationClamp.x) ||
                           ((Mathf.Approximately(this.transform.eulerAngles.x, rotationClamp.x)))))
            vertical = -1;

        if(vertical != 0 || horizontal != 0)
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
