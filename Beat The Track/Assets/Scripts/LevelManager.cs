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
        AudioManager.instance.PlayVictory();
        //UIController.instance.StartFadeToBlack();
        PlayerController.instance.canMove = false;
        yield return new WaitForSeconds(waitToLoad);

        CharacterTracker.instance.currentCoins = currentCoins;
        CharacterTracker.instance.maxHealth = PlayerHealthController.instance.maxHealth;
        CharacterTracker.instance.currentHealth = PlayerHealthController.instance.currentHealth;

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
            nextLevel = "level electro";
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
