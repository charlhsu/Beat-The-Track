using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCenter : MonoBehaviour
{

    public List<GameObject> enemies = new List<GameObject>();
    public bool openWhenEnnemiesCleared;

    public Room theRoom;
    public bool needGrav;
    // Start is called before the first frame update
    void Start()
    {
        if (openWhenEnnemiesCleared)
        {
            theRoom.closeWhenEntered = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count > 0 && theRoom.roomActive && openWhenEnnemiesCleared)
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
                theRoom.OpenDoors();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (needGrav)
            {
                //theRoom.activeGravity = true;
                PlayerController.instance.canJump = true;
            }


        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (needGrav)
            {
                //theRoom.activeGravity = true;
                PlayerController.instance.canJump = false;
            }


        }

    }
}
