using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instrument : MonoBehaviour
{
    //Choix du l'instrument
    public bool isGuitar;
    
    
    public SpriteRenderer instrumentSR;
    public Sprite frontSprite;
    public Sprite backSprite;
    public SpriteRenderer[] instrumentHandsSR;

    public Transform firePoint;
    [HideInInspector]
    public bool isFront = true;

    // Start is called before the first frame update
    void Start()
    {
        if (isFront)
        {
            instrumentSR.sprite = frontSprite;
        }
        else
        {
            if (isGuitar)
            {
                instrumentSR.sprite = backSprite;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwitchSide()
    {
        if (isGuitar)
        {
            if (isFront)
            {
                instrumentSR.sprite = frontSprite;
            }
            else
            {
                instrumentSR.sprite = backSprite;
            }
        }
    }
}
