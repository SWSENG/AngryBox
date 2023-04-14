using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    

    private void Start()
    {
       

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
            player.HandleHealth();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
