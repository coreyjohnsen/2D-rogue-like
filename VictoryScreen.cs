using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public string levelToLoad = "MainMenu";
    public float wait = 2f;
    public GameObject anyKeyText;
    void Start()
    {
        Time.timeScale = 1f;
    }
    void Update()
    {
        if(wait > 0)
        {
            wait -= Time.deltaTime;
            if(wait <= 0)
            {
                anyKeyText.SetActive(true);
            }
        } 
        else
        {
            if (Input.anyKeyDown)
                SceneManager.LoadScene(levelToLoad);
        }
    }
    void LoadMainMenu()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
