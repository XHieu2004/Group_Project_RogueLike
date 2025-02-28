using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class ProjectTile : MonoBehaviour
{
    public float bulletSpeed;
    private Vector2 direction;
    private bool hit;
    public Animator anim;
    private CapsuleCollider2D capCollider;
    private Rigidbody2D rb;
    private float lifetime;
    public int damage = 20;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // capCollider = GetComponent<CapsuleCollider2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    private void FixedUpdate()
    {
        if (hit) return;
        rb.velocity = direction * bulletSpeed;
        lifetime += Time.deltaTime;
        if (lifetime > 10) gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       Explode();
       return;
       //No need for this
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("Enemy") || collider2D.CompareTag("Enemyroom1") || collider2D.CompareTag("Enemyroom2") || collider2D.CompareTag("Enemyroom2.2")
        || collider2D.CompareTag("Enemyroom3") || collider2D.CompareTag("Enemyroom4") || collider2D.CompareTag("Enemyroom5") || collider2D.CompareTag("Boss") )
        {
            Debug.Log("Bullet hit enemy!");
            EnemyHealth enemy = collider2D.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Explode();
            return;
        }

        if (collider2D.gameObject.layer == LayerMask.NameToLayer("Ground") || collider2D.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
             Explode();
              return;
        }
    }

    public void Explode()
    {
        hit = true;
        // capCollider.enabled = false; 
        rb.velocity = Vector2.zero;
        anim.SetTrigger("Explode");

        Invoke("Deactivate", 0.3f);
    }

    public void SetDirection(Vector2 _direction)
    {
        lifetime = 0;
        direction = _direction.normalized;
        gameObject.SetActive(true);
        hit = false;
        

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        float localScaleX = Mathf.Abs(transform.localScale.x);
        if (direction.x < 0)
        {
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
        
    }
}