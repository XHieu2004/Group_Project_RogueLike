using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RatEnemy : MonoBehaviour
{
    [SerializeField] private Transform target; // Player's position
    [SerializeField] private float safeDistance = 10f; // Range for "Run" animation
    [SerializeField] private float stopDistance = 1.5f; // Range for "Attack" animation
    [SerializeField] private float attackCooldown = 2f; // Cooldown between attacks
    [SerializeField] private int damage = 10; // Damage dealt to the player

    private NavMeshAgent agent;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private float lastAttackTime = -Mathf.Infinity; // Time of the last attack
    private bool isPlayerInRange = false; // Track if the player is within attack range

    private void OnDrawGizmos()
    {
        // Visualize safeDistance and stopDistance
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, safeDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
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

        // Set stopping distance for NavMeshAgent
        agent.stoppingDistance = stopDistance;
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
    }

    private void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (target != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position);

            if (distanceToTarget <= safeDistance && distanceToTarget > stopDistance + 0.1f)
            {
                // Player within safeDistance but not stopDistance
                isPlayerInRange = false;
                agent.isStopped = false; // Allow movement
                agent.SetDestination(target.position);
                SetAnimationState(run: true, idle: false);
            }
            else if (distanceToTarget <= stopDistance + 0.1f)
            {
                // Player within attack range
                isPlayerInRange = true;
                agent.isStopped = true; // Stop movement
                agent.ResetPath();
                SetAnimationState(run: false, idle: false);

                // Attack if cooldown has elapsed
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    PerformAttack();
                    lastAttackTime = Time.time;
                }
            }
            else if (distanceToTarget > safeDistance)
            {
                // Player out of range
                isPlayerInRange = false;
                agent.isStopped = true; // Stop movement
                agent.ResetPath();
                SetAnimationState(run: false, idle: true);
            }

            // Flip sprite based on player's position
            FlipSprite(target.position.x);
        }
    }

    private void PerformAttack()
    {
        animator.SetTrigger("Attack");
        DealDamageToPlayer();
    }

    private void FlipSprite(float targetX)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = targetX <= transform.position.x;
        }
    }

    private void SetAnimationState(bool run, bool idle)
    {
        if (animator != null)
        {
            animator.SetBool("Run", run);
            animator.SetBool("Idle", idle);
        }
    }

    public void EndAttack()
    {
        Debug.Log("Animation event: Attack finished.");
    }

    private void DealDamageToPlayer()
    {
        if (isPlayerInRange && target != null)
        {
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log($"Player took {damage} damage from RatEnemy.");
            }
            else
            {
                Debug.LogError("Target does not have a PlayerHealth component!");
            }
        }
    }
}
