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
    [SerializeField] private float moveSpeed = 3.5f; // Speed for moving forward

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

        // Disable obstacle avoidance for smoother forward movement
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        agent.enabled = false; // Disable NavMeshAgent for transform.forward-based movement
    }

    private void Update()
    {
        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget <= safeDistance && distanceToTarget > stopDistance)
            {
                // Move towards the player using transform.forward
                isPlayerInRange = false;
                MoveTowardsTarget();
                SetAnimationState(run: true, idle: false);
            }
            else if (distanceToTarget <= stopDistance)
            {
                // Stop moving and attack
                isPlayerInRange = true;
                SetAnimationState(run: false, idle: false);

                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    PerformForwardAttack();
                    lastAttackTime = Time.time;
                }
            }
            else
            {
                // Idle when out of range
                isPlayerInRange = false;
                SetAnimationState(run: false, idle: true);
            }

            // Flip sprite based on player's position
            FlipSprite(target.position.x);
        }
    }

    // Move towards the target using transform.forward
    private void MoveTowardsTarget()
    {
        // Rotate towards the target
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, directionToTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.deltaTime);

        // Move forward in the direction of transform.forward
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    // Perform a forward attack
    private void PerformForwardAttack()
    {
        animator.SetTrigger("Attack");
        Debug.Log("RatEnemy is performing a forward attack!");
    }

    // Flip the sprite based on the player's position
    private void FlipSprite(float targetX)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = targetX < transform.position.x;
        }
    }

    // Set animation state for "Run" and "Idle"
    private void SetAnimationState(bool run, bool idle)
    {
        if (animator != null)
        {
            animator.SetBool("Run", run);
            animator.SetBool("Idle", idle);
        }
    }

    // Animation event handler for attack
    public void RatEndAttack()
    {
        Debug.Log("Animation event: Attack finished.");
    }
}
