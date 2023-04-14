using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    private Collider2D coll;
    [SerializeField] private LayerMask ground;
    Rigidbody2D rb;
    [SerializeField] private float fballTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DelayDestory());

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            DestroySelf();

        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(0, -5);
        if(coll.IsTouchingLayers(ground))
        {
            DestroySelf();

        }
        
    }

    private void DestroySelf()
    {
        Destroy(this.gameObject);

    }

    IEnumerator DelayDestory()
    {
        yield return new WaitForSeconds(fballTime);
        DestroySelf();
    }
}
