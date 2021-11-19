using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCenter : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public bool openedWhenEnemiedCleared;
    public Room room;
    void Start()
    {
        if(openedWhenEnemiedCleared)
        {
            room.closedRoom = true;
        }
    }
    void Update()
    {
        if (enemies.Count > 0 && room.roomActive && openedWhenEnemiedCleared)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }
            if (enemies.Count == 0)
            {
                room.OpenDoors();
            }
        }
    }
}
