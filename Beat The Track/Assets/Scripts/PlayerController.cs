using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Référence
    public static PlayerController instance;
    //Définition des variables de jeu

    [Header("Déplacement")]
    public float moveSpeed;
    [HideInInspector]
    public Vector2 moveInput;

    [Header("saut")]
    public bool canJump;
    public float jumpForce;
    public bool isJumping;
    public bool isOnGround;
    public Transform groundCheck;
    public LayerMask groundLayer;

    //Définition des objets de jeu
    [Header("Assignement des objets")]
    public Rigidbody2D theRB;
    public Transform instrumentArm, meleeArm;
    public SpriteRenderer[] handsSR;
    public GameObject jukebox;
    public Animator anim;
    public SpriteRenderer bodySR;
    private SpriteRenderer jukeboxBodySR;
    [HideInInspector]
    public SpriteRenderer[] disksSR = new SpriteRenderer[5];
    public SpriteRenderer diskSR;
    public Transform[] slots;


    private Camera theCam;

    /*public GameObject bulletToFire;
    public Transform firePoint;

    public float timeBetweenShots;
    private float shotCounter;*/
    [HideInInspector]
    public bool canMove = true;

    public List<Gun> avaiableGuns = new List<Gun>();
    [HideInInspector]
    public int currentGun;



    private float activeMoveSpeed;
    [Header("Dash")]
    public float dashSpeed = 8f, dashLength = 0.5f, dashCooldown = 1f, dashInvincibility = 0.5f;
    private float dashCoolCounter;
    [HideInInspector]
    public float dashCounter;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main;
        activeMoveSpeed = moveSpeed;

        //attribution des SR
        jukeboxBodySR = jukebox.transform.Find("body").GetComponent<SpriteRenderer>();




        //MAJ UI pour l'arme de départ
        /*UIController.instance.currentGun.sprite = avaiableGuns[currentGun].gunUI;
        UIController.instance.gunText.text = avaiableGuns[currentGun].weaponName;*/
    }

    // Update is called once per frame
    void Update()
    {
        bool arrayEmpty = true;
        foreach (Disk slot in JukeboxAnimation.instance.currentDisks)
        {
            if (slot != null)
            {
                arrayEmpty = false;
            }
        }
        if (canMove && !LevelManager.instance.isPaused)
        {
            if (!canJump)
            {
                Physics2D.gravity = new Vector2(0, 0);
                moveInput.y = Input.GetAxisRaw("Vertical");
                moveInput.x = Input.GetAxisRaw("Horizontal");
                moveInput.Normalize();
                theRB.velocity = moveInput * activeMoveSpeed;
            }
            if (canJump)
            {
                Physics2D.gravity = new Vector2(0, -9.8f);
                moveInput.x = Input.GetAxisRaw("Horizontal");
                theRB.velocity = new Vector2(activeMoveSpeed * moveInput.x, theRB.velocity.y);

                isOnGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

                if (isOnGround)
                {
                    if (Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        theRB.velocity = Vector2.up * jumpForce;
                    }
                }

            }

            //Acquisition des coordonnées de la sourie sur le monde
            Vector3 mousePos = Input.mousePosition;
            Vector3 screenPoint = theCam.WorldToScreenPoint(transform.localPosition);
            if (!canJump)
            {
                theRB.velocity = moveInput * activeMoveSpeed;
            }

            //Orientation du joueur en fontion de la position de la souris
            if (mousePos.x < screenPoint.x)
            {
                //transform.localScale = new Vector3(-1f, 1f, 1f);
                if (!arrayEmpty)
                {

                    instrumentArm.localScale = new Vector3(-1f, -1f, 1f);
                }
            }
            else
            {
                //transform.localScale = Vector3.one;
                if (!arrayEmpty)
                {
                    instrumentArm.localScale = new Vector3(-1f, 1f, 1f);
                }
            }
            //rotation du bras armé
            Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            if (!arrayEmpty)
            {

                instrumentArm.rotation = Quaternion.Euler(0, 0, angle);
                
            }
            meleeArm.rotation = Quaternion.Euler(0, 0, angle);

            //Orientation du joueur

            SortBodyElements();

            //tir
            /*if (Input.GetMouseButtonDown(0))
            {
                Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                shotCounter = timeBetweenShots;
                AudioManager.instance.PlaySFX(14);
            }

            if (Input.GetMouseButton(0))
            {
                shotCounter -= Time.deltaTime;
                if (shotCounter <= 0)
                {
                    Instantiate(bulletToFire, firePoint.position, firePoint.rotation);
                    AudioManager.instance.PlaySFX(14);
                    shotCounter = timeBetweenShots;
                }
            }*/

            //Switching weapon 

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (avaiableGuns.Count > 0)
                {
                    currentGun++;
                    if (currentGun > avaiableGuns.Count - 1)
                    {
                        currentGun = 0;
                    }

                    SwitchGun();
                }
                else
                {
                    Debug.LogError("Player has no guns!!! (pas bien booooouh)");
                }
            }
            //Gestion du dash
            /*if (Input.GetKeyDown(KeyCode.Space) && dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;

                anim.SetTrigger("dash");
                PlayerHealthController.instance.MakeInvincible(dashInvincibility);
                AudioManager.instance.PlaySFX(8);
            }

            if (dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;
                if (dashCounter <= 0)
                {
                    activeMoveSpeed = moveSpeed;
                    dashCoolCounter = dashCooldown;
                }
            }

            if (dashCoolCounter > 0)
            {
                dashCoolCounter -= Time.deltaTime;
            }*/

            //changement entre l'animation statique et l'animation de mouvement


            anim.SetBool("isMoving", moveInput != Vector2.zero);

        }
        else
        {
            theRB.velocity = Vector2.zero;
            anim.SetBool("isMoving", false);
        }

    }

    public void SwitchGun()
    {
        foreach (Gun theGun in avaiableGuns)
        {
            theGun.gameObject.SetActive(false);
        }

        avaiableGuns[currentGun].gameObject.SetActive(true);

        UIController.instance.currentGun.sprite = avaiableGuns[currentGun].gunUI;
        UIController.instance.gunText.text = avaiableGuns[currentGun].weaponName;
    }

    private void SortBodyElements()
    {
        bool arrayEmpty = true;
        foreach (Disk slot in JukeboxAnimation.instance.currentDisks)
        {
            if (slot != null)
            {
                arrayEmpty = false;
            }
        }

        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = theCam.WorldToScreenPoint(transform.localPosition);

        //Cas tourné vers le haut
        if (mousePos.y > screenPoint.y)
        {
            if (!arrayEmpty)
            {
                foreach (Disk disk in JukeboxAnimation.instance.currentDisks)
                {
                    if (disk != null)
                    {
                        disk.instrumentLinked.isFront = false;
                        disk.instrumentLinked.SwitchSide();
                    }
                }
            }
            //animation
            anim.SetBool("isFront", false);
            //changement de l'ordre des éléments du joueur

            jukeboxBodySR.sortingOrder = 1;

            if (!arrayEmpty)
            {
                foreach (SpriteRenderer sr in disksSR)
                {
                    if (sr != null)
                    {
                        sr.sortingOrder = 2;
                    }
                }
                foreach (Disk disk in JukeboxAnimation.instance.currentDisks)
                {
                    if (disk != null)
                    {
                        disk.instrumentLinked.instrumentSR.sortingOrder = -1;

                        disk.instrumentLinked.transform.localPosition = new Vector3(-0.3f, 0.45f, 0f);
                        foreach (SpriteRenderer hand in disk.instrumentLinked.instrumentHandsSR)
                        {
                            hand.sortingOrder = -2;
                        }
                    }
                    
                }
                //sorting des mains de l'arme
            }
            else
            {


                foreach (SpriteRenderer hand in handsSR)
                {
                    Debug.Log("Je suis passé par ici");
                    hand.sortingOrder = -2;
                }
            }
        }
        //Cas tourné vers le bas
        else
        {
            jukeboxBodySR.sortingOrder = -1;
            anim.SetBool("isFront", true);
            if (!arrayEmpty)
            {
                foreach (SpriteRenderer sr in disksSR)
                {
                    if (sr != null)
                    {
                        sr.sortingOrder = -2;
                    }
                }
                foreach (Disk disk in JukeboxAnimation.instance.currentDisks)
                {
                    if (disk != null)
                    {
                        disk.instrumentLinked.isFront = true;
                        disk.instrumentLinked.SwitchSide();
                        disk.instrumentLinked.instrumentSR.sortingOrder = 1;
                        disk.instrumentLinked.transform.localPosition = new Vector3(-0.3f, 0.45f, 0);
                        foreach (SpriteRenderer hand in disk.instrumentLinked.instrumentHandsSR)
                        {
                            hand.sortingOrder = 2;
                        }
                    }
                    

                }



            }
            else
            {
                foreach (SpriteRenderer hand in handsSR)
                {
                    hand.sortingOrder = 2;
                }
            }

        }

    }
    public void Jump()
    {
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
        theRB.velocity = moveInput * jumpForce; //Vector2(0f, 1f * jumpForce) ;

        //theRB.velocity = moveInput * activeMoveSpeed;
        isJumping = true;
    }
}
