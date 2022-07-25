using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioClip ExplosionSound;
    [SerializeField] AudioClip SuccessSound;

    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Outcomes for different collisions
    private void OnCollisionEnter(Collision other) {
        switch(other.gameObject.tag)
        {
            case "Respawn": 
                break;
            case "Finish": 
                FinishSequence();
                break;
            case "Obstacle": 
                CrashSequence();
                break;
            default:
                break;
        }
    }
    //Play audio ExplosionSound, disable Movement Script, Invoke ReloadLevel after 1 second
    void CrashSequence()
    {
        audioSource.PlayOneShot(ExplosionSound);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", 1f);
    }

    void FinishSequence()
    {
        audioSource.PlayOneShot(SuccessSound);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", 1f);
        
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
