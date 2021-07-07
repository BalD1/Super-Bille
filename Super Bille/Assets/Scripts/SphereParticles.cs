using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereParticles : MonoBehaviour
{
    [SerializeField] private float requiredVelocity;
    [SerializeField] private Rigidbody sphereRB;
    [SerializeField] private ParticleSystem particles;

    private void Start()
    {
        if(sphereRB == null)
            sphereRB = GameObject.FindGameObjectWithTag("sphere").GetComponent<Rigidbody>();
        if(particles == null)
            particles = this.gameObject.GetComponent<ParticleSystem>();
        
    }

    private void Update()
    {
        this.transform.position = sphereRB.transform.position;
        if(sphereRB.velocity.z >= requiredVelocity && !particles.isPlaying)
        {
            particles.Play();
        }
        else if (sphereRB.velocity.z < requiredVelocity)
        {
            particles.Stop();
        }
        
    }

}
