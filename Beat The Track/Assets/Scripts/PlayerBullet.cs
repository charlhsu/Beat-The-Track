using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 7.5f;
    public Rigidbody2D theRB;
    public GameObject impactEffect;
    public int bulletDamages = 20;
    public bool hasMultipleSprites;
    public Sprite[] sprites;
    public SpriteRenderer bulletSR;

    // Start is called before the first frame update
    void Start()
    {
        if (hasMultipleSprites) 
        {
            int spriteChoice = Random.Range(0, sprites.Length);
            bulletSR.sprite = sprites[spriteChoice];
        }
    }

    // Update is called once per frame
    void Update()
    {
        //caca
        theRB.velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        if(other.tag == "Wall")
        {
            AudioManager.instance.PlaySFX(4);
        }

        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().DamageEnemy(bulletDamages);
        }

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
