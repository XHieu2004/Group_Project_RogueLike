using UnityEngine;

public class BulletScript : MonoBehaviour
{
    GameObject target;
    public float speed;
    Rigidbody2D bulletRB;
    public int damage;

    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");

        if (target != null)
        {
            Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
            bulletRB.velocity = new Vector2(moveDir.x, moveDir.y);
        }
        else
        {
            Debug.LogError("Target not found. Bullet will not move.");
        }
    }
        private void OnTriggerEnter2D(Collider2D collision){
        // if (collision.CompareTag("Wall")){
        //     Explode();
        // }
        if (collision.CompareTag("Player")){
            Debug.Log("Bullet hit player!"); 
            PlayerHealth player= collision.GetComponent<PlayerHealth>();
            if(player != null)
            {
                player.TakeDamage(damage); 
            }

        }
        Destroy(gameObject);
    }
}
