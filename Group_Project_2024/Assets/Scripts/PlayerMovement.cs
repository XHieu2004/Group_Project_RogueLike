using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    public Vector2 moveDir;
    public Vector2 lastPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        lastPosition = rb.position; // Initialize last position
    }

    void Update()
    {
        InputManagement();
    }

    void FixedUpdate()
    {
        Move();
    }

    void InputManagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        // Calculate target position based on input
        Vector2 targetPosition = rb.position + moveDir * speed * Time.fixedDeltaTime;

        if (IsPositionWalkable(targetPosition))
        {
            rb.MovePosition(targetPosition); // Use MovePosition for Rigidbody2D movement
            lastPosition = targetPosition;  // Update last valid position
        }
        else
        {
            Debug.Log("Blocked: Not Walkable");
        }
    }

    bool IsPositionWalkable(Vector2 position)
    {
        // Convert 2D position to 3D for NavMesh sampling
        Vector3 position3D = new Vector3(position.x, position.y, 0f);
        NavMeshHit hit;

        // Check if the position is on a walkable NavMesh
        if (NavMesh.SamplePosition(position3D, out hit, 1.0f, NavMesh.AllAreas))
        {
            Debug.Log($"Walkable: {hit.position}");
            return hit.mask != 0; // Return true if on a walkable surface
        }

        Debug.Log("Not Walkable");
        return false;
    }
}
