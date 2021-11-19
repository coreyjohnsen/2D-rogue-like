using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("Chasing Player/General Movement")]
    public float moveSpeed = 3f;
    public bool shouldChase;
    public float rangeToChase = 10f;
    private Vector3 moveDir;

    [Header("Health and Death")]
    public int health = 150;

    public GameObject[] deathFXs;
    public GameObject hurtFX;

    [Header("Shooting Behavior")]
    public bool shootingEnemy;
    public GameObject bullet;
    public Transform firePoint;
    public float secondsBetweenShots;
    private float fireCounter;
    public SpriteRenderer sprite;
    public float shootRange = 10f;

    [Header("Run Away")]
    public bool runAway;
    public float runRange;

    [Header("Damager Player On Contact")]
    public bool damagePlayerOnContact;

    [Header("Wander")]
    public bool shouldWander;
    public float wanderTime, stopTime;
    private float wanderCounter, stopCounter;
    private Vector3 wanderDir;

    [Header("EnemyDrops")]
    public bool dropsEnabled = false;
    public GameObject[] drops;
    public float itemDropPercent;

    void Awake() 
    {

    }
    void Start()
    {
        if(shouldWander)
            stopCounter = Random.Range(stopTime * .75f, stopTime * 1.5f);
    }
    void Update()
    {
        if (sprite.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {
            EnemyMove();
            Shoot();
        } else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void Shoot()
    {
        if (shootingEnemy && Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= shootRange)
        {
            fireCounter -= Time.deltaTime;
            if (fireCounter <= 0)
            {
                fireCounter = secondsBetweenShots;
                Instantiate(bullet, firePoint.position, firePoint.rotation);
                AudioManager.instance.PlaySFX(13);
            }
        }
    }

    private void EnemyMove()
    {

        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= rangeToChase && shouldChase)
        {
            moveDir = PlayerController.instance.transform.position - transform.position;
            GetComponent<Animator>().SetBool("isWalking", true);
        }
        else if(runAway && Vector3.Distance(transform.position, PlayerController.instance.transform.position) <= runRange)
        {
            moveDir = -(PlayerController.instance.transform.position - transform.position);
            GetComponent<Animator>().SetBool("isWalking", true);
        } 
        else if (shouldWander)
        {
            if (wanderCounter > 0)
            {
                wanderCounter -= Time.deltaTime;
                moveDir = wanderDir;
                GetComponent<Animator>().SetBool("isWalking", true);
                if (wanderCounter <= 0)
                {
                    stopCounter = Random.Range(stopTime * .75f, stopTime * 1.5f);
                }
            }
            if (stopCounter > 0)
            {
                stopCounter -= Time.deltaTime;
                GetComponent<Animator>().SetBool("isWalking", false);
                moveDir = Vector3.zero;
                if (stopCounter <= 0)
                {
                    wanderCounter = Random.Range(wanderTime * .75f, wanderTime * 1.5f);
                    wanderDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                }
            }
        }
        else
        {
            moveDir = Vector3.zero;
            GetComponent<Animator>().SetBool("isWalking", false);
        }
        moveDir.Normalize();
        rb.velocity = moveDir * moveSpeed;
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
            if (dropsEnabled && Random.Range(0f, 100f) <= itemDropPercent)
            {
                Instantiate(drops[Random.Range(0, drops.Length)], transform.position, transform.rotation);
            }
            int rotation = Random.Range(0, 4) * 90;
            Instantiate(deathFXs[Random.Range(0,deathFXs.Length)], transform.position, Quaternion.Euler(0, 0, rotation));
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.tag == "Player" && damagePlayerOnContact)
        {
            PlayerHealthController.instance.DecreaseHealth();
        }
    }
}
