using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicEvent : MonoBehaviour
{
    public static MusicEvent instance;
    public bool event1, event2, event3;
    public float musicCount;
    public float musicLength;
    public float timeEventStart, timeEventEnd;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        musicCount += Time.deltaTime;
        if (musicCount >= timeEventStart)
        {
            event1 = true;
            if (musicCount >= timeEventEnd)
            {
                event1 = false;
                event2 = true;
            }
        }
        if (musicCount >= musicLength)
        {
            musicCount = 0;

        }
    }

    public void EventMusic(float musicDuration)
    {
        musicLength = musicDuration;

    }
}
