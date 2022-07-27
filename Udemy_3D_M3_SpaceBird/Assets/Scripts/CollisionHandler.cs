using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioClip explosionSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem mainThrusterParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;

    AudioSource audioSource;
    Rigidbody rb;

    bool isAlternating;
    bool collisionEnabled;
    bool freezeRbConstrains;
    void Start()
    {
        isAlternating = false;
        collisionEnabled = true;
        freezeRbConstrains = true;
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CheatJumpNextLevel();
        CheatCollisionEnabler();
        FreezeRbConstrains(freezeRbConstrains);
    }
    
    void CheatJumpNextLevel()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            LoadNextLevel();
            Debug.Log("n pressed");
        }
    }

    void CheatCollisionEnabler()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            collisionEnabled = !collisionEnabled;
        }
        if(collisionEnabled == false)
        {
           FreezeRbConstrains(true);
        }
    }
    
    //Outcomes for different collisions
    void OnCollisionEnter(Collision other) {
        if (isAlternating == false)
        {
            switch(other.gameObject.tag)
            {
                case "Respawn": 
                    break;
                case "Finish": 
                    FinishSequence();
                    break;
                case "Obstacle": 
                    CrashSequence();
                    other.rigidbody.freezeRotation= false;
                    break;
                case "Friendly":
                    FriendlySequence();
                    break;
                default:
                    break;
            }
        }
        else{return;}
    }
    //Play audio ExplosionSound, disable Movement Script, Invoke ReloadLevel after 1 second
    void FinishSequence()
    {
        isAlternating = true;
        freezeRbConstrains = false;
        successParticles.Play();
        audioSource.PlayOneShot(successSound);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", 1f);
        
    }
    void CrashSequence()
    {
        if(collisionEnabled == true)
        {
            isAlternating = true;
            freezeRbConstrains = false;
            explosionParticles.Play();
            audioSource.PlayOneShot(explosionSound, 0.35f);
            GetComponent<Movement>().enabled = false;
            Invoke("ReloadLevel", 1f);
        }
    }

    void FriendlySequence()
    {
        freezeRbConstrains = true;
    }

    void FreezeRbConstrains(bool freezeRbConstrains)
    {
        if(freezeRbConstrains == true)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionZ;
        }
        if(freezeRbConstrains == false)
        {
            rb.freezeRotation = false;
        }
    }


    

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalSceneIndex = SceneManager.sceneCountInBuildSettings;

        if(currentSceneIndex == totalSceneIndex-1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(currentSceneIndex+1);
        }
    }
}
