using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
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

    public EnemySpawner[] enemySpawners;
    private EnemyHealth enemyHealth;
    private int spawnCount = 0;

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
        enemyHealth = GetComponent<EnemyHealth>();

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
            // Handle movement and animations based on the player's distance
            Debug.Log("Distance " + distanceToTarget);
            if(distanceToTarget <= safeDistance && distanceToTarget > stopDistance)
            {
                // Player within safeDistance but not stopDistance
                isPlayerInRange = false;
                agent.SetDestination(target.position);
                SetAnimationState(run: true, idle: false);
                Debug.Log("Chase");
            }
            else if (distanceToTarget <= stopDistance)
            {
                // Player within attack range
                isPlayerInRange = true;
                agent.ResetPath();
                SetAnimationState(run: false, idle: false);

                // Attack if cooldown has elapsed
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    PerformAttack();
                    lastAttackTime = Time.time;
                }
                Debug.Log("In range");
            }
            else if(distanceToTarget > safeDistance)
            {
                // Player out of range
                isPlayerInRange = false;
                agent.ResetPath();
                SetAnimationState(run: false, idle: true);
                Debug.Log("Out of range");
            }

            // Flip sprite based on player's position
            FlipSprite(target.position.x);
            CheckHealth();
        }
    }

    void CheckHealth()
    {
        if (enemyHealth.currentHealth <= enemyHealth.maxHealth*0.75 && spawnCount == 0)
        {
            animator.SetTrigger("Attack 1");
            foreach(var item in enemySpawners)
            {
                StartCoroutine(item.SpawnBossEnemyCoroutine());
            }
            spawnCount++;
            Debug.Log(spawnCount.ToString());
        }
        if(enemyHealth.currentHealth <= enemyHealth.maxHealth * 0.5 && spawnCount == 1)
        {
            animator.SetTrigger("Attack 1");
            foreach (var item in enemySpawners)
            {
                StartCoroutine(item.SpawnBossEnemyCoroutine());
            }
            spawnCount++;
            Debug.Log(spawnCount.ToString());
        }
        if (enemyHealth.currentHealth <= enemyHealth.maxHealth * 0.25 && spawnCount == 2)
        {
            animator.SetTrigger("Attack 1");
            foreach (var item in enemySpawners)
            {
                StartCoroutine(item.SpawnBossEnemyCoroutine());
            }
            spawnCount++;
            Debug.Log(spawnCount.ToString());
        }

    }

    // Perform the single attack animation
    private void PerformAttack()
    {
        animator.SetTrigger("Attack 2");
        Debug.Log("Performing attack: Single Attack");

        // Cause damage to the player if within range
        DealDamageToPlayer();
    }

    // Deal damage to the player
    private void DealDamageToPlayer()
    {
        if (isPlayerInRange && target != null)
        {
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Debug.Log($"Player took {damage} damage from Boss.");
            }
            else
            {
                Debug.LogError("Target does not have a PlayerHealth component!");
            }
        }
    }

    // Flip the sprite based on the player's position
    private void FlipSprite(float targetX)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = targetX <= transform.position.x;
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

    // Animation event handler for "GolemEndAbility"
    public void BossAbility()
    {
        // Logic to handle the end of an attack or ability animation
        Debug.Log("Animation event: BossAbility triggered.");
    }
}
