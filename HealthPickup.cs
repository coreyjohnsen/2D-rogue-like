using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 1;
    public float waitCollectTime = .5f;
    void Start()
    {
        
    }
    void Update()
    {
        if(waitCollectTime > 0)
        {
            waitCollectTime -= Time.deltaTime;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && waitCollectTime <= 0)
        {
            PlayerHealthController.instance.HealPlayer(healAmount);
            Destroy(gameObject);
        }
    }
}
