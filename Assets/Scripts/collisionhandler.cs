using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class collision : MonoBehaviour
{
    [SerializeField]float delay = 1.5f; 
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip levelcomplete;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem levelCompleteParticles;
    AudioSource audioSource;
    bool iscontrollable = true;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update() {
        RespondToDebugKeys();
    }
    void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            NextLevel();
            
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (!iscontrollable) { return; } //if the player is not alive, return
        switch (other.gameObject.tag)
        {
            case "Friendly":
                
                break;

            case "Finish":
                NextLevelSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void NextLevelSequence()
    {
        iscontrollable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(levelcomplete); //plays the level complete sound
        levelCompleteParticles.Play(); //plays the level complete particles
        GetComponent<movment>().enabled = false;
        Invoke("NextLevel", 1f); 
    }

    private void StartCrashSequence()
    {
        iscontrollable = false; 
        audioSource.Stop();
        audioSource.PlayOneShot(crash); //plays the crash sound
        crashParticles.Play(); //plays the crash particles
        GetComponent<movment>().enabled = false; //disables the movment script
        Invoke("ReloadLevel", 1.5f); //reloads the level after 1 second
    }

    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //gets index of the current scene
        int nextSceneIndex = currentSceneIndex + 1; //gets index of the next scene
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) //checks if the next scene index is less than the total number of scenes in the build settings
        {
            SceneManager.LoadScene(nextSceneIndex); //loads the next scene
        }
        else
        {
            currentSceneIndex = 0; //sets the current scene index to 0
            SceneManager.LoadScene(currentSceneIndex); //loads the first scene
        }
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //gets index of the current scene
        SceneManager.LoadScene(currentSceneIndex); //reloads the current scene

    }
}

