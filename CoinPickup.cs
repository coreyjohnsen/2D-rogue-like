using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int value = 1;
    public float waitTime = 0.5f;
    private bool collectable = false;
    void Start()
    {
        
    }
    void Update()
    {
        if(waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            collectable = true;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            LevelManager.instance.AddCoins(value);
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(5);
        }
    }
}
