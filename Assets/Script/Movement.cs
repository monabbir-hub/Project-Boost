using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{    
    public float mainThrust = 100f;
    public float rotationThrust = 1f;
    public AudioClip mainEngine;

    public ParticleSystem mainThrustParticles;
    public ParticleSystem leftThrustParticles;
    public ParticleSystem rightThrustParticles;

    Rigidbody rb;
    AudioSource sfx;
    bool isAlive;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotation();
        
    }


    private void Thrust() 
    {
        if (Input.GetKey(KeyCode.Space) )
        {
            StartThrusting();

        }
        else 
        {
            sfx.Stop();
            mainThrustParticles.Stop();
        }       
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!sfx.isPlaying)
        {
            sfx.PlayOneShot(mainEngine);

        }

        if (!mainThrustParticles.isPlaying)
        {
            mainThrustParticles.Play();
        }
    }

    private void Rotation() 
    {

        if(Input.GetKey(KeyCode.A) )
        {
            LeftRoation();

        }

        else if(Input.GetKey(KeyCode.D))
        {
            RightRotation();
        }

        else
        {
            rightThrustParticles.Stop();
            leftThrustParticles.Stop();
        }

    }

    private void RightRotation()
    {
        ApplyRotation(-rotationThrust);
        if (!leftThrustParticles.isPlaying)
        {
            leftThrustParticles.Play();
        }
    }

    private void LeftRoation()
    {
        ApplyRotation(rotationThrust);
        if (!rightThrustParticles.isPlaying)
        {
            rightThrustParticles.Play();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;   //Freezing Rotation for manual rotation
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;  //Unfreezing rotation for physics system rotation
    }
}
