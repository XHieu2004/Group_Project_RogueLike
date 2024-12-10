using UnityEngine;

public class BulletScript : MonoBehaviour
{
    GameObject target;
    public float speed;
    Rigidbody2D bulletRB;
    Vector2 initialPosition; // Vị trí ban đầu của viên đạn
    public float maxDistance = 5f; // Khoảng cách tối đa viên đạn di chuyển trước khi biến mất

    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        initialPosition = transform.position; // Lưu vị trí ban đầu
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

    void Update()
    {
        // Kiểm tra nếu viên đạn đã di chuyển vượt quá khoảng cách tối đa
        if (Vector2.Distance(initialPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject); // Xóa đối tượng viên đạn
        }
    }
}
