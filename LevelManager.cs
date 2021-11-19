using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public float loadTime = 4f;
    public string nextLevel;
    public bool paused;
    public int currentCoins = 0;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        UIController.instance.coinText.text = currentCoins.ToString();
        Time.timeScale = 1;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }
    public IEnumerator LevelEnd()
    {
        PlayerController.instance.canMove = false;
        AudioManager.instance.PlayLevelWin();
        UIController.instance.StartFadeToBlack();
        yield return new WaitForSeconds(loadTime);
        SceneManager.LoadScene(nextLevel);
    }
    public void PauseUnpause()
    {
        if(!paused)
        {
            UIController.instance.pauseMenu.SetActive(true);
            paused = true;
            Time.timeScale = 0;
        } else
        {
            UIController.instance.pauseMenu.SetActive(false);
            paused = false;
            Time.timeScale = 1;
        }
    }
    public void AddCoins(int coinsAdded)
    {
        currentCoins += coinsAdded;
        UIController.instance.coinText.text = currentCoins.ToString();
    }
    public void RemoveCoins(int coinsTaken)
    {
        currentCoins -= coinsTaken;
        UIController.instance.coinText.text = currentCoins.ToString();
        if (currentCoins < 0)
            currentCoins = 0;
    }
}
