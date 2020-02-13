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
    SpriteRenderer p_spriterend;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] float groundedRadius;
    [SerializeField] Animator p_anim;
    [SerializeField] Collider2D p_eCollision;
    [SerializeField] Collider2D p_gCollision;
    [SerializeField] Collider2D groundCollider;
    [SerializeField] Collider2D e_RoombaCollider;
    [SerializeField] int p_MaxInvincibilityFrames = 120;
    [SerializeField] int p_StartHealth = 3;
    public static int p_Health;
    bool p_IsInvincible;
    bool p_GotHit;
    int p_InvincibilityFrames;
    private void Start()
    {
        p_IsInvincible = false;
        p_InvincibilityFrames = p_MaxInvincibilityFrames;
        p_Health = p_StartHealth;
    }
    void Update()
    {
        
        //Animation control
        if (Input.GetKey("a"))
        {

            p_anim.SetBool("IsMoving", true);
        }
        else if (Input.GetKey("d"))
        {

            p_anim.SetBool("IsMoving", true);
        }
        else
        {
            p_anim.SetBool("IsMoving", false);
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

        //Enemy collision detection, will get reworked later probably
        if (p_IsInvincible == false)
        {
            //Roomba collision detection
            if (p_eCollision.IsTouching(e_RoombaCollider))
            {
                p_Health = p_Health - 1;
                p_GotHit = true;
            }
        }

        //Restarts the level when hp reaches 0
        if (p_Health == 0)
        {
            string s_SceneName = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene(s_SceneName);
        }

    }


    void FixedUpdate()
    {
        //Movement scripts
        if(Input.GetKey("a"))
        {
            transform.position = new Vector2(transform.position.x - MovementSpeed,transform.position.y);
            p_spriterend.flipX = true;
            p_anim.SetBool("IsMoving", true);
        }

        if (Input.GetKey("d"))
        {
            transform.position = new Vector2(transform.position.x + MovementSpeed, transform.position.y);
            p_spriterend.flipX = false;
            p_anim.SetBool("IsMoving", true);
        }
        if (p_GotHit == true)
        {
            p_InvincibilityFrames = p_InvincibilityFrames - 1;
            p_IsInvincible = true;
            p_GotHit = false;
        }
        else if (p_InvincibilityFrames <= p_MaxInvincibilityFrames - 1)
        {
            if (p_InvincibilityFrames >= 1) {
                p_InvincibilityFrames = p_InvincibilityFrames - 1;
            }
            else if (p_InvincibilityFrames == 0)
            {
                p_IsInvincible = false;
                p_InvincibilityFrames = p_MaxInvincibilityFrames;
            }
        }
    }
    
}
