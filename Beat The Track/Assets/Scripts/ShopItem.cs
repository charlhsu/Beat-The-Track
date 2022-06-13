using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public GameObject buyMessage;
    private bool inBuyZone = false;

    public bool isHealthRestore, isHealthUpgrade, isWeapon;
    public int itemCost;

    public int healthUpgradeAmount;

    public Gun[] potentialGuns;
    private Gun theGun;
    public SpriteRenderer gunSprite;
    public Text infoText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inBuyZone && Input.GetKeyDown(KeyCode.E))
        {
            if(LevelManager.instance.currentCoins >= itemCost)
            {
                LevelManager.instance.SpendCoins(itemCost);
                if (isHealthRestore)
                {
                    PlayerHealthController.instance.HealPlayer(PlayerHealthController.instance.maxHealth);
                }

                if (isHealthUpgrade)
                {
                    PlayerHealthController.instance.IncreaseMaxHealth(healthUpgradeAmount);
                }


                gameObject.SetActive(false);
                inBuyZone = false;

                AudioManager.instance.PlaySFX(18);
            }
            else
            {
                AudioManager.instance.PlaySFX(19);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            buyMessage.SetActive(true);
            inBuyZone = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            buyMessage.SetActive(false);
            inBuyZone = false;
        }
    }
}
