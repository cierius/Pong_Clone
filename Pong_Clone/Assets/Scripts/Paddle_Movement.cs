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

    public bool inverseControl = false;
    public GameObject redPaddlePrefab;
    public GameObject bluePaddlePrefab;
    public GameObject ballPrefab;
    private GameObject redPaddle;
    private GameObject bluePaddle;
    private GameObject ball;

    
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
        
        // Find ball or create one if not present.
        if(GameObject.Find("Ball"))
        {
            ball = GameObject.Find("Ball");
        }
        else
        {
            ball = Instantiate(ballPrefab);
            ball.name = "Ball";
        }
    }


    void Update()
    {
        //Keyboard input that moves the paddles - Inverse control swaps the side of which is controlled
        if(Input.GetKey(KeyCode.W) && !inverseControl || Input.GetKey(KeyCode.UpArrow) && inverseControl)
        {
            bluePaddle.transform.position += new Vector3(0, SPEED * Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.S) && !inverseControl || Input.GetKey(KeyCode.DownArrow) && inverseControl)
        {
            bluePaddle.transform.position -= new Vector3(0, SPEED * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.UpArrow) && !inverseControl || Input.GetKey(KeyCode.W) && inverseControl)
        {
            redPaddle.transform.position += new Vector3(0, SPEED * Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.DownArrow) && !inverseControl || Input.GetKey(KeyCode.S) && inverseControl)
        {
            redPaddle.transform.position -= new Vector3(0, SPEED * Time.deltaTime);
        }

        Vector3 bPos = bluePaddle.transform.position;
        Vector3 rPos = redPaddle.transform.position;
        // Very messy clamping to keep the paddles inside the walls. Need to clean up.
        bluePaddle.transform.position = new Vector3(bPos.x, Mathf.Clamp(bPos.y, GameObject.Find("Wall_Bottom").transform.position.y + 0.75f, GameObject.Find("Wall_Top").transform.position.y - 0.75f), 0);
        redPaddle.transform.position = new Vector3(rPos.x, Mathf.Clamp(rPos.y, GameObject.Find("Wall_Bottom").transform.position.y + 0.75f, GameObject.Find("Wall_Top").transform.position.y - 0.75f), 0);
    }
}
