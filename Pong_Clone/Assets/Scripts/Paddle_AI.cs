using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle_AI : MonoBehaviour
{
    private int SPEED = 5;
    private Transform ball = null;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ball == null)
        {
            ball = GameObject.Find("Ball").transform;
        } 

        Vector2 pos = transform.position;

        float dist = Vector2.Distance(new Vector2(pos.x, 0), new Vector2(ball.transform.position.x, 0));

        if(dist <= 7.5f)
        {
            pos.y = Mathf.Lerp(pos.y, ball.position.y, SPEED * Time.deltaTime);

            pos = new Vector2(pos.x, Mathf.Clamp(pos.y, GameObject.Find("Wall_Bottom").transform.position.y + 0.75f, GameObject.Find("Wall_Top").transform.position.y - 0.75f));

            transform.position = pos;
        }
    }
}
