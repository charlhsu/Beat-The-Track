using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public AudioSource levelMusic, gameOverMusic, winMusic;

    public AudioSource[] sfx;
    public AudioSource[] musicList;
    public AudioSource musicToPlay;
    public float testLongueur;
    private float floatNumberOfRoom;
    public int numberOfLevel;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        musicToPlay = musicList[Random.Range(0, musicList.Length)];
        musicToPlay.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGameOver()
    {
        levelMusic.Stop();
        gameOverMusic.Play();
    }
    public void PlayVictory()
    {
        levelMusic.Stop();
        winMusic.Play();
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();
    }
    public void StopSFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();
    }
}
