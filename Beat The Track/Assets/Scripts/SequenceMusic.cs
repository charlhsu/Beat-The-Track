using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceMusic : MonoBehaviour
{
    public static SequenceMusic instance;
    public float musicTimeCounter;
    public MusicSequence[] sequences;
    public int currentSequence;
    
    public MusicAction[] actions;
    private int currentAction;
    private float actionCounter;

    public float musicLength;

    private bool aSequenceIsActive = false;
    private bool sequencesAreOver = false;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
/*        actions = sequences[currentSequence].actions;
        actionCounter = actions[currentAction].actionLength;
        musicLength = AudioManager.instance.musicToPlay.clip.length;*/
    }

    // Update is called once per frame
    void Update()
    {
        musicTimeCounter += Time.deltaTime;

        //Mise à jour de la séquence en cours
        if (currentSequence <= sequences.Length - 1 && musicTimeCounter >= sequences[currentSequence].timeForSequence && !sequencesAreOver)
        {
            //Setup de la séquence 
            actions = sequences[currentSequence].actions;
            currentAction = 0;
            actionCounter = actions[currentAction].actionLength;
            Debug.Log("Sequences.Length : " + sequences.Length.ToString());
            if(currentSequence == 1)
            {
                Debug.Log("bumcello");
            }
            aSequenceIsActive = true;
            if (currentSequence == sequences.Length - 1)
            {
                sequencesAreOver = true;
            }
            else
            {
                currentSequence++;
            }
        }

        //Mise à jour du action counter
        if (actionCounter > 0)
        {
            actionCounter -= Time.deltaTime;
        }
        //Application de l'action
        if(actionCounter <= 0 && currentAction <= actions.Length -1 && aSequenceIsActive)
        {
            //Activation des spawners
            foreach(EnemySpawner spawner in actions[currentAction].spawnersToActivate)
            {
                spawner.Trigger();

            }
            //mise à jour de l'action en court
            currentAction++;
            actionCounter = actions[currentAction].actionLength; //initialisation du timer de l'action suivante

            if(currentAction == actions.Length)
            {
                aSequenceIsActive = false;
            }
        }

    }

    public void timeInMusic(int time)
    {
        musicTimeCounter = time;
    }

}

[System.Serializable]
public class MusicAction
{
    [Header("Action")]
    public float actionLength;
    public EnemySpawner[] spawnersToActivate;
    

}
[System.Serializable]
public class MusicSequence
{
    [Header("Sequence")]
    public MusicAction[] actions;
    public float timeForSequence;
}