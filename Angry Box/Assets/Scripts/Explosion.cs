using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private CircleCollider2D coll;


    [SerializeField] private float exSpeed = 3;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<CircleCollider2D>();
        StartCoroutine(DestroySelf());

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(coll.radius < 12f)
        {
            coll.radius += Time.deltaTime * exSpeed;
        }
        
    }


    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);

    }

}
