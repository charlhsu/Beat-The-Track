using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk : MonoBehaviour
{
    //Variables communes 
    [Header("Variables générales")]

    public string weaponName;
    public Instrument instrumentLinked;
    private SpriteRenderer instrumentSR;

    public bool isDistance;
    public bool isMelee;

    public int audioManagerRef;

    [HideInInspector]
    public int actualSlotNumber = 1;

    

    //Variables pour arme distance
    [Header("Variables armes distance")]
    public GameObject bulletToFire;
    public float timeBetweenShots;
    public float angleError;
    private float shotCounter;

    // Start is called before the first frame update
    void Start()
    {
        instrumentSR = instrumentLinked.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        //Contrôle de l'état du joueur
        if (PlayerController.instance.canMove && !LevelManager.instance.isPaused)
        {
            //Cas arme distance
            if (isDistance)
            {

                if (shotCounter > 0)
                {
                    shotCounter -= Time.deltaTime;
                }
                else
                {


                    if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
                    {
                        float error = Random.Range(-angleError/2, angleError/2);
                        Instantiate(bulletToFire, instrumentLinked.firePoint.position, instrumentLinked.firePoint.rotation * Quaternion.Euler(0,0,error));
                        shotCounter = timeBetweenShots;
                       
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        AudioManager.instance.PlaySFX(audioManagerRef);
                        AudioManager.instance.levelMusic.volume = 0f;
                        instrumentSR.enabled = true;
                        foreach (SpriteRenderer hand in instrumentLinked.instrumentHandsSR)
                        {
                            hand.enabled = true;
                        }
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
                if (Input.GetMouseButtonUp(0))
                {
                    Debug.Log("it worked");
                    AudioManager.instance.StopSFX(audioManagerRef);
                    AudioManager.instance.levelMusic.volume = 0.15f;
                    instrumentSR.enabled = false;
                    foreach (SpriteRenderer hand in instrumentLinked.instrumentHandsSR)
                    {
                        hand.enabled = false;
                    }
                }
            }

            if (isMelee)
            {

                if (shotCounter > 0)
                {
                    shotCounter -= Time.deltaTime;
                }
                else
                {


                    if (Input.GetMouseButtonDown(1) || Input.GetMouseButton(1))
                    {
                        float error = Random.Range(-angleError / 2, angleError / 2);
                        Instantiate(bulletToFire, instrumentLinked.firePoint.position, instrumentLinked.firePoint.rotation * Quaternion.Euler(0, 0, error));
                        shotCounter = timeBetweenShots;

                    }
                    if (Input.GetMouseButtonDown(1))
                    {
                        AudioManager.instance.PlaySFX(audioManagerRef);
                        AudioManager.instance.levelMusic.volume = 0f;
                        instrumentSR.enabled = true;
                        foreach (SpriteRenderer hand in instrumentLinked.instrumentHandsSR)
                        {
                            hand.enabled = true;
                        }
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
                if (Input.GetMouseButtonUp(1))
                {
                    Debug.Log("it worked");
                    AudioManager.instance.StopSFX(audioManagerRef);
                    AudioManager.instance.levelMusic.volume = 0.15f;
                    instrumentSR.enabled = false;
                    foreach (SpriteRenderer hand in instrumentLinked.instrumentHandsSR)
                    {
                        hand.enabled = false;
                    }
                }
            }
        }
    }
    public void SetType()
    {
        if (isDistance)
        {
            actualSlotNumber = 0;
        }
        if (isMelee)
        {
            actualSlotNumber = 1;
        }
    }
}
