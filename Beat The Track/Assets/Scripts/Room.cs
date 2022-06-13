using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour

{
    public bool closeWhenEntered;
    public bool activeGravity;
    //public bool openWhenEnnemiesCleared;

    //public List<GameObject> enemies = new List<GameObject>();


    public GameObject[] doors;

    [HideInInspector]
    public bool roomActive;

    public GameObject mapHider;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //Suppression d'ennemis dans la liste lorsqu'ils sont détruits
        /*if(enemies.Count > 0 && roomActive && openWhenEnnemiesCleared)
        {
            for(int i = 0; i< enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }
            if(enemies.Count == 0)
            {
                foreach (GameObject door in doors)
                {
                    door.SetActive(false);

                    closeWhenEntered = false;
                }
            }
        }*/
    }
    public void OpenDoors()
    {
        foreach (GameObject door in doors)
        {
                door.SetActive(false);

                closeWhenEntered = false;
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //déplacement de la caméra à l'entrée
            CameraController.instance.ChangeTarget(transform);

            //fermeture des portes à l'entrée
            if (closeWhenEntered)
            {
                foreach(GameObject door in doors)
                {
                    door.SetActive(true);
                }
            }
            
            roomActive = true;

            mapHider.SetActive(false);
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            roomActive = false;
            PlayerController.instance.canJump = false;
        }
    }

    
}
