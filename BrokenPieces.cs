using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenPieces : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Vector3 moveDir;
    public float decceleration = 5f;
    public float lifetime = 5f;
    public SpriteRenderer body;
    void Start()
    {
        moveDir.x = Random.Range(-moveSpeed, moveSpeed);
        moveDir.y = Random.Range(-moveSpeed, moveSpeed);
    }
    void Update()
    {
        transform.position += moveDir * Time.deltaTime;
        moveDir = Vector3.Lerp(moveDir, Vector3.zero, decceleration * Time.deltaTime);
        lifetime -= Time.deltaTime;
        if(lifetime <= 2)
        {
            body.color = new Color(body.color.r, body.color.g, body.color.b, lifetime / 2f);
            if (lifetime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
