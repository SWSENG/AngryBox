using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organics_2 : Enemy
{
    Object bulletRef;
    [SerializeField] private float atkDelay = 5f;
    private float nextAtkTime;

    protected override void Start()
    {
        //inheritance
        base.Start();
        bulletRef = Resources.Load("Bullet");


    }


    private void Update()
    {
        

        if (Time.time > nextAtkTime)
        {
            GameObject bullet = (GameObject)Instantiate(bulletRef);
            bullet.transform.position = new Vector3(transform.position.x, transform.position.y);
            nextAtkTime+=atkDelay;
        }
    }



}
