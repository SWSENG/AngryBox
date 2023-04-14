using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AfterBoss : MonoBehaviour
{
    [SerializeField] private Boss boss;





    private void Update()
    {
        if (boss.health <= 0)
        {
            SceneManager.LoadScene("End");

        }
    }
    
}
