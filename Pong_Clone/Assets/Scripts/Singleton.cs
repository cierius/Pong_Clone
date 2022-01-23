using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton : MonoBehaviour
{
    // The universal instance to this class
    public static Singleton Instance { get; private set; } = null;

    // Game states
    public enum State { Menu, Single, Multi };
    public State curState = State.Menu; // Default state is menu

    // Audio variables
    [SerializeField] private AudioSource musicObject;
    [SerializeField] private AudioClip music;
    private bool musicStarted = false;
    public bool musicMute = false; // Default false
    public float musicVol = 1.0f; // Default 1.0f


    // Ball variables
    public bool ballRespawning = true; // Default true because ball has a delayed spawn

    /* Singleton pattern - only one instance of this object
       Checks to see if there are multiple singleton gameobjects and if the Instance is set to itself.
       If there are multiples it will delete the others.
    */
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        Application.targetFrameRate = 60;
    }

    // This function was used for debugging purposes with the ESC key - may be turned into a pause button later on
    public void SwitchToMenu()
    {
        print("Switching To Main Menu");
        curState = State.Menu;
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    // Called when the timer hits 0
    // May take a player to a score screen at some point, currently just returns to main menu
    public void EndGame()
    {
        print("End Game has occured - Returning to menu");

        SwitchToMenu();
    }

    // Janky music function for the time being
    public void StartMusic()
    {
         if(musicMute == true)
        {
            musicObject.mute = true;
        }
        musicObject.volume = musicVol;

        if(!musicStarted)
        {
            musicObject.PlayOneShot(music);
            musicStarted = true;
        }
    }
}
