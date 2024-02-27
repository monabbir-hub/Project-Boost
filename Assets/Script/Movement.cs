using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float mainThrust = 100f;
    public float rotationThrust = 1f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
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
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }        
    }

    private void Rotation() 
    {

        if(Input.GetKey(KeyCode.A) )
        {
            ApplyRotation(rotationThrust);

        }

        else if(Input.GetKey(KeyCode.D)) 
        {
            ApplyRotation(-rotationThrust);   
        }

    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;   //Freezing Rotation for manual rotation
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;  //Unfreezing rotation for physics system rotation
    }
}
