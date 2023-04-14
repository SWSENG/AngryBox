using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organics_1 : Enemy
{

    private Collider2D coll;

    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    [SerializeField] private float moveSpeed = 5f;

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
        if(facingLeft)
        {
            if (transform.position.x > leftCap)
            {
                rb.velocity = new Vector2(-moveSpeed, 0);

            }
            else
            {
                facingLeft = false;
                transform.localScale = new Vector2(transform.localScale.x * -1 , transform.localScale.y);

            }
              
        }
        else
        {
            if(transform.position.x < rightCap)
            {
                rb.velocity = new Vector2(moveSpeed, 0);

            }
            else
            {
                facingLeft = true;
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            }

        }

        
    }

}
