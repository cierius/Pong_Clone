using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Controller : MonoBehaviour
{
    
    private const int SPEED = 5;

    private GameObject ball;
    private bool flipDir = false; //Left = true, right = false
    private Vector3 movement = new Vector3(1, 0, 0);
    
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("Ball")){
            ball = GameObject.Find("Ball");
        }

        float ranNum = Random.Range(0.0f, 360.0f) * Mathf.Deg2Rad;;
        Debug.Log(ranNum);
    }

    // Update is called once per frame
    void Update()
    {
        //ball.transform.position += movement * Time.deltaTime;
    }
}
