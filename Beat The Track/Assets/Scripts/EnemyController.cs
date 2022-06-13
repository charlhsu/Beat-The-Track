using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D theRB;
    //Comportement des ennemis
    public float moveSpeed;
  
    [Header("Chase Player")]
    public bool shouldChasePlayer;
    public float rangeToChasePlayer;
    private Vector3 moveDirection;
    
    [Header("Run Away")]
    public bool shouldRunAway;
    public float runAwayRange;
    
    [Header("Wandering")]
    public bool shouldWander;
    public float wanderLength, pauseLength;
    private float wanderCounter, pauseCounter;
    private Vector3 wanderDirection;
    
    [Header("Patrolling")]
    public bool shouldPatrol;
    public Transform[] patrolPoints;
    private int currentPatrolPoint = 0;
    public bool shouldPatrolAndShoot;
    public float shootPauseLength;
    private float shootPauseCounter;


    [Header("Shooting")]
    public bool shouldShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;
    public float shootRange;
    public bool shootsAtMaxRange;



    [Header("Variables")]
    //Sprite render
    public SpriteRenderer theBody;

    //Association de l'animation
    public Animator anim;

    //Statistiques des ennemis
    public int health = 100;
    public bool immortal;
    public bool shouldDropItem;
    public GameObject[] itemsToDrop;
    public float itemDropPercent;

    public GameObject[] deathSplatters;
    public GameObject enemyHit;

   

    // Start is called before the first frame update
    void Start()
    {
        if (shouldWander)
        {
            wanderCounter = Random.Range(wanderLength * 0.75f, wanderLength * 1.25f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(theBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {

            moveDirection = Vector3.zero;

            //Behaviour : chase and shoot
            //=======================================================================
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer && shouldChasePlayer )
            {
                if (shootsAtMaxRange && Vector3.Distance(transform.position, PlayerController.instance.transform.position) > shootRange)
                {
                    //Enemy only mooves if he's further away than his max  shoot rangerange 
                    moveDirection = PlayerController.instance.transform.position - transform.position;
                }
                else if(!shootsAtMaxRange)
                {
                    moveDirection = PlayerController.instance.transform.position - transform.position;
                }
            }
            //Behavious : wander and patrol
            //=======================================================================
            else
            {
                if (shouldWander) //wander
                {
                  
                    if(wanderCounter > 0)
                    {
                        wanderCounter -= Time.deltaTime;

                        //move the enemy
                        moveDirection = wanderDirection; //moveDirection already normalized

                        if(wanderCounter <= 0)
                        {
                            pauseCounter = Random.Range(pauseLength*0.75f, pauseLength*1.25f);
                        }
                    }

                    if(pauseCounter > 0)
                    {
                        pauseCounter -= Time.deltaTime; //L'ennemi est déjà réglé sur immobile au début des loops
                        if(pauseCounter<= 0)
                        {
                            wanderCounter = Random.Range(wanderLength  * 0.75f, wanderLength  * 1.25f);
                            wanderDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                        }
                    }
                }
                if (shouldPatrol)//Patrol
                {
                    
                    moveDirection = patrolPoints[currentPatrolPoint].position - transform.position;
                    if(Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) < 0.2f)
                    {
                        currentPatrolPoint++;
                        if(currentPatrolPoint >= patrolPoints.Length)
                        {
                            currentPatrolPoint = 0;
                        }
                    }
                }
                if (shouldPatrol && shouldPatrolAndShoot)
                {
                    //moveDirection = patrolPoints[currentPatrolPoint].position - transform.position;


                    if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) < .5f)
                    {
                        shouldShoot = true;
                        if (shootPauseCounter > 0)
                        {
                            shootPauseCounter -= Time.deltaTime;
                            moveDirection = Vector3.zero;

                        }
                        if (shootPauseCounter <= 0)
                        {
                            shouldShoot = false;
                            shootPauseCounter = shootPauseLength;
                            moveDirection = patrolPoints[currentPatrolPoint].position - transform.position;
                            currentPatrolPoint++;
                            if (currentPatrolPoint >= patrolPoints.Length)
                            {
                                currentPatrolPoint = 0;
                            }
                        }

                    }
                }


            }

            //Behaviour run away and shoot
            //=======================================================================
            if(shouldRunAway && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < runAwayRange)
            {
                moveDirection = transform.position - PlayerController.instance.transform.position;
            }
            //Behaviour : wander
            //=======================================================================
            
            /*else
            {
                moveDirection = Vector3.zero;
            }*/

            moveDirection.Normalize();
            theRB.velocity = moveDirection * moveSpeed;

            if (moveDirection.x > 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }

            

            //Tir de l'ennemi 
            if (shouldShoot && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < shootRange)
            {
                fireCounter -= Time.deltaTime;
                if (fireCounter <= 0)
                {
                    fireCounter = fireRate;
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    AudioManager.instance.PlaySFX(15);
                }
            }
        }
        else
        {
            theRB.velocity = Vector2.zero;
        }
        anim.SetBool("isMoving", moveDirection != Vector3.zero);
    }

    public void DamageEnemy(int damages)
    {

        if (!immortal)
        {
            health -= damages;
            AudioManager.instance.PlaySFX(2);
            Instantiate(enemyHit, transform.position, transform.rotation);

            if (health <= 0)
            {
                Destroy(gameObject);

                TempoManager.instance.enemyKilled();

                int selectedSplatter = Random.Range(0, deathSplatters.Length);

                int rotation = Random.Range(0, 4);

                AudioManager.instance.PlaySFX(1);

                Instantiate(deathSplatters[selectedSplatter], transform.position, Quaternion.Euler(0f, 0f, rotation * 90));


                //Instantiate(deathSplatter, transform.position, transform.rotation);

                if (shouldDropItem)
                {
                    float dropChance = Random.Range(0f, 100f);

                    if (dropChance < itemDropPercent)
                    {
                        int randomItem = Random.Range(0, itemsToDrop.Length);

                        Instantiate(itemsToDrop[randomItem], transform.position, transform.rotation);
                    }
                }
            }
        }
    }
}
