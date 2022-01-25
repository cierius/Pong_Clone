using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Timer : MonoBehaviour
{
    [SerializeField] private GameObject timerText;
    public float timeLeft = 121; // Default is 121sec to go with music
    public bool countDown = false;
    public bool scoreScreen = false;


    // Update is called once per frame
    void Update()
    {
        if(countDown)
        {
            if(timeLeft > 0f)
            {
                timeLeft -= Time.deltaTime;
                timerText.GetComponent<TextMesh>().text = Mathf.Round(timeLeft).ToString();
            }
            else if(scoreScreen)
            {
                Singleton.Instance.EndGame();
            }
            else 
            {
                Singleton.Instance.ShowScoreboard();
            }
        }
    }
}
