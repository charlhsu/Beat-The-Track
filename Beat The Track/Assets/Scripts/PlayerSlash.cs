using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlash : MonoBehaviour
{

    public Rigidbody2D theRB;
    public GameObject Impact;
    public int damageToGive = 50;

    public float timeBeforeDestroy = .1f;
    private float timeBeforeDestroyCount;
    // Start is called before the first frame update
    void Start()
    {
        timeBeforeDestroyCount = timeBeforeDestroy;
    }

    // Update is called once per frame
    void Update()
    {
        //theRB.velocity = transform.right * speed;
        timeBeforeDestroyCount -= Time.deltaTime;
        if (timeBeforeDestroyCount <= 0)
        {
            timeBeforeDestroyCount = timeBeforeDestroy;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(Impact, transform.position, transform.rotation);
        Destroy(gameObject);
        AudioManager.instance.PlaySFX(4);

        if (other.tag == "Enemy") // si lors d'une collision, l'objet touché porte le tag eennemi...
        {

            other.GetComponent<EnemyController>().DamageEnemy(damageToGive); // on envoie ailleurs, a Ennemy controller dans la fonction damage ennemy la valeur de damage to give
        }
        /*if (other.tag == "Boss") // si lors d'une collision, l'objet touché porte le tag eennemi...
        {

            BossController.instance.TakeDamage(damageToGive); // on envoie ailleurs, a Ennemy controller dans la fonction damage ennemy la valeur de damage to give
            Instantiate(BossController.instance.hitEffect, transform.position, transform.rotation);
        }*/

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


}