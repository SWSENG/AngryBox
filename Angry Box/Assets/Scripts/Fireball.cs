using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    
    Rigidbody2D rb;
    [SerializeField] private float fballTime = 10f;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DelayDestory());
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        


        if (player.transform.localScale.x == 1)
        {
            rb.velocity = new Vector2(5, 0);
        }
        else
        {
            rb.velocity = new Vector2(-5, 0);
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);

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
