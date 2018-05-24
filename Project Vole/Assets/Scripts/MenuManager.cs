using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {


    void Start()
    {
        if(PlayerPrefs.GetInt("hasRunBefore") == 0) {
			PlayerPrefs.SetInt("hasRunBefore", 1);
			PlayerPrefs.SetFloat("musicVolume", 0.5f);
			PlayerPrefs.SetFloat("soundFxVolume", 1f);
		}
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevel(string scenename) {
        SceneManager.LoadScene(scenename);
    }

    public void StartGame(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
    public void RestartGame(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
    public void ReturnMenu(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
}
