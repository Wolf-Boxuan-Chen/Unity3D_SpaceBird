using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 oscillateRange;
    [SerializeField] [Range(0,1)] float oscillateDegree;
    [SerializeField] float oscillateTimePeriod;
    [SerializeField] bool turnAroundAtEnd;
    float tau = Mathf.PI*2;
    Vector3 initialPosition;
    float nextActionTime;
    float rotateTimes = 0f;

    // Start is called before the first frame update
    void Start()
    {
        nextActionTime = oscillateTimePeriod/2;
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Oscillate();
        Rotator();
    }
    void Oscillate()
    {
        oscillateDegree = (Mathf.Sin(Time.time*tau/oscillateTimePeriod+tau*3/4)+1)/2+Mathf.Epsilon;
        transform.position=initialPosition+oscillateRange*oscillateDegree;
    }
    void Rotator()
    {
        if(turnAroundAtEnd == true)
        {
            if (Time.time > nextActionTime ) 
            { 
                rotateTimes = rotateTimes+180;
                nextActionTime = Time.time + oscillateTimePeriod/2;
                GetComponent<Rigidbody>().rotation = Quaternion.AngleAxis(rotateTimes, Vector3.up);        
            }
        }
        
    }
}
