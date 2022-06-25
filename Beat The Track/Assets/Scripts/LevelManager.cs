using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;
    public float waitToLoad;
    public bool isPaused;
    private string nextRockLevel, nextElectroLevel;
    public bool nextLevelIsRock, nextLevelIsElectro;

    public int currentCoins;

    public Transform startPoint;

    public string nextLevel;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        PlayerController.instance.transform.position = startPoint.position;
        PlayerController.instance.canMove = true;

        currentCoins = CharacterTracker.instance.currentCoins;
        Time.timeScale = 1f;
        UIController.instance.coinText.text = currentCoins.ToString();
        /*if(CharacterTracker.instance.currentDisk != null)
        {
            CharacterTracker.instance.currentDisk.transform.parent = PlayerController.instance.slots[CharacterTracker.instance.currentDisk.actualSlotNumber];
            CharacterTracker.instance.currentDisk.transform.position = PlayerController.instance.slots[CharacterTracker.instance.currentDisk.actualSlotNumber].position;
            CharacterTracker.instance.currentDisk.transform.localRotation = Quaternion.Euler(Vector3.zero);
            CharacterTracker.instance.currentDisk.transform.localScale = new Vector3(3.1f, 3.1f, 1f);

            JukeboxAnimation.instance.currentDisks[CharacterTracker.instance.currentDisk.actualSlotNumber] = CharacterTracker.instance.currentDisk;
            PlayerController.instance.disksSR[CharacterTracker.instance.currentDisk.actualSlotNumber] = CharacterTracker.instance.currentDisk.GetComponent<SpriteRenderer>();

            Instrument instrumentClone = Instantiate(CharacterTracker.instance.currentDisk.instrumentLinked);
            instrumentClone.transform.parent = PlayerController.instance.instrumentArm.transform;
            instrumentClone.transform.localPosition = instrumentClone.localPos;
            instrumentClone.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            CharacterTracker.instance.currentDisk.instrumentLinked = instrumentClone;
        }*/

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public IEnumerator LevelEnd()
    {
        //AudioManager.instance.PlayVictory();
        //UIController.instance.StartFadeToBlack();
        PlayerController.instance.canMove = false;
        yield return new WaitForSeconds(0.5f);

        /*CharacterTracker.instance.currentCoins = currentCoins;
        CharacterTracker.instance.maxHealth = PlayerHealthController.instance.maxHealth;
        CharacterTracker.instance.currentHealth = PlayerHealthController.instance.currentHealth;
        CharacterTracker.instance.currentDisk = */

        SceneManager.LoadScene(nextLevel);
    }

    public void PauseUnpause()
    {
        if (!isPaused)
        {
            UIController.instance.pauseMenu.SetActive(true);
            isPaused = true;
            Time.timeScale = 0f;
        }
        else
        {
            UIController.instance.pauseMenu.SetActive(false);
            isPaused = false;
            Time.timeScale = 1f;
        }
    }

    public void GetCoins(int amount)
    {
        currentCoins += amount;
        UIController.instance.coinText.text = currentCoins.ToString();
    }
    public void SpendCoins(int amount)
    {
        currentCoins -= amount;
        if(currentCoins < 0)
        {
            currentCoins = 0;
        }
        UIController.instance.coinText.text = currentCoins.ToString();

    }
    public void LevelToLoad(int levelToLoad)
    {
        if (levelToLoad == 1)
        {
            nextLevel = "Level Rock";
        }
        if (levelToLoad == 2)
        {
            nextLevel = "Defi Electro";
        }
        if (levelToLoad == 3)
        {
            nextLevel = "Level RAP";
        }
        if (levelToLoad == 4)
        {
            nextLevel = "Level POP";
        }
        LevelEnd();
    }

}
