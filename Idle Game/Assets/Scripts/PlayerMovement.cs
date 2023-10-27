using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // 기본 체력 등..
    private float baseMaxHealth; // 체력
    private float basePower; // 공격력
    private float baseDefense; // 방어력

    // 체력 등..
    private float maxHealth; // 체력 = 기본체력 + 업글체력
    private float currentHealth;
    public float power; // 공격력 = 기본공격 + 업글공격
    private float defense; // 방어력 = 기본방어 + 업글방어

    private int level; // 업글레벨 (체 + 공 + 방 업글)
    private float totalPower; // 투력 = 공격력(100%) + 방어(50%) + 체력(50%)
    public float damege; // 데미지 = 공격력 - 방어력
    public int money; // 현재 돈

    // 업글 스탯
    private float upgradeHealth;
    private float upgradePower;
    private float upgradeDefense;

    // 이동
    public float spd;
    public Vector2 inputVec;

    // 감지
    public Transform pos;
    public Vector2 BoxSize;
    private Collider2D currentTarget;  // 현재 타겟

    // 공격
    public GameObject bulletPrefab;
    public Transform shoot;
    public float bulletSpeed;

    // 사망
    private bool isDead;

    private Rigidbody2D rigib;

    private void Awake()
    {
        rigib = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        spd = 0.2f;
        bulletSpeed = 10;

        // 기본 스탯 설정
        baseMaxHealth = 1;
        basePower = 15;
        baseDefense = 1;

        // 변동 스탯
        SetStats();

        power = basePower; // 일단 몬스터 처리를 위한 선언
        StartCoroutine(AutoShoot());
    }

    IEnumerator AutoShoot() // 자동 사격
    {
        while (true)
        {
            if (IsValidTarget(currentTarget))
            {
                ShootBullet();
            }
            yield return new WaitForSeconds(.5f); // 공격 속도 조절   
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

        // 타겟이 없을때 새로운 적 타겟
        if (!IsValidTarget(currentTarget))
        {
            currentTarget = GetRandomMonster();
        }
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
        Die();  
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

 
    private Collider2D GetRandomMonster()  // 랜덤 몬스터 추적
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(pos.position, BoxSize, 0f);

        List<Collider2D> monsters = new List<Collider2D>();

        foreach (Collider2D collider in colliders)
            if (collider.CompareTag("Monster"))
                monsters.Add(collider);

        return monsters.Count > 0 ? monsters[Random.Range(0, monsters.Count)] : null;
    }

    private bool IsValidTarget(Collider2D target) // 몬스터 감지
    {
        // 타겟이 null 이거나 Monster 태그가 없으면 false 반환
        return target != null && target.CompareTag("Monster");
    }

    private void ShootBullet() // 사격
    {
        if (IsValidTarget(currentTarget))
        {
            GameObject bullet = Instantiate(bulletPrefab, shoot.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity =
                (currentTarget.transform.position - shoot.position).normalized * bulletSpeed;
        }
    }
    
    void Die()
    {
        if (!isDead && currentHealth <= 0)
        {
            isDead = true;
            transform.position = new Vector2(0, 0);
            MonsterSpwan spawner = GameObject.Find("Manager").GetComponent<MonsterSpwan>();
            StageManger stageManger = GameObject.Find("Manager").GetComponent<StageManger>();
            if (spawner != null)
            {
                spawner.ResetStage();
            }
            stageManger.deadMonster = 0;
            SetStats();
            isDead = false;
        }
    }

    public void SetStats()
    {
        maxHealth = baseMaxHealth;
        currentHealth = maxHealth;
        power = basePower;
        defense = baseDefense;
    }
        private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            MonsterScript monsterScript = collision.gameObject.GetComponent<MonsterScript>();

            if (monsterScript != null)
            {
                damege = monsterScript.power - defense;
                currentHealth -= damege;
            }
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, BoxSize);
    }*/
}
