using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_Animation : MonoBehaviour
{
    // Variables for animating the score board via code.
    private Vector2 pos;
    private Vector2 dest = new Vector2(0, 0);
    private Vector2 home;


    [SerializeField]
    private float lerpSpeed = 5.0f;
    private bool hasScored = false;
    public bool scoreAble = true;
    private float scoreTimer = 0;
    
    [SerializeField]
    private float onscreenTime = 5.0f; //Time in seconds for the scoreboard to be on the screen

    private void Awake()
    {
        // Starting position for the scoreboard = above the wall where it spawns at
        home = transform.position;
    }


    private void Score()
    {
        if(scoreTimer <= onscreenTime)
        {
            scoreTimer += Time.deltaTime;

            pos = transform.position;
            transform.position = new Vector2(pos.x, Mathf.SmoothStep(pos.y, dest.y, lerpSpeed));
        }
        else
        {
            hasScored = false;
            scoreTimer = 0;
        }
    }

    public void ReverseScore()
    {
        pos = transform.position;
        transform.position = new Vector2(pos.x, Mathf.SmoothStep(pos.y, home.y, lerpSpeed));
    }

    // if true will cause the score to come down, flase brings it back up
    public void SetScored(bool s)
    {
        if(scoreAble)
        {
            hasScored = s;
        }
    }

    private void Update()
    {
        if(hasScored)
        {
            Score();
        }
        else if(!hasScored)
        {
            ReverseScore();
        }
    }
}
