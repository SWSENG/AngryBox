using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    protected Rigidbody2D rb;
    public int health;

    private Material matWhite;
    private Material matDefault;
    SpriteRenderer sr;

    protected virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = sr.material;
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Fireball"))
        {
            JumpedOn(3);

        }
        if(collision.CompareTag("Explosion"))
        {
            JumpedOn(5);
            
        }
    }

    public void JumpedOn(int dmgEnemy)
    {
        health -= dmgEnemy;
        sr.material = matWhite;

        Invoke("ResetMaterial", 0.2f);

        if (health <= 0)
        {
            Destroy(this.gameObject);

        }

    }

    private void ResetMaterial()
    {
        sr.material = matDefault;
    }

    
}
