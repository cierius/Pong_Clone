using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle_Movement : MonoBehaviour
{
    /*
    This script controls the paddles from an empty GameObject using player input or enabling paddle_ai on said paddle
    */

    private const float SPEED = 5.0f;

    // Single player variables
    private bool pickedSide = false;
    private bool leftSide = false;

    // Prefabs that will be loaded to the game at runtime
    public GameObject redPaddlePrefab;
    public GameObject bluePaddlePrefab;
    public GameObject ballPrefab;

    // Declaration of references to game objects
    private GameObject redPaddle;
    private GameObject bluePaddle;
    private GameObject ball = null;

    // Ball spawning variables
    private bool ballSpawned = false;
    [SerializeField] float spawnDelay = 2.5f;

    // Single player and multiplayer text variables
    private GameObject[] tutorialText;
    private Color[] textColor;
    private bool fadeText = false;

    
    void Start()
    {
        // Find both of the paddles and assign them to variables for later control. Create them if they aren't present.
        if(GameObject.Find("Red_Paddle"))
        {
            redPaddle = GameObject.Find("Red_Paddle");
        }
        else
        {
            redPaddle = Instantiate(redPaddlePrefab);
            redPaddle.name = "Red_Paddle";
        }

        if(GameObject.Find("Blue_Paddle"))
        {
            bluePaddle = GameObject.Find("Blue_Paddle");
        }
        else
        {
            bluePaddle = Instantiate(bluePaddlePrefab);
            bluePaddle.name = "Blue_Paddle";
        }

        // If Singleplayer assign the tutorial text
        if(Singleton.Instance.curState == Singleton.State.Single)
        {
            textColor = new Color[3];
            tutorialText = new GameObject[3];

            tutorialText[0] = GameObject.Find("Canvas/Text_Left");
            tutorialText[1] = GameObject.Find("Canvas/Text_Middle");
            tutorialText[2] = GameObject.Find("Canvas/Text_Right");

            textColor[0] = tutorialText[0].GetComponent<CanvasRenderer>().GetColor();
            textColor[1] = tutorialText[1].GetComponent<CanvasRenderer>().GetColor();
            textColor[2] = tutorialText[2].GetComponent<CanvasRenderer>().GetColor();
        }
        // Assign multiplayer text variables
        else if(Singleton.Instance.curState == Singleton.State.Multi)
        {
            textColor = new Color[2];
            tutorialText = new GameObject[2];

            tutorialText[0] = GameObject.Find("Canvas/Text_Left");
            tutorialText[1] = GameObject.Find("Canvas/Text_Right");

            textColor[0] = tutorialText[0].GetComponent<CanvasRenderer>().GetColor();
            textColor[1] = tutorialText[1].GetComponent<CanvasRenderer>().GetColor();
        }

        // Spawns the ball in the main menu for the background "animation"
        if(Singleton.Instance.curState == Singleton.State.Menu)
        {
            if(ball == null)
            {
                SpawnBall();
            }
        }
    }


    void Update()
    {
        // Mostly for debug purposes, might get changed to a pause menu at some point
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Singleton.Instance.SwitchToMenu();
            Singleton.Instance.StopMusic();
        }

        // Respawns the ball in the main menu for the background "animation"
        if(Singleton.Instance.curState == Singleton.State.Menu)
        {
            if(ball == null && !ballSpawned)
            {
                SpawnBall();
                ballSpawned = true;
            }
        }

        // If multiplayer the paddles can move right away then it spawns ball after a defined spawn delay
        else if(Singleton.Instance.curState == Singleton.State.Multi)
        {
            Movement();

            if(ball == null && !ballSpawned)
            {
                Invoke("SpawnBall", spawnDelay);
                ballSpawned = true;
            }
            else if(ball != null && !fadeText) // Checks to see if the ball has spawned and if the text has been faded yet
            {
                    textColor[0].a = Mathf.Lerp(textColor[0].a, 0, 2.0f * Time.deltaTime);
                    textColor[1].a = Mathf.Lerp(textColor[1].a, 0, 2.0f * Time.deltaTime);

                    tutorialText[0].GetComponent<CanvasRenderer>().SetColor(textColor[0]);
                    tutorialText[1].GetComponent<CanvasRenderer>().SetColor(textColor[1]);

                    StartGame();
            }

        }
        // If singleplayer then wait for player to choose side then spawn ball after a delay and fade the text
        else if(Singleton.Instance.curState == Singleton.State.Single)
        {
            if(!pickedSide)
            {
                if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                {
                    pickedSide = true;
                    leftSide = true;
                    redPaddle.GetComponent<Paddle_AI>().enabled = true;
                }
                else if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
                {
                    pickedSide = true;
                    bluePaddle.GetComponent<Paddle_AI>().enabled = true;
                }
            }
            else if(pickedSide)
            {
                if(ball == null && !ballSpawned)
                {
                    Invoke("SpawnBall", spawnDelay);
                    ballSpawned = true;
                }
                
                if(!fadeText)
                {
                    textColor[0].a = Mathf.Lerp(textColor[0].a, 0, 2.0f * Time.deltaTime);
                    textColor[1].a = Mathf.Lerp(textColor[1].a, 0, 2.0f * Time.deltaTime);
                    textColor[2].a = Mathf.Lerp(textColor[2].a, 0, 2.0f * Time.deltaTime);

                    tutorialText[0].GetComponent<CanvasRenderer>().SetColor(textColor[0]);
                    tutorialText[1].GetComponent<CanvasRenderer>().SetColor(textColor[1]);
                    tutorialText[2].GetComponent<CanvasRenderer>().SetColor(textColor[2]);

                    StartGame();
                }
            }

            Movement();
        }

       
    }


    private void Movement()
    {
        //Keyboard input that moves the paddles
        if(Input.GetKey(KeyCode.W) && leftSide || Input.GetKey(KeyCode.W) && Singleton.Instance.curState == Singleton.State.Multi)
        {
            bluePaddle.transform.position += new Vector3(0, SPEED * Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.S) && leftSide || Input.GetKey(KeyCode.S) && Singleton.Instance.curState == Singleton.State.Multi)
        {
            bluePaddle.transform.position -= new Vector3(0, SPEED * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.UpArrow) && !leftSide || Input.GetKey(KeyCode.UpArrow) && Singleton.Instance.curState == Singleton.State.Multi)
        {
            redPaddle.transform.position += new Vector3(0, SPEED * Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.DownArrow) && !leftSide || Input.GetKey(KeyCode.DownArrow) && Singleton.Instance.curState == Singleton.State.Multi)
        {
            redPaddle.transform.position -= new Vector3(0, SPEED * Time.deltaTime);
        }

        Vector3 bPos = bluePaddle.transform.position;
        Vector3 rPos = redPaddle.transform.position;

        // Clamp that keeps the paddles inside of the boundary walls
        bluePaddle.transform.position = new Vector3(bPos.x, Mathf.Clamp(bPos.y, GameObject.Find("Wall_Bottom").transform.position.y + 0.75f, GameObject.Find("Wall_Top").transform.position.y - 0.75f), 0);
        redPaddle.transform.position = new Vector3(rPos.x, Mathf.Clamp(rPos.y, GameObject.Find("Wall_Bottom").transform.position.y + 0.75f, GameObject.Find("Wall_Top").transform.position.y - 0.75f), 0);
    }

    public void SpawnBall()
    {
        ball = Instantiate(ballPrefab);
        ball.name = "Ball";
    }

    public void BallDeath()
    {
        ballSpawned = false;
    }

    private void StartGame()
    {
        GameObject timerText = GameObject.Find("Game_Timer");
        timerText.GetComponent<Game_Timer>().countDown = true;

        Singleton.Instance.StartMusic();
    }

}
