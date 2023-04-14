using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    static bool alwaysActive = false;

    [SerializeField] GameObject anger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            alwaysActive = true;

        }
    }

    private void Update()
    {
        if(alwaysActive)
        {
            anger.gameObject.SetActive(true);
        }
    }
}
