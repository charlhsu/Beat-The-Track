using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guitar : MonoBehaviour
{
    public SpriteRenderer guitarSR;
    public Sprite frontSprite;
    public Sprite backSprite;
    public SpriteRenderer[] guitarHandsSR;
    [HideInInspector]
    public bool isFront = true;

    // Start is called before the first frame update
    void Start()
    {
        if (isFront)
        {
            guitarSR.sprite = frontSprite;
        }
        else
        {
            guitarSR.sprite = backSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchSide()
    {
        if (isFront)
        {
            guitarSR.sprite = frontSprite;
        }
        else
        {
            guitarSR.sprite = backSprite;
        }
    }
}
