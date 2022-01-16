using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle_Movement : MonoBehaviour
{
    /*
    This script controls the paddles from an empty GameObject.
    */

    [SerializeField]
    private float SPEED = 5.0f;

    private bool pickedSide = false;
    private bool leftSide = false;

    public bool inverseControl = false;
    public GameObject redPaddlePrefab;
    public GameObject bluePaddlePrefab;
    public GameObject ballPrefab;
    private GameObject redPaddle;
    private GameObject bluePaddle;
    private GameObject ball = null;

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
        // Mostly for debug purposes, will get changed to a pause menu at some point
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Singleton.Instance.SwitchToMenu();
        }

        // Respawns the ball in the main menu for the background "animation" if it scores
        if(Singleton.Instance.curState == Singleton.State.Menu)
        {
            if(ball == null)
            {
                SpawnBall();
            }
        }

        // If multiplayer the paddles can move right away then it spawns ball after 5 seconds
        if(Singleton.Instance.curState == Singleton.State.Multi)
        {
            Movement();

            if(ball == null)
            {
                SpawnBall();
            }
        }
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
                if(ball == null)
                {
                    SpawnBall();
                }
                
                if(!fadeText)
                {
                    textColor[0].a = Mathf.Lerp(textColor[0].a, 0, 2.0f * Time.deltaTime);
                    textColor[1].a = Mathf.Lerp(textColor[1].a, 0, 2.0f * Time.deltaTime);
                    textColor[2].a = Mathf.Lerp(textColor[2].a, 0, 2.0f * Time.deltaTime);

                    tutorialText[0].GetComponent<CanvasRenderer>().SetColor(textColor[0]);
                    tutorialText[1].GetComponent<CanvasRenderer>().SetColor(textColor[1]);
                    tutorialText[2].GetComponent<CanvasRenderer>().SetColor(textColor[2]);
                }
            }

            Movement();
        }

        Vector3 bPos = bluePaddle.transform.position;
        Vector3 rPos = redPaddle.transform.position;
        // Very messy clamping to keep the paddles inside the walls. Need to clean up.
        bluePaddle.transform.position = new Vector3(bPos.x, Mathf.Clamp(bPos.y, GameObject.Find("Wall_Bottom").transform.position.y + 0.75f, GameObject.Find("Wall_Top").transform.position.y - 0.75f), 0);
        redPaddle.transform.position = new Vector3(rPos.x, Mathf.Clamp(rPos.y, GameObject.Find("Wall_Bottom").transform.position.y + 0.75f, GameObject.Find("Wall_Top").transform.position.y - 0.75f), 0);
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
    }

    public void SpawnBall()
    {
        ball = Instantiate(ballPrefab);
        ball.name = "Ball";
    }
}
