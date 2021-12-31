using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    
    private GameObject[] scoreObj = new GameObject[2];
    private int[] scoreNum = new int[2] {0, 0};
    
    void Awake()
    {
        scoreObj[0] = GameObject.Find("Score/Left");
        scoreObj[1] = GameObject.Find("Score/Right");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.gameObject.name);
        if(col.gameObject.name == "Wall_Left")
        {
            scoreNum[0]++;
            scoreObj[0].GetComponent<TextMesh>().text = scoreNum[0].ToString();
        }
        else if(col.gameObject.name == "Wall_Right")
        {
            scoreNum[1]++;
            scoreObj[1].GetComponent<TextMesh>().text = scoreNum[1].ToString();
        }
    }
}
