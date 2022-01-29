using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    /*
    This script is on every ball instance.

    When the ball spawns it finds all of it's refernces then randomly chooses a direction to launch at said angle.
    */
    

    private const int SPEED = 8;

    // Score board variables
    private TextMesh[] scoreText = new TextMesh[2];
    private int[] scoreNum = new int[2];

    // Refs to the ball's components itself
    private Rigidbody2D ballRB;
    private GameObject ball;
    

    // Finds all the references & instances to game objects before Start() is called
    private void Awake()
    {
        ballRB = gameObject.GetComponent<Rigidbody2D>();
        ball = gameObject;
        
        scoreText[0] = GameObject.Find("Score/Left").GetComponent<TextMesh>();
        scoreText[1] = GameObject.Find("Score/Right").GetComponent<TextMesh>();
        scoreNum[0] = int.Parse(scoreText[0].text);
        scoreNum[1] = int.Parse(scoreText[1].text);

        Singleton.Instance.ballRespawning = false;
    }


    private void Start()
    {
        RandomizeTrajectory();

        // Start the ball on it angle 
        ballRB.AddForce(ball.transform.right * SPEED, ForceMode2D.Impulse);
    }


    // Randomizes an angle within parameters and sets the balls rotation to it so that transform.right is the "forward"
    public Vector3 RandomizeTrajectory()
    {
        Vector3 ranAngle = new Vector3(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
        while(ranAngle.z >= 45 && ranAngle.z <= 135 || ranAngle.z >= 225 && ranAngle.z <= 305) // Keep generating an angle until it's satisfactory
        {
            ranAngle = new Vector3(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
        }
        ball.transform.Rotate(ranAngle);

        return ranAngle;
    }


    // Checks to see what the ball is colliding with
    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.gameObject.name);
        if(col.gameObject.name == "Wall_Left")
        {
            GameObject.Find("Score").GetComponent<Score_Animation>().SetScored(true);
            scoreNum[1]++;
            scoreText[1].text = scoreNum[1].ToString();
            GameObject.Find("Paddle_Controller").GetComponent<Paddle_Movement>().BallDeath();
            Singleton.Instance.ballRespawning = true;
            Destroy(this.gameObject);
        }
        else if(col.gameObject.name == "Wall_Right")
        {
            GameObject.Find("Score").GetComponent<Score_Animation>().SetScored(true);
            scoreNum[0]++;
            scoreText[0].text = scoreNum[0].ToString();
            GameObject.Find("Paddle_Controller").GetComponent<Paddle_Movement>().BallDeath();
            Singleton.Instance.ballRespawning = true;
            Destroy(this.gameObject);
        }
    }
}
