using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletSpeed = 7.5f;
    public GameObject impactFX;
    public GameObject hurtFX;
    public int damage = 30;
    void Start()
    {
        
    }
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * bulletSpeed;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        if (other.tag == "Enemy")
        {
            Instantiate(other.gameObject.GetComponent<EnemyController>().hurtFX, transform.position, transform.rotation);
            AudioManager.instance.PlaySFX(2);
        }
        else
        {
            Instantiate(impactFX, transform.position, transform.rotation);
            AudioManager.instance.PlaySFX(4);
        }
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().DamageEnemy(damage);
        }
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
