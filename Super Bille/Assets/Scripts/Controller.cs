﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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

    [SerializeField] [Range(1, 1.1f)] private float sphereSlowDown;
    [SerializeField] private GameObject sphereGO;
    private Rigidbody sphereRB;
    private Vector3 sphereVelocity;

    private float timer;

    private bool reseting;

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
        if(Input.GetKeyDown(KeyCode.Return))
            Reset();

        if(!reseting)
        {
            if(Input.GetKey(KeyCode.LeftArrow)
                && (this.transform.eulerAngles.z >= 360 - rotationClamp.z || this.transform.eulerAngles.z <= rotationClamp.z || Mathf.Approximately(this.transform.eulerAngles.z, 360 - rotationClamp.z)))
                horizontal = 1;

            else if(Input.GetKey(KeyCode.RightArrow) && (!(this.transform.eulerAngles.z >= rotationClamp.z &&
                                                        this.transform.eulerAngles.z <= 360 - rotationClamp.z) ||
                                                        ((Mathf.Approximately(this.transform.eulerAngles.z, rotationClamp.z)))))
                horizontal = -1;

            if(Input.GetKey(KeyCode.UpArrow)
                && (this.transform.eulerAngles.x >= 360 - rotationClamp.x || this.transform.eulerAngles.x < rotationClamp.x))
                vertical = 1;

            else if(Input.GetKey(KeyCode.DownArrow) && (!(this.transform.eulerAngles.x >= rotationClamp.x &&
                               this.transform.eulerAngles.x <= 360 - rotationClamp.x) ||
                               ((Mathf.Approximately(this.transform.eulerAngles.x, rotationClamp.x)))))
                vertical = -1;


            if(vertical != 0)
                V();
            if(horizontal != 0)
                H();

        }
    }

    private void H()
    {

        inputRotation.z += horizontal * baseHorizontalRotateSpeed;
        horizontalRotateSpeed = inputRotation.z;

        deltaRotation = Quaternion.Euler(inputRotation * Time.fixedDeltaTime);

        this.transform.RotateAround(
                                     sphereRB.transform.position,
                                     trayBody.rotation * deltaRotation.eulerAngles,
                                     horizontalRotateSpeed * Time.deltaTime
                                    );
        sphereRB.velocity /= sphereSlowDown;
        horizontal = 0;
        inputRotation = Vector3.zero;
    }

    private void V()
    {

        inputRotation.x += vertical * baseVerticalRotateSpeed;
        verticalRotateSpeed = inputRotation.x;

        deltaRotation = Quaternion.Euler(inputRotation * Time.fixedDeltaTime);

        this.transform.RotateAround(
                                     sphereRB.transform.position,
                                     trayBody.rotation * deltaRotation.eulerAngles,
                                     verticalRotateSpeed * Time.deltaTime
                                    );

        sphereRB.velocity /= sphereSlowDown;
        vertical = 0;
        inputRotation = Vector3.zero;
    }


    /// <summary>
    /// Reset la rotation du plateau
    /// </summary>
    private void Reset()
    {
        reseting = true;

        StartCoroutine(SmoothRotate(this.transform, Quaternion.identity, 0.5f));

    }


    private IEnumerator SmoothRotate(Transform target, Quaternion rot, float duration)
    {

        float t = 0f;
        Quaternion start = target.rotation;
        while(t < duration)
        {
            sphereRB.velocity /= (sphereSlowDown / 1.01f);
            if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow)) // si on appuie sur une touche, on annule le reset
            {
                t = duration;
                reseting = false;
                yield break;
            }
            target.rotation = Quaternion.Slerp(start, rot, t / duration);
            yield return null;
            t += Time.deltaTime;
        }
        reseting = false;
        target.rotation = rot;
    }
}
