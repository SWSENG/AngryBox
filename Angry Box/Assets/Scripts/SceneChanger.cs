using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChanger : MonoBehaviour
{
    
    [SerializeField] private string sceneName;
    

    private GameMaster gm;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName);

            if(sceneName == "Tutorial")
            {
                gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
                gm.lastCheckpointPos = new Vector2(-36.21f, -2.48f);
            }
            if(sceneName == "Level1")
            {
                gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
                gm.lastCheckpointPos = new Vector2(-20.168f, 1.475f);
            }
            else if(sceneName == "Level2")
            {
                gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
                gm.lastCheckpointPos = new Vector2(-10f, -1.3f);
            }
            else if(sceneName == "LevelBoss")
            {
                gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
                gm.lastCheckpointPos = new Vector2(13.45f, -1.32f);
            }



        }
    }


    

}
