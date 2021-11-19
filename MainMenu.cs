using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;
    void Start()
    {
        Time.timeScale = 1f;
    }
    void Update()
    {
        
    }
    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
