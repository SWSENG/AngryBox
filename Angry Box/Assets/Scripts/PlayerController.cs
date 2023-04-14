using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;
    private GameMaster gm;

    [SerializeField] private AudioSource dmgSound;
    [SerializeField] private AudioSource footstep;
    [SerializeField] private AudioSource jump;
    [SerializeField] private AudioSource pointSound;
    [SerializeField] private AudioSource heartSound;
    [SerializeField] private AudioSource chillSound;
    [SerializeField] private AudioSource Amc;
    [SerializeField] private AudioSource candySound;
    [SerializeField] private GameObject pause;

    private enum State {idle, run, jump, fall, hurt};
    //initialization
    private State state = State.idle;
    

    public float moveSpeed = 5f;
    public float jumpVelocity = 10f;
    public float hurtForce = 10f;
    public int dmgToEnemy = 1;
    

    [SerializeField] private LayerMask ground;
    
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        

        PermanentUI.perm.pointText.text = PermanentUI.perm.point.ToString();
        PermanentUI.perm.healthText.text = PermanentUI.perm.health.ToString();

        


    }


    private void Update()
    {

        if (state != State.hurt)
        {
            Movement();
        }


        AnimationState();
        anim.SetInteger("state", (int)state);

        Pause();


        if (PermanentUI.perm.health <= 0)
        {
            SceneManager.LoadScene("Game Over");
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
            gm.lastCheckpointPos = new Vector2(-36.21f, -2.48f);
            PermanentUI.perm.Reset();

            
        }



    }


    private void Pause()
    {


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                pause.SetActive(true);
                Time.timeScale = 0;

            }
            else
            {
                pause.SetActive(false);
                Time.timeScale = 1;

            }

        }
    }



    //When collide with enemy
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            bool unbreakable = GameObject.Find("AngerController").GetComponent<Test>().isUnbreakable;
            if (state == State.fall)
            {
                enemy.JumpedOn(dmgToEnemy = 1);

                Jump();
                state = State.jump;
            }
            else if (unbreakable)
            {
                enemy.JumpedOn(dmgToEnemy = 3);
            }
            else
            {

                state = State.hurt;
                HandleHealth();

                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    //Enemy is to my right, damaged and move left
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    //Enemy is to my left, damaged and move right
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }

            }
        }
        

       
    }

    public void HandleHealth()
    {
        PermanentUI.perm.health -= 1;
        PermanentUI.perm.healthText.text = PermanentUI.perm.health.ToString();
        dmgSound.Play();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "AMC")
        {
            GameObject.Find("AngerController").GetComponent<Test>().inAmc = true;
            Amc.Play();

        }
        else if(other.gameObject.tag == "Candy")
        {
            GameObject.Find("AngerController").GetComponent<Test>().changeAnger = false;
            StartCoroutine(ResetEffect());
            candySound.Play();

            Destroy(other.gameObject);

        }
        else if(other.gameObject.tag == "ChillPill")
        {
            GameObject.Find("AngerController").GetComponent<Test>().hasChill = true;
            Destroy(other.gameObject);
            chillSound.Play();

        }
        else if(other.gameObject.tag == "Point")
        {
            Destroy(other.gameObject);
            PermanentUI.perm.point += 100;
            PermanentUI.perm.pointText.text = PermanentUI.perm.point.ToString();
            pointSound.Play();



        }
        else if(other.gameObject.tag == "Bullet")
        {
            state = State.hurt;
            HandleHealth();
            

        }
        else if(other.gameObject.tag == "Health")
        {
            PermanentUI.perm.health += 1;
            PermanentUI.perm.healthText.text = PermanentUI.perm.health.ToString();
            heartSound.Play();


            Destroy(other.gameObject);
        }
        else if(other.gameObject.tag == "DeathZone")
        {
            PermanentUI.perm.health -= 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "AMC")
        {
            GameObject.Find("AngerController").GetComponent<Test>().inAmc = false;
            

        }
    }

    private void Movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        
        
        //move left
        if(moveHorizontal <  0)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            transform.localScale = new Vector3(-1, 1);
        }
        //move right
        else if (moveHorizontal > 0)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            transform.localScale = new Vector3(1, 1);
        }

        //Jump
        if (Input.GetButtonDown("Jump"))
        {
            RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.down, 0.5f, ground);


            if (hit.collider != null)
            {
                Jump();
                
            }
            
        }

        
    }

    private void Jump()
    {
        rb.velocity = Vector2.up * jumpVelocity;
        state = State.jump;
    }

    private void AnimationState()
    {
        //when player is jumping and stops at the peak, play fall animation
        if(state == State.jump)
        {
            if (rb.velocity.y < .1f)
                state = State.fall;
        }
        //when player is falling and hit the ground, play idle animation
        else if (state == State.fall)
        {
            if(coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        //if player is moving
        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.run;

            if (!coll.IsTouchingLayers(ground))
            {
                state = State.fall;
            }
        }
        else
        {

            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
            else
            {
                state = State.fall;
            }
           
        }
            
    }

    private void Footstep()
    {
        footstep.Play();

    }

    private void JumpSound()
    {
        jump.Play();

    }

    private IEnumerator ResetEffect()
    {
        yield return new WaitForSeconds(1f);
        GameObject.Find("AngerController").GetComponent<Test>().changeAnger = true;

    }


}
