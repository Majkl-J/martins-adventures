using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomba_ctrl : MonoBehaviour
{
    [SerializeField] private Transform m_PlayerCheck;
    [SerializeField] float playerRadius;
    [SerializeField] private LayerMask m_WhatIsPlayer;
    [SerializeField] private Transform m_Player;
    [SerializeField] float movement_speed;
    [SerializeField] SpriteRenderer e_RoombaSprite;
    [SerializeField] Animator e_RoombaAnimator;

    bool inRadius;
    void Start()
    {
        
    }

    void Update()
    {
        //Tests for player in radius
        bool wasGrounded = inRadius;
        inRadius = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_PlayerCheck.position, playerRadius, m_WhatIsPlayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                inRadius = true;
            }
        }
    }
    void FixedUpdate()
    {
        // Makes roomba move towards player
        // Help my friend's house is getting haunted
        if (inRadius==true)
        {
            if (m_Player.position.x - transform.position.x <= 0)
            {
                transform.position = new Vector2(transform.position.x - movement_speed, transform.position.y);
                e_RoombaSprite.flipX = false;
            }
            else if (m_Player.position.x - transform.position.x >= 0)
            {
                transform.position = new Vector2(transform.position.x + movement_speed, transform.position.y);
                e_RoombaSprite.flipX = true;
            }
            e_RoombaAnimator.Play("mainanim", -1, (1f / 2) * 1);
        }
        else
        {
            //animator.Play ("AnimationName", 1, ( 1f / total_frames_in_animation ) * desired_frame);
            e_RoombaAnimator.Play("mainanim", -1, (1f / 2) * 2);
            
        }
    }
}
