using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusicExit : MonoBehaviour
{
    public bool isRock, isElectro, isRAP, isPOP, canOpen;
    public GameObject message;
    public string levelToLoad;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isRock)
                {
                    //StartCoroutine(LevelManager.instance.LevelEnd());
                    LevelManager.instance.LevelToLoad(1);
                    StartCoroutine(LevelManager.instance.LevelEnd());
                }
                if (isElectro)
                {
                    LevelManager.instance.LevelToLoad(2);
                    StartCoroutine(LevelManager.instance.LevelEnd());
                }
                if (isRAP)
                {
                    //StartCoroutine(LevelManager.instance.LevelEnd());
                    LevelManager.instance.LevelToLoad(3);
                    StartCoroutine(LevelManager.instance.LevelEnd());
                }
                if (isPOP)
                {
                    LevelManager.instance.LevelToLoad(4);
                    StartCoroutine(LevelManager.instance.LevelEnd());
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //StartCoroutine(LevelManager.instance.LevelEnd());
            canOpen = true;
            message.SetActive(true);

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //StartCoroutine(LevelManager.instance.LevelEnd());
            canOpen = false;
            message.SetActive(false);

        }
    }
}
