using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoManager : MonoBehaviour
{
    public static TempoManager instance;
    public float tempoCounter, tempoMaxValue;
    public float volumeValue;
    public bool isChallengeRoom;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (isChallengeRoom)
        {
            UIController.instance.tempoSlider.gameObject.SetActive(true);
            tempoCounter = tempoMaxValue;
            UIController.instance.tempoSlider.maxValue = tempoMaxValue;
            UIController.instance.tempoSlider.value = tempoCounter;
            volumeValue = UIController.instance.tempoSlider.value / 10;
        }
        else
        {
            UIController.instance.tempoSlider.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isChallengeRoom)
        {
            tempoCounter -= Time.deltaTime;
            UIController.instance.tempoSlider.value = tempoCounter;
            volumeValue = UIController.instance.tempoSlider.value / 10;
            AudioListener.volume = volumeValue;
            if (tempoCounter <= 0)
            {
                PlayerHealthController.instance.currentHealth = 0;
                PlayerHealthController.instance.DamagePlayer();
            }
            if (tempoCounter >= tempoMaxValue)
            {
                tempoCounter = tempoMaxValue;
            }
        }
    }

    public void enemyKilled()
    {
        tempoCounter = tempoCounter + 2;
    }
}
