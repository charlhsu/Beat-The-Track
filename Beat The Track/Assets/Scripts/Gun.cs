using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletToFire;
    public Transform firePoint;

    public float timeBetweenShots;
    private float shotCounter;

    public string weaponName;
    public Sprite gunUI;
    //[Header("DPS = "+ DPS.ToString())]
    // Start is called before the first frame update
    void Start()
    {
        /*float DPS = (bulletToFire.bulletDamages * (1 / timeBetweenShots));
        Debug.Log("Current weapon DPS : " + DPS.ToString());*/
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.canMove && !LevelManager.instance.isPaused)
        {

            if (shotCounter > 0)
            {
                shotCounter -= Time.deltaTime;
            }
            else
            {


                if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
                {
                    Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                    shotCounter = timeBetweenShots;
                    AudioManager.instance.PlaySFX(14);
                }

                /*if (Input.GetMouseButton(0))
                {
                    shotCounter -= Time.deltaTime;
                    if (shotCounter <= 0)
                    {
                        Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                        AudioManager.instance.PlaySFX(14);
                        shotCounter = timeBetweenShots;
                    }
                }*/
            }
        }
    }
}
