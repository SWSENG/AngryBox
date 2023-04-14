using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organics_3 : Enemy
{
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


    }


    private bool facingLeft = true;

    private void Update()
    {
        if (facingLeft)
        {
            if (transform.position.x > leftCap)
            {
                if(coll.IsTouchingLayers(ground))
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
                if(coll.IsTouchingLayers(ground))
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

}
