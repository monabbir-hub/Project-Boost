using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    public float delayTime = 1f;
    public AudioClip crash;
    public AudioClip success;

    public ParticleSystem crashParticles;
    public ParticleSystem successParticles;

    AudioSource sfx;
    bool isTransitioning = false;
    bool collisionDisable = false;

    private void Start() {
        sfx = GetComponent<AudioSource>();
    }


    private void Update() {
        
        RespondToDebugKeys();
    }


    void RespondToDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable;  //toggle collision
        }
    }

    
    private void OnCollisionEnter(Collision other)
    {
        if(isTransitioning || collisionDisable)
        {
            return;
        }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is Friendly");
                break;
            case "Finish":
                StartSuccesSequence();
                break;            
            default:
                StartCrashSequence();
                break;

        }
    }

    private void StartSuccesSequence()
    {
        isTransitioning = true;        
        //sfx audio
        sfx.Stop();
        sfx.PlayOneShot(success);

        successParticles.Play();

        //load level
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delayTime);
    }

    void StartCrashSequence() 
    {
        isTransitioning = true;
        //sfx audio
        sfx.Stop();
        sfx.PlayOneShot(crash);

        crashParticles.Play();
        //restart level
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayTime);
    }




    void LoadNextLevel()
    {
        int currentlevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextlevelIndex = currentlevelIndex + 1;

        if(nextlevelIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextlevelIndex = 0;
        }

        SceneManager.LoadScene(nextlevelIndex);
    }

    void ReloadLevel()
    {
        int currentlevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentlevelIndex);
    }




}
