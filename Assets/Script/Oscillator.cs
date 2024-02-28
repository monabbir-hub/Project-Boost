using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    Vector3 startingPos;
    public Vector3 movementVec;
    float movementFact;
    public float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if ( period <= Mathf.Epsilon) { return; }
        //Sin wave for oscillation
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFact = (rawSinWave + 1f) / 2f;


        Vector3 offset = movementVec * movementFact;
        transform.position = startingPos + offset;
        
    }
}
