using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnterNextScene : MonoBehaviour
{
    [SerializeField] private string sceneName;
    private GameMaster gm;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Return))
        {
           

            SceneManager.LoadScene(sceneName);

        }

        if (sceneName == "Tutorial")
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
            gm.lastCheckpointPos = new Vector2(-36.21f, -2.48f);
        }
    }
}
