using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    
    private TextMesh[] scoreText = new TextMesh[2];
    private int[] scoreNum = new int[2];

    [SerializeField]
    private int SPEED = 5;
    private Rigidbody2D ballRB;
    private GameObject ball;
    
    void Awake()
    {
        ballRB = gameObject.GetComponent<Rigidbody2D>();
        ball = gameObject;
        scoreText[0] = GameObject.Find("Score/Left").GetComponent<TextMesh>();
        scoreText[1] = GameObject.Find("Score/Right").GetComponent<TextMesh>();
        scoreNum[0] = int.Parse(scoreText[0].text);
        scoreNum[1] = int.Parse(scoreText[1].text);
    }


    void Start()
    {
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


    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.gameObject.name);
        if(col.gameObject.name == "Wall_Left")
        {
            GameObject.Find("Score").GetComponent<Score_Animation>().setScored(true);
            scoreNum[1]++;
            scoreText[1].text = scoreNum[1].ToString();
            Destroy(this.gameObject);
        }
        else if(col.gameObject.name == "Wall_Right")
        {
            GameObject.Find("Score").GetComponent<Score_Animation>().setScored(true);
            scoreNum[0]++;
            scoreText[0].text = scoreNum[0].ToString();
            Destroy(this.gameObject);
        }
    }
}
