using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class martin_ctrl : MonoBehaviour
{
    [SerializeField]
    float MovementSpeed = 0.1f;
    [SerializeField]
    Rigidbody2D m_rigidbody;
    [SerializeField]
    float JumpPower = 100f;
    [SerializeField]
    bool isGrounded;
    [SerializeField]
    private LayerMask m_WhatIsGround;
    [SerializeField]
    private Transform m_GroundCheck;
    [SerializeField]
    float groundedRadius;
    [SerializeField]
    Collider2D p_eCollision;
    [SerializeField]
    Collider2D p_gCollision;
    [SerializeField]
    Collider2D groundCollider;
    [SerializeField]
    Collider2D e_RoombaCollider;
    [SerializeField]
    int p_StartHealth = 3;

    //attack control
    [SerializeField]
    Collider2D atk_collider;
    float atk_colX;

    Vector2 p_AttackColliderOffset;
    //Health.
    public static int p_Health;

    //Invincibility variable
    bool p_IsInvincible;
    bool p_GotHit;

    //Punching variables
    bool p_Punch;
    bool an_Punch;

    Animator p_anim;

    SpriteRenderer p_spriterend;

    private void Start()
    {
        p_anim = this.gameObject.GetComponent<Animator>();
        p_IsInvincible = false;
        p_Health = p_StartHealth;
        p_spriterend = GetComponent<SpriteRenderer>();
        atk_colX = atk_collider.offset.x;
        Physics2D.IgnoreLayerCollision(8, 11, false);
    }
    void Update()
    {

        //Animation control
        if (Input.GetKey("a"))
        {

            if (an_Punch == false)
            {
                p_anim.SetBool("IsMoving", true);
            }

        }
        else if (Input.GetKey("d"))
        {

            if (an_Punch == false)
            {
                p_anim.SetBool("IsMoving", true);
            }

        }
        else
        {
            if (an_Punch == false)
            {
                p_anim.SetBool("IsMoving", false);
            }
            
        }

        //New improved test for ground
        if (p_gCollision.IsTouching(groundCollider))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        //Jumping script
        if (isGrounded == true)
        {
            if (Input.GetKeyDown("space"))
            {
                m_rigidbody.AddForce(new Vector2(0, JumpPower));
            }
        }


        
        


        //Attack control

        if (Input.GetKeyDown("a"))
        {
            atk_collider.offset = new Vector2(atk_colX * -1,atk_collider.offset.y);
        }

        if (Input.GetKeyDown("d"))
        {
            atk_collider.offset = new Vector2(atk_colX, atk_collider.offset.y);
        }

        if (an_Punch == false)
        {
            if (Input.GetKeyDown("e"))
            {
                StartCoroutine(Punched());
            }
        }
    }






    void FixedUpdate()
    {
        //Movement scripts
        if (Input.GetKey("a"))
        {
            transform.position = new Vector2(transform.position.x - MovementSpeed, transform.position.y);
            p_spriterend.flipX = true;
            
            
            
        }

        if (Input.GetKey("d"))
        {
            transform.position = new Vector2(transform.position.x + MovementSpeed, transform.position.y);
            p_spriterend.flipX = false;

            
        }


    }


    
    //Detects damaging collision with enemy
    private void OnCollisionEnter2D(Collision2D collision)
    {

        GameObject enemy = collision.gameObject;
        //Enemy collision detection
        if (enemy.CompareTag("Enemy"))
        {
            if (p_IsInvincible == false)
            {
                //Roomba collision detection
                if (enemy.name.Contains("Roomba"))
                {
                    p_Health = p_Health - 1;
                    StartCoroutine(gotHit());
                }
            }
        }
    }

    //Detects if hit enemy with punch
    private void OnTriggerStay2D(Collider2D collision2)
    {
        GameObject enemy = collision2.gameObject;
        if (p_Punch == true)
        {
            if (enemy.name.Contains("Roomba"))
            {
                Debug.Log("attempted to kill roomba");
                enemy.GetComponent<roomba_ctrl>().killRoomba();
            }
        }
    }


    //Runs when got hit
    IEnumerator gotHit()
    {
        if (p_Health == 0)
        {
            string s_SceneName = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene(s_SceneName);
        }
        else
        {
            p_IsInvincible = true;
            p_spriterend.color = new Color(255, 0, 0);
            Physics2D.IgnoreLayerCollision(8, 11, true);
        }
        yield return new WaitForSeconds(1);
        p_IsInvincible = false;
        p_spriterend.color = new Color(255, 255, 255);
        Physics2D.IgnoreLayerCollision(8, 11, false);
    }

    //Runs when Martin punches
    IEnumerator Punched()
    {
        an_Punch = true;
        p_anim.Play("attack", -1, (1f / 2) * 2);
        yield return new WaitForSeconds(0.25f);
        p_Punch = true;
        p_anim.Play("attack", -1, (1f / 2) * 1);
        yield return new WaitForSeconds(0.25f);
        p_anim.Play("idle");
        p_Punch = false;
        an_Punch = false;
    }
}
