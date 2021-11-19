using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameObject[] brokenPeices;
    public bool dropsEnabled = false;
    public GameObject[] drops;
    public float itemDropPercent;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if((other.tag == "Player" && PlayerController.instance.dashCounter > 0) || other.tag == "PlayerBullet")
        {
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(0);
            for (int i = 0; i < Random.Range(3, 5); i++)
            {
                int randomRot = Random.Range(0, 4);
                Vector3 randomPos = new Vector3(transform.position.x + Random.Range(-50f, 50f) / 100f, transform.position.y + Random.Range(-50f, 50f) / 100f, 0);
                Instantiate(brokenPeices[Random.Range(0, brokenPeices.Length)], randomPos, Quaternion.Euler(0, 0, randomRot * 90));
            }
            if (dropsEnabled && Random.Range(0f, 100f) <= itemDropPercent)
            {
                Instantiate(drops[Random.Range(0, drops.Length)], transform.position, transform.rotation);
            }
        }
    }
}
