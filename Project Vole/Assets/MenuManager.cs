using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {


    void Start()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
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
