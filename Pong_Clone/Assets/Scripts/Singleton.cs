using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton : MonoBehaviour
{
    // The universal instance to this class
    public static Singleton Instance { get; private set; } = null;

    // Game states
    public enum State { Menu, Single, Multi, Score };
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

    //TODO Comment this function
    public void ShowScoreboard()
    {
        Instance.curState = State.Score;

        int[] scores = new int[2] {int.Parse(GameObject.Find("Score/Left").GetComponent<TextMesh>().text), int.Parse(GameObject.Find("Score/Right").GetComponent<TextMesh>().text)};
        
        var scoreboard = GameObject.Find("Score_Screen");
        scoreboard.GetComponent<SpriteRenderer>().enabled = true;
        MeshRenderer[] childrenMesh = scoreboard.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer m in childrenMesh)
        {
            m.enabled = true;
        }

        GameObject.Find("Score_Screen/Blue_Score_Num").GetComponent<TextMesh>().text = scores[0].ToString();
        GameObject.Find("Score_Screen/Red_Score_Num").GetComponent<TextMesh>().text = scores[1].ToString();

        var timer = GameObject.Find("Game_Timer").GetComponent<Game_Timer>();
        timer.timeLeft = 15;
        timer.countDown = true;
        timer.scoreScreen = true;

        
        if(scores[0] > scores[1])
        {
            GameObject.Find("Score_Screen/Winner_Text").GetComponent<TextMesh>().text = "Blue Paddle Wins!";
        }
        else if(scores[0] < scores[1])
        {
            GameObject.Find("Score_Screen/Winner_Text").GetComponent<TextMesh>().text = "Red Paddle Wins!";
        }
        else
        {
            GameObject.Find("Score_Screen/Winner_Text").GetComponent<TextMesh>().text = "Tie!";
        }

        GameObject.Find("Score").GetComponent<Score_Animation>().ReverseScore();
        GameObject.Find("Score").GetComponent<Score_Animation>().scoreAble = false;

        GameObject.Find("Red_Paddle").GetComponent<Paddle_AI>().enabled = true;
        GameObject.Find("Blue_Paddle").GetComponent<Paddle_AI>().enabled = true;
        GameObject.Find("Red_Paddle").GetComponent<Paddle_Movement>().enabled = false;
        GameObject.Find("Blue_Paddle").GetComponent<Paddle_Movement>().enabled = false;
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

    public void StopMusic()
    {
        musicObject.Stop();
    }
}
