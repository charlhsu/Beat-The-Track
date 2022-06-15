using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    //Définition du canva de la pièce de base
    public GameObject layoutRoom;

    //Propriétés de génération
    public int distanceToEnd;
    public bool includeShop, includeChest;
    public int maxDistanceToChest;
    public int minDistanceToShop;
    public int maxDistanceToShop;
    public int minDistanceToChest;


    public Color startColor, endColor, shopColor, chestColor;

    //Point de génération des pièces
    public Transform generatorPoint;

    //Variables permettant de choisir une direction aléatoirement
    public enum Direction { up, right, down, left};
    public Direction selectedDirection;
    public float xOffset = 18f, yOffset = 10f;

    //Calque avec lequel interagir pur vérifier s'il y a des rooms autour
    public LayerMask whatIsRoom;

    //Gestion de la pièce de fin
    private GameObject endRoom, shopRoom, chestRoom;

    //Stockage des des rooms une fois disposés
    private List<GameObject> layoutRoomObjects = new List<GameObject>();//layouts
    private List<GameObject> generatedOutlines = new List<GameObject>();//Outlines

    //Classe stockant tous les types d'outlines
    public RoomPrefabs rooms;

    //Gestion des roomcenters
    public RoomCenter centerStart, centerEnd, centerShop, centerChest;
    public RoomCenter[] potentialCenters;

    //Gestion du nombre de room par rapport à la longueur
    private float testLongueur;
    private float floatNumberOfRoom;
    private int numberOfLevel;



    // Start is called before the first frame update
    void Start()
    {
        GetNumberOfRoom();
        Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation).GetComponent<SpriteRenderer>().color = startColor;

        selectedDirection = (Direction)Random.Range(0, 4);
        MoveGenerationPoint();

        for (int i = 0; i < distanceToEnd; i++)
        {
            GameObject newRoom = Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation);
            layoutRoomObjects.Add(newRoom);
            if (i + 1 == distanceToEnd)
            {
                newRoom.GetComponent<SpriteRenderer>().color = endColor;
                layoutRoomObjects.RemoveAt(layoutRoomObjects.Count - 1);
                endRoom = newRoom;
            }

            selectedDirection = (Direction)Random.Range(0, 4);
            MoveGenerationPoint();

            while (Physics2D.OverlapCircle(generatorPoint.position, .2f, whatIsRoom))
            {
                MoveGenerationPoint();
            }
        }

        //inclusion du shop
        if (includeShop)
        {
            int shopSelecter = Random.Range(minDistanceToShop, maxDistanceToShop + 1);
            shopRoom = layoutRoomObjects[shopSelecter];
            layoutRoomObjects.RemoveAt(shopSelecter);
            shopRoom.GetComponent<SpriteRenderer>().color = shopColor;
        }
        if (includeChest)
        {
           

                int chestSelecter = Random.Range(minDistanceToChest, maxDistanceToChest +1);
                chestRoom = layoutRoomObjects[chestSelecter];
                layoutRoomObjects.RemoveAt(chestSelecter);
                shopRoom.GetComponent<SpriteRenderer>().color = chestColor;

            
        }

        //Creating rooms outline
        CreateRoomOutlines(Vector3.zero);//start room
        foreach (GameObject room in layoutRoomObjects)
        {
            CreateRoomOutlines(room.transform.position);
        }
        CreateRoomOutlines(endRoom.transform.position);
        if (includeShop)
        {
            CreateRoomOutlines(shopRoom.transform.position);
        }
        if (includeChest)
        {
            CreateRoomOutlines(chestRoom.transform.position);
        }

        //Parcours des outlines générées pour y associer des centres
        foreach (GameObject outline in generatedOutlines) 
        {
            bool generateCenter = true;

            if (outline.transform.position == Vector3.zero)
            {
                Instantiate(centerStart, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();

                generateCenter = false;
            }

            if(outline.transform.position == endRoom.transform.position)
            {
                Instantiate(centerEnd, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();
                generateCenter = false;
            }

            if (includeShop)
            {
                if (outline.transform.position == shopRoom.transform.position)
                {
                    Instantiate(centerShop, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();
                    generateCenter = false;
                }
            }

            if (includeChest)
            {
                if (outline.transform.position == chestRoom.transform.position)
                {
                    Instantiate(centerChest, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();
                    generateCenter = false;
                }
            }


            if (generateCenter)
            {
                //choix aléatoire d'un des centres disponibles
                int centerSelect = Random.Range(0, potentialCenters.Length);

                Instantiate(potentialCenters[centerSelect], outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
#endif
    }

    public void MoveGenerationPoint()
    {
        switch (selectedDirection)
        {
            case Direction.up:
                generatorPoint.position += new Vector3(0f, yOffset, 0f);
                break;
            case Direction.down:
                generatorPoint.position += new Vector3(0f, -yOffset, 0f);
                break;
            case Direction.right:
                generatorPoint.position += new Vector3(xOffset, 0f, 0f);
                break;
            case Direction.left:
                generatorPoint.position += new Vector3(-xOffset, 0f, 0f);
                break;

        }
    }
    public void CreateRoomOutlines(Vector3 roomPosition)
    {
        bool roomAbove = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, yOffset, 0f), .2f, whatIsRoom);
        bool roomBelow = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, -yOffset, 0f), .2f, whatIsRoom);
        bool roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset, 0f, 0f), .2f, whatIsRoom);
        bool roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0f, 0f), .2f, whatIsRoom);

        //Vérification des emplacements des pièces adjacentes
        int directionCount = 0;
        if (roomAbove)
        {
            directionCount++;
        }
        if (roomBelow)
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

        //Définition des outlines en fonction d'où se trouvent les pièces adjacentes
        switch (directionCount)
        {
            case 0:
                Debug.LogError("Found no room");
                break;
            case 1:

                if (roomAbove)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleUp, roomPosition, transform.rotation));
                }
                if (roomBelow)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleDown, roomPosition, transform.rotation));
                }
                if (roomRight) 
                {
                    generatedOutlines.Add(Instantiate(rooms.singleRight, roomPosition, transform.rotation));
                }
                if (roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleLeft, roomPosition, transform.rotation));
                }
                break;
            case 2:
                if(roomAbove && roomBelow)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleUpDown, roomPosition, transform.rotation));
                }
                if (roomAbove && roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleRightTop, roomPosition, transform.rotation));
                }
                if (roomAbove && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleLeftTop, roomPosition, transform.rotation));
                }
                if (roomRight && roomBelow)
                {
                    generatedOutlines.Add(Instantiate(rooms.doublerightDown, roomPosition, transform.rotation));
                }
                if (roomRight && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleLeftRight, roomPosition, transform.rotation));
                }
                if (roomBelow && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleLeftDown, roomPosition, transform.rotation));
                }
                break;
            case 3:
                if (roomAbove && roomBelow && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleTopLeftDown, roomPosition, transform.rotation));
                }
                if (roomRight && roomBelow && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleLeftDownRight, roomPosition, transform.rotation));
                }
                if (roomAbove && roomBelow && roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleTopRightDown, roomPosition, transform.rotation));
                }
                if (roomAbove && roomRight && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleLeftUpRight, roomPosition, transform.rotation));
                }

                break;
            case 4:
                if(roomAbove && roomBelow && roomLeft && roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.quad, roomPosition, transform.rotation));
                }

                break;
        }
    }
    public void GetNumberOfRoom()
    {
        
      
        testLongueur = AudioManager.instance.musicToPlay.clip.length;
        //distanceToEnd = numberOfRoom;
        //MusicEvent.instance.EventMusic(testLongueur);
        floatNumberOfRoom = testLongueur / 10;
        numberOfLevel = Mathf.RoundToInt(floatNumberOfRoom);
        distanceToEnd = numberOfLevel;


    }
}


[System.Serializable]
public class RoomPrefabs
{
    public GameObject singleUp, singleDown, singleRight, singleLeft,
        doubleUpDown, doubleLeftRight, doubleLeftTop, doubleLeftDown, doubleRightTop, doublerightDown,
        tripleLeftUpRight, tripleLeftDownRight, tripleTopLeftDown, tripleTopRightDown, quad; 
}
