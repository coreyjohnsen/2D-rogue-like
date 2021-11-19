using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool closedRoom;
    public GameObject[] doors;
    [HideInInspector]
    public bool roomActive;
    public GameObject hider;
    void Start()
    {
        
    }
    void Update()
    {

    }
    public void OpenDoors()
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(false);
            closedRoom = false;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            roomActive = true;
            hider.SetActive(false);
            CameraController.instance.SetTarget(transform);
            if(closedRoom)
            {
                foreach(GameObject door in doors)
                {
                    door.SetActive(true);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
            roomActive = false;
    }
}
