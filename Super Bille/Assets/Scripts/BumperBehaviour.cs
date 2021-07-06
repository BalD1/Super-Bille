using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperBehaviour : MonoBehaviour
{
    public int speed;
    public bool mirror; //pour inverser le direction de départ
    Vector3 startPosition;
    Renderer rend;
    public float timer;
    float time;

    private void Start()
    {
        time = timer;
        rend = GetComponent<Renderer>();
        startPosition = this.transform.position;
    }
    void Update()
    {
        time -= Time.deltaTime;
        if(time <0)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector3 position = this.transform.position;
        position.x += speed * Time.deltaTime;
        this.transform.position = position;
        //pour inverser le sens de déplacement
        if (mirror)
        {
            if (position.x < startPosition.x - rend.bounds.size.x)
            {
                speed *= -1;
            }
            if (position.x > startPosition.x)
            {
                speed *= -1;
                time = timer;
            }
        }
        else
        {
            if (position.x > startPosition.x + rend.bounds.size.x)
            {
                speed *= -1;
            }
            if (position.x < startPosition.x)
            {
                speed *= -1;
                time = timer;
            }
        }
    }
}
