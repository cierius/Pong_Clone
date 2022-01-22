using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }

    // Game states
    public enum State
    {
        Menu,
        Single,
        Multi
    };

    public State curState = State.Menu;

    [SerializeField] private AudioSource musicObject;
    [SerializeField] private AudioClip music;
    private bool musicStarted = false;

    // Singleton pattern - only one instance of this object
    // Checks to see if there are multiple singleton gameobjects and if the Instance is set to itself
    private void Awake()
    {
        GameObject[] singletonInstances = GameObject.FindGameObjectsWithTag("Singleton");
        if(singletonInstances.Length > 1)
        {
            foreach(GameObject s in singletonInstances)
            {
                if(s != this.gameObject)
                {
                    Destroy(s);
                }
            }
        }

        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
        Application.targetFrameRate = 60;
    }


    public void SwitchToMenu()
    {
        print("Switching To Main Menu");
        curState = State.Menu;
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void EndGame()
    {
        print("End Game has occured - Returning to menu");

        SwitchToMenu();
    }

    public void StartMusic()
    {
        if(!musicStarted)
        {
            musicObject.PlayOneShot(music);
            musicStarted = true;
        }
    }
}
