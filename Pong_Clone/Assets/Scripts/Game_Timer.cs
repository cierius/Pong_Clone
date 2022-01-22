using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Timer : MonoBehaviour
{
    [SerializeField] private GameObject timerText;
    private float timeLeft = 121; // Default is 2 min to go with music
    public bool countDown = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(countDown)
        {
            if(timeLeft > 0f)
            {
                timeLeft -= 1 * Time.deltaTime;
                timerText.GetComponent<TextMesh>().text = Mathf.Round(timeLeft).ToString();
            }
            else
            {
                Singleton.Instance.EndGame();
            }
        }
    }
}
