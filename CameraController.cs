using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public float moveSpeed;
    public Transform target;
    public Camera bigCam;
    public Camera mainCam;
    public bool bigMapActive;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }
    void Update()
    {
        if(target != null)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        if(Input.GetKeyDown(KeyCode.M))
        {
            if(bigMapActive && Time.timeScale == 0)
            {
                DeactivateBigMap();
            } else if (Time.timeScale == 1)
            {
                ActivateBigMap();
            }
        }
    }
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
    public void ActivateBigMap()
    {
        bigMapActive = true;
        bigCam.enabled = true;
        mainCam.enabled = false;
        UIController.instance.minimap(false);
        PlayerController.instance.canMove = false;
        if (Time.timeScale == 1)
            Time.timeScale = 0;
    }
    public void DeactivateBigMap()
    {
        bigMapActive = false;
        bigCam.enabled = false;
        mainCam.enabled = true;
        UIController.instance.minimap(true);
        PlayerController.instance.canMove = true;
        if (Time.timeScale == 0)
            Time.timeScale = 1;
    }
}
