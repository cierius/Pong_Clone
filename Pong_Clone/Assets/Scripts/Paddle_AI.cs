using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle_AI : MonoBehaviour
{
    private const int SPEED = 5;
    private Transform ball = null;


    // Update is called once per frame
    void Update()
    {
        // Finds the ball in the scene
        if(ball == null)
        {
            ball = GameObject.Find("Ball").transform;
        } 

        Vector2 pos = transform.position;

        float dist = Vector2.Distance(new Vector2(pos.x, 0), new Vector2(ball.transform.position.x, 0));
        
        // Moves the paddle if the ball is within roughly about half of the screen, else the paddles doesn't move
        if(dist <= 6.0f)
        {
            if(ball.position.y > pos.y + 0.5f)
            {
                MoveUp();
            }
            else if(ball.position.y < pos.y - 0.5f)
            {
                MoveDown();
            }
        }
    }


    private void MoveUp()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + SPEED * Time.deltaTime);
    }


    private void MoveDown()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - SPEED * Time.deltaTime);
    }
}
