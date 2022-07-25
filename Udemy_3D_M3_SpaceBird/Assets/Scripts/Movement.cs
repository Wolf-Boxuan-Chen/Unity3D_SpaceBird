using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Parameters
    [SerializeField] float thrustAmount = 1200f;
    [SerializeField] float rotateAmount = 80f;
    [SerializeField] AudioClip ThrustSound;
    //Cache
    Rigidbody rb;
    AudioSource audioSource;
    //State

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        rb.freezeRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        BodyThrust();
        BodyRotate();
    }

    //Move when keydown
    void BodyThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            ApplyThrust(thrustAmount);
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(ThrustSound);
            }
        }
        //The code below stops other audioClips too so I had to go with another way
            // else{audioSource.Stop();}
        if(Input.GetKeyUp(KeyCode.Space))
        {
            audioSource.Stop();
        }
    }
    void BodyRotate()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotateAmount);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotateAmount);
        }
    }
    void ApplyThrust(float thrustAmount)
    {
        rb.AddRelativeForce(Vector3.up * thrustAmount * Time.deltaTime);
    }
    void ApplyRotation(float rotateAmount)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotateAmount * Time.deltaTime);
        rb.freezeRotation = false;
    }


}
