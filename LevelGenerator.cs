using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    public GameObject layoutRoom;
    public Color start;
    public Color end, shopColor;
    public int distanceToEnd;
    public bool includeShop;
    public int minDistanceToShop;
    public int maxDistanceToShop;
    public Transform generatePoint;
    public enum Direction { up, right, down, left };
    public Direction selectedDir;
    public float xOffset = 18, yOffset = 10;
    public LayerMask whatIsRoom;
    private GameObject endRoom, shopRoom;
    private List<GameObject> roomObjects = new List<GameObject>();
    public RoomPrefabs rooms;
    private List<GameObject> generatedOutlines = new List<GameObject>();
    public RoomCenter startCenter, endCenter, shopCenter;
    public RoomCenter[] centers;
    void Start()
    {
       Instantiate(layoutRoom, generatePoint.position, generatePoint.rotation).GetComponent<SpriteRenderer>().color = start;
       selectedDir = (Direction) Random.Range(0, 4);
       MoveGenerationPoint();
       for(int i = 0; i < distanceToEnd; i++)
       {
            GameObject newRoom = Instantiate(layoutRoom, generatePoint.position, generatePoint.rotation);
            roomObjects.Add(newRoom);
            if(i + 1 == distanceToEnd)
            {
                newRoom.GetComponent<SpriteRenderer>().color = end;
                endRoom = newRoom;
                roomObjects.RemoveAt(roomObjects.Count - 1);
            }
            selectedDir = (Direction)Random.Range(0, 4);
            MoveGenerationPoint();
            while(Physics2D.OverlapCircle(generatePoint.position, .2f, whatIsRoom) != null)
            {
                MoveGenerationPoint();
            }
        }
       if(includeShop)
        {
            int shopPos = Random.Range(minDistanceToShop, maxDistanceToShop + 1);
            shopRoom = roomObjects[shopPos];
            roomObjects.RemoveAt(shopPos);
            shopRoom.GetComponent<SpriteRenderer>().color = shopColor;
        }

        //create room outline
        CreateRoomOutline(Vector3.zero);
        foreach(GameObject room in roomObjects)
        {
            CreateRoomOutline(room.transform.position);
        }
        CreateRoomOutline(endRoom.transform.position);
        if(includeShop)
            CreateRoomOutline(shopRoom.transform.position);

        foreach (GameObject roomOutline in generatedOutlines)
        {
            bool generate = true;
            if(roomOutline.transform.position == Vector3.zero)
            {
                Instantiate(startCenter, roomOutline.transform.position, transform.rotation).room = roomOutline.GetComponent<Room>();
                generate = false;
            }
            if(roomOutline.transform.position == endRoom.transform.position)
            {
                Instantiate(endCenter, roomOutline.transform.position, transform.rotation).room = roomOutline.GetComponent<Room>();
                generate = false;
            }
            if (includeShop)
            {
                if (roomOutline.transform.position == shopRoom.transform.position)
                {
                    Instantiate(shopCenter, roomOutline.transform.position, transform.rotation).room = roomOutline.GetComponent<Room>();
                    generate = false;
                }
            } 
            if(generate)
            {
                Instantiate(centers[Random.Range(0, centers.Length)], roomOutline.transform.position, transform.rotation).room = roomOutline.GetComponent<Room>();
            }

        }



    }
    void Update()
    {
#if UNITY_EDITOR
        if(Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
#endif
    }
    public void MoveGenerationPoint()
    {
        switch(selectedDir)
        {
            case Direction.up:
                generatePoint.position += new Vector3(0, yOffset, 0);
                break;
            case Direction.right:
                generatePoint.position += new Vector3(xOffset, 0, 0);
                break;
            case Direction.down:
                generatePoint.position += new Vector3(0, -yOffset, 0);
                break;
            case Direction.left:
                generatePoint.position += new Vector3(-xOffset, 0, 0);
                break;
            default:
                break;
        }
    }
    public void CreateRoomOutline(Vector3 roomPos)
    {
        bool roomAbove = Physics2D.OverlapCircle(roomPos + new Vector3(0f, yOffset, 0f), .2f, whatIsRoom);
        bool roomDown = Physics2D.OverlapCircle(roomPos + new Vector3(0f, -yOffset, 0f), .2f, whatIsRoom);
        bool roomLeft = Physics2D.OverlapCircle(roomPos + new Vector3(-xOffset, 0f, 0f), .2f, whatIsRoom);
        bool roomRight = Physics2D.OverlapCircle(roomPos + new Vector3(xOffset, 0f, 0f), .2f, whatIsRoom);
        int directionCount = 0;
        if(roomAbove)
        {
            directionCount++;
        }
        if (roomDown)
        {
            directionCount++;
        }
        if (roomLeft)
        {
            directionCount++;
        }
        if (roomRight)
        {
            directionCount++;
        }
        switch(directionCount)
        {
            case 1:
                if(roomAbove)
                {
                    generatedOutlines.Add(Instantiate(rooms.u, roomPos, transform.rotation));
                }
                if (roomDown)
                {
                    generatedOutlines.Add(Instantiate(rooms.d, roomPos, transform.rotation));
                }
                if (roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.l, roomPos, transform.rotation));
                }
                if (roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.r, roomPos, transform.rotation));
                }
                break;
            case 2:
                if (roomAbove)
                {
                    if(roomDown)
                    {
                        generatedOutlines.Add(Instantiate(rooms.ud, roomPos, transform.rotation));
                    }
                     else if(roomRight)
                    {
                        generatedOutlines.Add(Instantiate(rooms.ur, roomPos, transform.rotation));
                    }
                    else if(roomLeft)
                    {
                        generatedOutlines.Add(Instantiate(rooms.ul, roomPos, transform.rotation));
                    }
                }
                else if (roomDown)
                {
                    if (roomRight)
                        generatedOutlines.Add(Instantiate(rooms.dr, roomPos, transform.rotation));
                    if (roomLeft)
                        generatedOutlines.Add(Instantiate(rooms.dl, roomPos, transform.rotation));
                }
                else if (roomLeft)
                {
                    if (roomRight)
                        generatedOutlines.Add(Instantiate(rooms.rl, roomPos, transform.rotation));
                    else
                        generatedOutlines.Add(Instantiate(rooms.l, roomPos, transform.rotation));
                }
                else if (roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.r, roomPos, transform.rotation));
                }
                break;
            case 3:
                if(roomAbove)
                {
                    if (roomDown)
                    {
                        if (roomRight)
                            generatedOutlines.Add(Instantiate(rooms.udr, roomPos, transform.rotation));
                        else
                            generatedOutlines.Add(Instantiate(rooms.udl, roomPos, transform.rotation));
                    }
                    else if (roomRight)
                    {
                        generatedOutlines.Add(Instantiate(rooms.url, roomPos, transform.rotation));
                    }
                }
                else
                {
                    generatedOutlines.Add(Instantiate(rooms.drl, roomPos, transform.rotation));
                }
                break;
            case 4:
                generatedOutlines.Add(Instantiate(rooms.udrl, roomPos, transform.rotation));
                break;
            default:
                Debug.LogError("Invalid number of adjacent rooms!");
                break;
        }
    }
}

[System.Serializable]
public class RoomPrefabs
{
    public GameObject u, d, r, l, ud, ur, ul, dr, dl, rl, udr, udl, url, drl, udrl;
}