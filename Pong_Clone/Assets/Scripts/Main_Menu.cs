using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    // Simple Main Menu button manager


    public void SinglePlayer()
    {
        Debug.Log("Loading Singleplayer Scene");
        Singleton.Instance.curState = Singleton.State.Single;
        SceneManager.LoadScene("Single_Player", LoadSceneMode.Single);
    }

    public void LocalMultiplayer()
    {
        Debug.Log("Loading Local Multiplayer Scene");
        Singleton.Instance.curState = Singleton.State.Multi;
        SceneManager.LoadScene("Local_Multiplayer", LoadSceneMode.Single);
    }

    public void Exit()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
