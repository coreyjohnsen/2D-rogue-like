using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed = 7f;
    private Vector3 dir;
    public bool isHoming = false;
    void Start()
    {
        dir = PlayerController.instance.transform.position - transform.position;
        dir.Normalize();
    }
    void Update()
    {
        if (isHoming)
        {
            dir = PlayerController.instance.transform.position - transform.position;
            dir.Normalize();
        }
        transform.position += dir * bulletSpeed * Time.deltaTime;
    }
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerHealthController.instance.DecreaseHealth();
        }
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
