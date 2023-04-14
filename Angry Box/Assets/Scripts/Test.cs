using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    Object fballRef;//fireball
    Object exRef;//explosion

    public PlayerController player;
    private SpriteRenderer spriteRen;
    

    [SerializeField]private AudioSource powerUp;
    [SerializeField] private AudioSource explosionSound;

    public int abilityCooldownTime = 3;
    public int abilityDurationTime = 2;
    public float minAnger = 0;
    public float currentAnger;
    public float pressAnger = 0.10f;
    
    public float timeScale;

    
    bool isActivate = false;
    bool canExplode = true;
    public bool canActivate = true;
    public bool inAmc = false;
    public bool isUnbreakable = false;
    public bool changeAnger = true;
    public bool hasChill = false;

    private enum StateColor { grey, yellow, orange, red , explode};
    private StateColor stateColor = StateColor.grey;
   

    private void Start()
    {
        currentAnger = minAnger;
        PermanentUI.perm.angerMeter.SetMinAnger(minAnger);
        spriteRen = player.GetComponent<SpriteRenderer>();
        exRef = Resources.Load("Explosion");
        fballRef = Resources.Load("Fireball");
        powerUp = GetComponent<AudioSource>();


        

      
    }

    private void Update()
    {
        SetColour();

        if(currentAnger < 0f)
        {
            currentAnger = 0;
        }

        if (!isActivate &&  inAmc == false && changeAnger == true)
        {
            AngerIncrease();

        }
        else if(changeAnger == true)
        {
            AngerDecrease();
        }


        if (currentAnger >= 3f && inAmc == false)
        {
            ActivateAbility();
        }

        


        PressIncreaseAnger();

    }

    //When pressed will increase anger 
    private void PressIncreaseAnger()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            currentAnger += pressAnger;
        }
    }

    //handles ability activation condition
    void ActivateAbility()
    {

        if (Input.GetKeyDown(KeyCode.K) && canActivate)
        {
            powerUp.Play();


            switch (stateColor)
            {
                case StateColor.yellow:
                    
                    Boosto(); 
                    break;

                case StateColor.orange:
                    
                    Fireball();

                    break;

                case StateColor.red:
                    
                    Unbreakable();
                    break;

                


            }

            StartCoroutine(ResetPower());
            
            
            isActivate = true;
            canActivate = false;

            StartCoroutine(AbilityTime());
            
        }

        //For explosion
        if (stateColor == StateColor.explode)
        {
            if(canExplode)
            {
                Explosion();
                canExplode = false;
            }

            
            player.gameObject.SetActive(false);
            StartCoroutine(ResetExplosion());

        }

        if(Input.GetKeyDown(KeyCode.K) && hasChill)
        {
            powerUp.Play();
            switch (stateColor)
            {
                case StateColor.yellow:

                    Boosto();
                    break;

                case StateColor.orange:

                    Fireball();

                    break;

                case StateColor.red:

                    Unbreakable();
                    break;




            }

            hasChill = false;
            StartCoroutine(ResetPower());
        }
        
    }

   


    //decreasing anger over time
    void AngerDecrease()
    {
        currentAnger -= Time.deltaTime*timeScale;
        SetAnger(currentAnger);
    }

    //increasing anger over time
    void AngerIncrease()
    {
        currentAnger += Time.deltaTime*timeScale;
        SetAnger(currentAnger);

        
    }

  

    //set the colour of the sprite renderer and handles the state of angry box
    void SetColour()
    {
        if(currentAnger < 3)
        {
            stateColor = StateColor.grey;
            spriteRen.color = Color.white;

        }

        if (currentAnger >= 3 && currentAnger < 13)
        {
            stateColor = StateColor.yellow;
            spriteRen.color = Color.yellow;
           
        }
        //orange
        else if (currentAnger >= 13 && currentAnger < 17)
        {
            stateColor = StateColor.orange;
            spriteRen.color = new Color32(254, 161, 0, 255);
        }
        //red
        else if (currentAnger >= 17 && currentAnger < 20)
        {
            stateColor = StateColor.red;
            spriteRen.color = Color.red;
        }
        //explode?
        else if (currentAnger >= 20)
        {
            stateColor = StateColor.explode;
            
        }
    }

    //Ability Boosto
    private void Boosto()
    {
        player.moveSpeed += 3;
        player.jumpVelocity += 7;
    }


    private void Fireball()
    {
        GameObject fireball = (GameObject)Instantiate(fballRef);
        fireball.transform.position = new Vector3(player.transform.position.x + .4f,
            player.transform.position.y + .2f);
        player.moveSpeed += 3;
        player.jumpVelocity += 7;
    }

    private void Unbreakable()
    {
        player.moveSpeed += 3;
        player.jumpVelocity += 7;
        player.hurtForce -= 10;
        isUnbreakable = true;
    }

    private void Explosion()
    {
        GameObject explosion = (GameObject)Instantiate(exRef);
        explosion.transform.position = new Vector3(player.transform.position.x, player.transform.position.y);
        player.HandleHealth();

        explosionSound.Play();


    }

    private IEnumerator ResetExplosion()
    {
        yield return new WaitForSeconds(2);
        player.gameObject.SetActive(true);
        currentAnger = minAnger;
        canExplode = true;

    }

    //Reset Power based on bool
    private IEnumerator ResetPower()
    {
        switch (stateColor)
        {
            case StateColor.yellow:
                yield return new WaitForSeconds(abilityDurationTime);
                //start coroutine to stop return power to normal levels
                player.moveSpeed -= 3;
                player.jumpVelocity -= 7;
                break;

            case StateColor.orange:
                yield return new WaitForSeconds(abilityDurationTime);
                player.moveSpeed -= 3;
                player.jumpVelocity -= 7;
                break;


            case StateColor.red:
                yield return new WaitForSeconds(abilityDurationTime);
                player.moveSpeed -= 3;
                player.jumpVelocity -= 7;
                player.hurtForce += 10;
                isUnbreakable = false;
                break;

        }
    }

    //Cooldown timer for the ability Motivation
    private IEnumerator WaitforCooldown()
    {
        yield return new WaitForSeconds(abilityCooldownTime);
       
        canActivate = true;
        
    }
    
    //how long the ability goes on, and after that duration is over ,trigger cooldown timer
    private IEnumerator AbilityTime()
    {
       
        
        yield return new WaitForSeconds(abilityDurationTime);

        isActivate = false;

        //cooldown timer
        yield return StartCoroutine(WaitforCooldown());
    }
    
    //set the anger in the meter
    void SetAnger(float anger)
    {
        PermanentUI.perm.angerMeter.SetAnger(anger);
    }

    

}
