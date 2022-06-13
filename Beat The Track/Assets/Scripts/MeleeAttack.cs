using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public GameObject MeleeHitBox; // renvoie un element créé sur unity
    public Transform MeleePoint;
    public float timeBetweenAttack; // temps en secodne entre chaque tir
    private float AttackCounter; // chrono, cooldown entre chaque tir
    public bool shouldDestroy;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.canMove && !LevelManager.instance.isPaused)
        {

            if (AttackCounter > 0)
            {
                AttackCounter -= Time.deltaTime;

            }
            else
            {
                if (Input.GetMouseButtonDown(1) || Input.GetMouseButton(1)) // appoui sur la souris. 0 = clique gauche, 1 clique droit, 2 molette. Down pour quand tu cliques, up pour quand tu relaches, rien pour quand tu maintiens
                {
                    //Instantiate(MeleeHitBox, MeleePoint.position, MeleePoint.rotation); // Cree une copie d'un objet ( lequel, ou en position, ou en rotation)
                    AttackCounter = timeBetweenAttack;
                    AudioManager.instance.PlaySFX(12);
                    //Gun gunClone = Instantiate(theGun);
                    //gunClone.transform.parent = PlayerControl.instance.gunArm;
                    //gunClone.transform.position = PlayerControl.instance.gunArm.position;
                    //gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                    //gunClone.transform.localScale = Vector3.one;
                    GameObject go = Instantiate(MeleeHitBox, MeleePoint.position, MeleePoint.rotation) as GameObject;
                    //go.transform.parent = GameObject.Find("Player").transform; //PlayerControl.instance.Player;
                    anim.SetTrigger("IsSlashing");
                    //go.transform.position = PlayerControl.instance.Player.position;



                }


            }
        }
    }
}
