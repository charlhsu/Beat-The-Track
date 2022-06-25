using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileManager : MonoBehaviour
{

    public TileBlink tileBlinker;
    private Tilemap tilemap;
    public Event[] events;

    private float switchTimer;
    private float musicTimer;

    private int currentEvent;


    // Start is called before the first frame update
    void Start()
    {
        tilemap = tileBlinker.tilemap;
        foreach (Vector3Int tilePosition in tilemap.cellBounds.allPositionsWithin)
        {
            tilemap.SetColor(tilePosition, tileBlinker.restColor);
        }
        //enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        musicTimer += Time.deltaTime;
        if(musicTimer >= events[currentEvent].startTime)
        {
            if(events[currentEvent].behaviour == 1)
            {
                AutoMode(events[currentEvent].AutoColorSet);
            }
            if (events[currentEvent].behaviour == 2)
            {
                RapidFlashes(events[currentEvent].colorSet, events[currentEvent].switchRate);
            }
            if(events[currentEvent].behaviour == 3)
            {
                Pause(events[currentEvent].pauseColor, events[currentEvent].colorLerp);
            }
        }

        if (currentEvent + 1 != events.Length) {
            if (musicTimer >= events[currentEvent + 1].startTime)
            {
                currentEvent++;
            }
        }
    }

    public void RapidFlashes(Color[] colorSet, float switchRate)
    {
        // behaviour 2

        /*autoTimer += Time.deltaTime;
        if (autoTimer >= manualTimestamp && autoMode)
        {
            autoMode = false;

            foreach (Vector3Int tilePosition in tilemap.cellBounds.allPositionsWithin)
            {
                tilemap.SetColor(tilePosition, tileBlinker.restColor);
            }

            tileBlinker.enabled = false;

        }*/
        tileBlinker.enabled = false;

        switchTimer += Time.deltaTime;
        if (switchTimer >= switchRate)
        {

            foreach (Vector3Int tilePosition in tilemap.cellBounds.allPositionsWithin)
            {
                int colorIndex = Random.Range(0, colorSet.Length);
                tilemap.SetColor(tilePosition, colorSet[colorIndex]);
            }
            switchTimer = 0;
        }

        
    }
    public void AutoMode(Color[] colorSet)
    {
        //behaviour 1
        tileBlinker.enabled = true;
        tileBlinker.beatColors = colorSet;
    }
    public void Pause(Color pauseColor, float colorLerp)
    {
        //behaviour 3
        tileBlinker.enabled = false;
        foreach (Vector3Int tilePosition in tilemap.cellBounds.allPositionsWithin)
        {
            Color color = Color.Lerp(tilemap.GetColor(tilePosition), pauseColor, colorLerp * Time.deltaTime);
            tilemap.SetColor(tilePosition, color);
        }
    }
        

    
}
[System.Serializable]
public class Event
{
    //gougougaga
    public int behaviour;
    public float startTime;
    [Header("used for behaviour 3")]
    public Color pauseColor;
    public float colorLerp;

    [Header("used for behaviour 2")]
    public Color[] colorSet;
    public float switchRate;

    [Header("used for behaviour 1")]
    public Color[] AutoColorSet;
}
