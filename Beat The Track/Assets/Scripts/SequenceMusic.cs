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

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        actions = sequences[currentSequence].actions;
        actionCounter = actions[currentAction].actionLength;
        musicLength = AudioManager.instance.musicToPlay.clip.length;
    }

    // Update is called once per frame
    void Update()
    {
        musicTimeCounter += Time.deltaTime;
        if (musicTimeCounter <= sequences[currentSequence].timeForSequence && currentSequence < sequences.Length - 1)
        {
            currentSequence++;
            actions = sequences[currentSequence].actions;
            currentAction = 0;
            actionCounter = actions[currentAction].actionLength;
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
    public GameObject[] spawnersToActivate;
}
[System.Serializable]
public class MusicSequence
{
    [Header("Sequence")]
    public MusicAction[] actions;
    public float timeForSequence;
}