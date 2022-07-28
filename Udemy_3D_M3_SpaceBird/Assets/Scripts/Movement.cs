using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Parameters
    [SerializeField] float thrustAmount = 1200f;
    [SerializeField] float rotateAmount = 80f;
    [SerializeField] AudioClip ThrustSound;
    [SerializeField] ParticleSystem mainThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;
    //Cache
    Rigidbody rb;
    AudioSource audioSource;

    bool isThrusting;

    //State

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        isThrusting = false;
    }

    // Update is called once per frame
    void Update()
    {
        BodyThrust();
        BodyRotate();
        ThrustingSoundEnabler();
        quitGame();

    }

    //Move when keydown
    void BodyThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            isThrusting  = true;
            ApplyThrust(thrustAmount);

            if(!mainThrustParticles.isPlaying)
            {
                mainThrustParticles.Play();
            }
        }
        //The code below stops other audioClips too so I had to go with another way
            // else{audioSource.Stop();}
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            isThrusting = false;
            mainThrustParticles.Stop();
        }
    }
    void BodyRotate()
    {
        ThrustLeft();
        ThrustRight();
    }

    void ThrustLeft()
    {
        if(Input.GetKey(KeyCode.A))
        {
            isThrusting = true;
            if(!rightThrustParticles.isPlaying)
            {
                rightThrustParticles.Play();
            }
            ApplyRotation(rotateAmount);
            
        }
        else if(Input.GetKeyUp(KeyCode.A))
        {
            isThrusting = false;
            rightThrustParticles.Stop();
        }
    }

    void ThrustRight()
    {
        if(Input.GetKey(KeyCode.D))
        {   
            isThrusting = true;
            if(!leftThrustParticles.isPlaying)
            {
                leftThrustParticles.Play();
            }
            ApplyRotation(-rotateAmount);

        }
        else if(Input.GetKeyUp(KeyCode.D))
        {
            isThrusting = false;
            leftThrustParticles.Stop();
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
    void ThrustingSoundEnabler()
    {
        if (isThrusting == true)
        {
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(ThrustSound);
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    void quitGame()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
