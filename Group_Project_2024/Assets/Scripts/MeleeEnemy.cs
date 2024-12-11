using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] Transform target; // The player's position
    [SerializeField] float safeDistance = 10f; // Desired range to start approaching player for melee
    NavMeshAgent agent;
    SpriteRenderer spriteRenderer;
    Animator animator;

    [SerializeField] private float attackCooldown = 2f; // Cooldown between melee attacks
    private float lastAttackTime = -Mathf.Infinity; // Time of the last attack

    private void OnDrawGizmos()
    {
        // Set the Gizmo color for the safe distance
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, safeDistance);
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component missing on this GameObject!");
        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component missing on this GameObject!");
        }
    }

    private void Update()
    {
        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget <= safeDistance)
            {
                // Close in on the player
                agent.SetDestination(target.position);
            }

            // Melee attack logic
            if (distanceToTarget <= agent.stoppingDistance && Time.time >= lastAttackTime + attackCooldown)
            {
                // Switch to attack animation
                animator.SetBool("Enemy_Attack", true);

                // Perform the attack (you can add damage logic here)
                Debug.Log("Melee Attack!");

                lastAttackTime = Time.time; // Update the time of the last attack
            }
            else
            {
                animator.SetBool("Enemy_Attack", false);
            }

            // Flip sprite based on the player's position
            FlipSprite(target.position.x);
        }
    }

    // Flip the sprite based on the player's position
    private void FlipSprite(float targetX)
    {
        if (spriteRenderer != null)
        {
            // Flip the sprite if the target is on the left or right
            spriteRenderer.flipX = targetX >= transform.position.x;
        }
    }
}
