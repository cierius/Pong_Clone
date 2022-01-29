using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    // Simple Main Menu button manager

    // UI Slider - For some reason it shows an error but it works... Unityengine.ui was depricated in 2019 but i cant find another way to get it to work.
    public Slider volSlider;
    public Text volText;

    private void Start()
    {
        LoadVolume();
    }

    // OnSliderChange
    public void SliderVolume(float vol)
    {
        Singleton.Instance.musicVol = vol;
        volText.text = (Singleton.Instance.musicVol * 100f).ToString("F0");
    }

    public void LoadVolume()
    {
        Singleton.Instance.musicVol = PlayerPrefs.GetFloat("Volume", 1.0f);
        volSlider.value = Singleton.Instance.musicVol;
        volText.text = (Singleton.Instance.musicVol * 100f).ToString("F0");
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("Volume", Singleton.Instance.musicVol);
        PlayerPrefs.Save();
    }

    // Switches scene to singleplayer
    public void SinglePlayer()
    {
        SaveVolume();
        Singleton.Instance.ballRespawning = true; // sets the ball back to respawning otherwise throws a null reference error
        
        Debug.Log("Loading Singleplayer Scene");
        Singleton.Instance.curState = Singleton.State.Single; // Sets singleton instance state
        SceneManager.LoadScene("Single_Player", LoadSceneMode.Single);
    }

    // Switches scene to 2-player
    public void LocalMultiplayer()
    {
        SaveVolume();
        Singleton.Instance.ballRespawning = true;

        Debug.Log("Loading Local Multiplayer Scene");
        Singleton.Instance.curState = Singleton.State.Multi;
        SceneManager.LoadScene("Local_Multiplayer", LoadSceneMode.Single);
    }

    // Exits game
    public void Exit()
    {
        SaveVolume();
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
