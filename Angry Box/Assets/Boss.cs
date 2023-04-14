using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    Object bulletRef;
    [SerializeField] private float atkDelay = 5f;
    private float nextAtkTime;
    private Collider2D coll;

    [SerializeField] private LayerMask ground;
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    [SerializeField] private float jumpLength = 10f;
    [SerializeField] private float jumpHeight = 15f;

    //override means change the base
    protected override void Start()
    {
        //inheritance
        base.Start();
        coll = GetComponent<Collider2D>();
        bulletRef = Resources.Load("Bullet");

    }


    private bool facingLeft = true;

    private void Update()
    {
        if (health <= 10 && health > 0)
        {
            Movement();
            rb.bodyType = RigidbodyType2D.Dynamic;


        }
        else
        {
            AfterMovement();
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        Fireball();

        
    }

    private void Movement()
    {
        if (facingLeft)
        {
            if (transform.position.x > leftCap)
            {
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                }


            }
            else
            {
                facingLeft = false;
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);

            }

        }
        else
        {
            if (transform.position.x < rightCap)
            {
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                }


            }
            else
            {
                facingLeft = true;
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            }

        }
    }

    private void AfterMovement()
    {
        if (facingLeft)
        {
            if (transform.position.x > leftCap)
            {
                
                rb.velocity = new Vector2(-jumpLength, 0);
                


            }
            else
            {
                facingLeft = false;
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);

            }

        }
        else
        {
            if (transform.position.x < rightCap)
            {
                
                 rb.velocity = new Vector2(jumpLength, 0);
                


            }
            else
            {
                facingLeft = true;
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            }

        }
    }

    private void Fireball()
    {
        if (Time.time > nextAtkTime)
        {
            GameObject bullet = (GameObject)Instantiate(bulletRef);
            bullet.transform.position = new Vector3(transform.position.x, transform.position.y);
            nextAtkTime += atkDelay;
        }
    }
}
