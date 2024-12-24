using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator anim;
    PlayerMovement pm;
    SpriteRenderer sr;

    void Start()
    {
        anim = GetComponent<Animator>();
        pm = GetComponent<PlayerMovement>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Handle animations based on movement direction
        if (pm.moveDir.x != 0 || pm.moveDir.y != 0)
        {
            anim.SetBool("Move", true);
            UpdateSpriteDirection();
        }
        else
        {
            anim.SetBool("Move", false);
        }
    }

    void UpdateSpriteDirection()
    {
        // Flip the sprite based on horizontal movement
        if (pm.moveDir.x < 0)
        {
            sr.flipX = true;
        }
        else if (pm.moveDir.x > 0)
        {
            sr.flipX = false;
        }
    }
}
