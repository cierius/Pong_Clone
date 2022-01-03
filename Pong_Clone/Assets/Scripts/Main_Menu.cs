using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    // Main menu button manager

    public void singlePlayer()
    {
        Debug.Log("Loading Singleplayer Scene");
        SceneManager.LoadScene("Single_Player", LoadSceneMode.Single);
    }

    public void localMultiplayer()
    {
        Debug.Log("Loading Local Multiplayer Scene");
        SceneManager.LoadScene("Local_Multiplayer", LoadSceneMode.Single);
    }

    public void exit()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
