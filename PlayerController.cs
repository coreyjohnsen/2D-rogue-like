using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public bool canMove = true;

    public static PlayerController instance;
    public float moveSpeed = 5f;
    private Vector2 moveInput;

    public Rigidbody2D rb;

    public Transform gunArm;
    private Camera mainCam;
    public GameObject playerBullet;
    public Transform firePoint;
    public float timeBetweenShots = .5f;
    private float shotCounter;

    public SpriteRenderer bodySR;

    private float activeMoveSpeed;
    public float dashSpeed = 8f, dashLength = .5f, dashCooldown = 2f, invincibilityLength = .5f;
    private float dashCooldownCounter;
    [HideInInspector] public float dashCounter;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        mainCam = Camera.main;
        activeMoveSpeed = moveSpeed;
}
    void Update()
    {
        if (canMove && !LevelManager.instance.paused)
        {
            GetMoveInput();
            MovePlayer();
            MoveArm();
            AnimatePlayer();
            DetectButtons();
        }
        else
        {
            rb.velocity = Vector2.zero;
            GetComponent<Animator>().SetBool("isMoving", false);
        }
    }
    private void AnimatePlayer()
    {
        if (moveInput != Vector2.zero)
        {
            GetComponent<Animator>().SetBool("isMoving", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("isMoving", false);
        }
    }
    private void GetMoveInput()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
        if(Input.GetKeyDown(KeyCode.Space) && dashCooldownCounter <= 0 && dashCounter <= 0)
        {
            GetComponent<Animator>().SetTrigger("Dash");
            activeMoveSpeed = dashSpeed;
            AudioManager.instance.PlaySFX(8);
            PlayerHealthController.instance.MakeInvincible(invincibilityLength);
            dashCounter = dashLength;
        }
        if(dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if(dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCooldownCounter = dashCooldown;
            }
        }
        if(dashCooldownCounter > 0)
        {
            dashCooldownCounter -= Time.deltaTime;
        }
    }
    private void MovePlayer()
    {
        rb.velocity = moveInput * activeMoveSpeed;
    }
    private void MoveArm()
    {
        Vector3 mousePos = Input.mousePosition; // mouse pos
        Vector3 screenPos = mainCam.WorldToScreenPoint(transform.localPosition); // player pos
        SwitchPlayerDirection(mousePos, screenPos);
        // rotate gun arm
        Vector2 offset = new Vector2(mousePos.x - screenPos.x, mousePos.y - screenPos.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gunArm.rotation = Quaternion.Euler(0, 0, angle);
    }
    private void SwitchPlayerDirection(Vector3 mousePos, Vector3 screenPos)
    {
        if (mousePos.x < screenPos.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            gunArm.localScale = new Vector3(-1, -1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            gunArm.localScale = new Vector3(1, 1, 1);
        }
    }
    private void DetectButtons()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(playerBullet, firePoint.position, firePoint.rotation);
            AudioManager.instance.PlaySFX(12);
            shotCounter = timeBetweenShots;
        }
        if (Input.GetMouseButton(0))
        {
            shotCounter -= Time.deltaTime;
            if( shotCounter <= 0)
            {
                Instantiate(playerBullet, firePoint.position, firePoint.rotation);
                AudioManager.instance.PlaySFX(12);
                shotCounter = timeBetweenShots;
            }
        }
    }
}
