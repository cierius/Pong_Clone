using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    // Simple Main Menu button manager

    // Switches scene to singleplayer
    public void SinglePlayer()
    {
        Singleton.Instance.ballRespawning = true; // sets the ball back to respawning otherwise throws a null reference error
        
        Debug.Log("Loading Singleplayer Scene");
        Singleton.Instance.curState = Singleton.State.Single; // Sets singleton instance state
        SceneManager.LoadScene("Single_Player", LoadSceneMode.Single);
    }

    // Switches scene to 2-player
    public void LocalMultiplayer()
    {
        Singleton.Instance.ballRespawning = true;

        Debug.Log("Loading Local Multiplayer Scene");
        Singleton.Instance.curState = Singleton.State.Multi;
        SceneManager.LoadScene("Local_Multiplayer", LoadSceneMode.Single);
    }

    // Exits game
    public void Exit()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
