using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PermanentUI : MonoBehaviour
{
    public int point = 0;
    public int health;

    public TextMeshProUGUI pointText;
    public TextMeshProUGUI healthText;
    public AngerMeter angerMeter;
    public static PermanentUI perm;
    

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        //singleton
        if (!perm)
        {
            
            perm = this;
            
        }
        else
        {
            

            Destroy(gameObject);
        }

        healthText.text = health.ToString();



    }

    public void Reset()
    {
        health = 3;
        healthText.text = health.ToString();
        point = 0;
        pointText.text = point.ToString();

    }

}
