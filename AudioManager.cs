using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource level1Music, gameOverMusic, winMusic;
    public AudioSource[] sfx;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        gameOverMusic.Stop();
        winMusic.Stop();
    }
    void Update()
    {
        
    }
    public void PlayGameOver()
    {
        level1Music.Stop();
        gameOverMusic.Play();
    }
    public void PlayLevelWin()
    {
        level1Music.Stop();
        winMusic.Play();
    }
    public void PlaySFX(int index)
    {
        sfx[index].Stop();
        sfx[index].Play();
    }
}

