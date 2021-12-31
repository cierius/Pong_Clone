using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Controller : MonoBehaviour
{
    
    private const int SPEED = 5;

    private GameObject[] score = new GameObject[2];

    private Rigidbody2D ballRB;
    private GameObject ball;
    private Vector3 movement = new Vector3(1, 0, 0);
    
    // Start is called before the first frame update
    void Start()
    {
        score[0] = GameObject.Find("Score/Left");
        score[1] = GameObject.Find("Score/Right");
        ball = GameObject.Find("Ball");
        ballRB = GetComponent<Rigidbody2D>();

        // Generate random starting trajectory
        // If traj is not satisfactory re-randomize it until it is
        Vector3 ranAngle = new Vector3(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
        while(ranAngle.z >= 45 && ranAngle.z <= 135 || ranAngle.z >= 225 && ranAngle.z <= 305)
        {
            ranAngle = new Vector3(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
        }
        ball.transform.Rotate(ranAngle);

        // Start the ball on it angle 
        ballRB.AddForce(ball.transform.right * SPEED, ForceMode2D.Impulse);
    }
}
