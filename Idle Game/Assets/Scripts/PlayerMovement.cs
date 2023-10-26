using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // 체력 등..
    public float maxHealth;  // 미사용
    public float currentHealth; // 미샤용
    public float damage; // 미사용
    public float defense; // 미사용

    // 이동
    public float spd;
    public Vector2 inputVec;

    // 감지
    public Transform pos;
    public Vector2 BoxSize;

    // 공격
    public GameObject bulletPrefab;
    public Transform shoot;
    public float bulletSpeed;
    

    private Rigidbody2D rigib;

    private void Awake()
    {
        rigib = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        spd = 0.2f;
        bulletSpeed = 3;
    }

    private void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBullet();
        }
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    void Move()
    {
        Vector2 nextVec = inputVec.normalized * spd;
        rigib.MovePosition(rigib.position + nextVec);
    }

    void Turn()
    {
        if (inputVec.x != 0)
        {
            float rotationY = inputVec.x > 0 ? 0f : 180f;
            transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
        }
    }

    private void ShootBullet()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(pos.position, BoxSize, 0f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Monster"))
            {
                GameObject bullet = Instantiate(bulletPrefab, shoot.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = (collider.transform.position - shoot.position).normalized * bulletSpeed;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, BoxSize);
    }
}
