using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Slider healthBar;
    public Text healthText;
    public GameObject deathScreen;
    public Image fadeScreen;
    public float fadeSpeed;
    private bool fadeIn;
    private bool fadeOut;
    public string newGame, mainMenu;
    public GameObject pauseMenu;
    public Text coinText;
    public GameObject Minimap;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        fadeOut = true;
        fadeIn = false;
    }
    void Update()
    {
        if(fadeOut)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 0)
            {
                fadeOut = false;
            }
        }
        if (fadeIn)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1)
            {
                fadeIn = true;
            }
        }
    }
    public void StartFadeToBlack()
    {
        fadeIn = true;
        fadeOut = false;
    }
    public void NewGame()
    {
        SceneManager.LoadScene(newGame);
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
    public void Resume()
    {
        LevelManager.instance.PauseUnpause();
    }
    public void minimap (bool isOn)
    {
        Minimap.SetActive(isOn);
    }
}
