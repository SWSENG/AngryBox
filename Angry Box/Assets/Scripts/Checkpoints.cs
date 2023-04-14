using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    private GameMaster gm;
    private AudioSource collected;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        collected = GetComponent<AudioSource>();

        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            gm.lastCheckpointPos = transform.position;
            GetComponent<SpriteRenderer>().color = Color.red;
            collected.Play();

        }
    }
}
