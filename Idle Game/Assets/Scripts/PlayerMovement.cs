using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // �⺻ ü�� ��..
    private float baseMaxHealth; // ü��
    private float basePower; // ���ݷ�
    private float baseDefense; // ����

    // ü�� ��..
    private float maxHealth; // ü�� = �⺻ü�� + ����ü��
    private float currentHealth;
    private float power; // ���ݷ� = �⺻���� + ���۰���
    private float defense; // ���� = �⺻��� + ���۹��

    private int level; // ���۷��� (ü + �� + �� ����)
    private float totalPower; // ���� = ���ݷ�(100%) + ���(50%) + ü��(50%)
    private float damege; // ������ = ���ݷ� - ����
    public int money; // ���� ��

    // ���� ����
    private float upgradeHealth;
    private float upgradePower;
    private float upgradeDefense;

    // �̵�
    public float spd;
    public Vector2 inputVec;

    // ����
    public Transform pos;
    public Vector2 BoxSize;
    private Collider2D currentTarget;  // ���� Ÿ��

    // ����
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

        // �⺻ ���� ����
        baseMaxHealth = 100;
        basePower = 10;
        baseDefense = 1;

        StartCoroutine(AutoShoot());
    }

    IEnumerator AutoShoot() // �ڵ� ���
    {
        while (true)
        {
            if (IsValidTarget(currentTarget))
            {
                ShootBullet();
            }
            yield return new WaitForSeconds(0.5f);   
        }
    }

    private void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBullet();
        }

        // Ÿ���� ������ ���ο� �� Ÿ��
        if (!IsValidTarget(currentTarget))
        {
            currentTarget = GetRandomMonster();
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

 
    private Collider2D GetRandomMonster()  // ���� ���� ����
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(pos.position, BoxSize, 0f);

        List<Collider2D> monsters = new List<Collider2D>();

        foreach (Collider2D collider in colliders)
            if (collider.CompareTag("Monster"))
                monsters.Add(collider);

        return monsters.Count > 0 ? monsters[Random.Range(0, monsters.Count)] : null;
    }

    private bool IsValidTarget(Collider2D target) // ���� ����
    {
        // Ÿ���� null �̰ų� Monster �±װ� ������ false ��ȯ
        return target != null && target.CompareTag("Monster");
    }

    private void ShootBullet() // ���
    {
        if (IsValidTarget(currentTarget))
        {
            GameObject bullet = Instantiate(bulletPrefab, shoot.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity =
                (currentTarget.transform.position - shoot.position).normalized * bulletSpeed;
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, BoxSize);
    }*/
}
