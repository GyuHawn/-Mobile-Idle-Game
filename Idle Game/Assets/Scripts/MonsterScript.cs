 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    public MonsterSpwan spawner;
    private StageManger sManager;

    // 기본 체력 등..
    private float baseMaxHealth; // 체력
    private float basePower; // 공격력
    private float baseDefense; // 방어력
    private int baseMoney; // 방어력

    // 체력 등..
    public float maxHealth; // 체력 = 기본체력 + (기본체력* (스테이지/10)) 
    public float currentHealth;
    public float power; // 공격력 = 기본공격 + (기본공격 * (스테이지/10)) 
    public float defense; // 방어력 = 기본방어 + (기본방어* (스테이지/10))
    public int money; // 몬스터 돈 = 기본 돈 + (기본돈* (스테이지/10))

    private float damege; // 데미지 = 공격력 - 방어력

    // 감지
    public Transform pos;
    public Vector2 BoxSize;
    public LayerMask playerLayer;

    // 이동
    public float spd;

    private Rigidbody2D rb;

    void Start()
    {
        spawner = GameObject.Find("Manager").GetComponent<MonsterSpwan>();
        sManager = GameObject.Find("Manager").GetComponent<StageManger>();
        rb = GetComponent<Rigidbody2D>();   
        spd = 5;

        // 기본 스탯 설정
        baseMaxHealth = 15;
        basePower = 5;
        baseDefense = 0;
        baseMoney = 10;

        SetStats(sManager.stage);
        Debug.Log("Start: " + currentHealth);
    }

    void Update()
    {
        Debug.Log("Update: " + currentHealth);
        Collider2D playerInBox = Physics2D.OverlapBox(pos.position, BoxSize, 0f, playerLayer);

        if (playerInBox != null)
        {
            Vector3 direction = (playerInBox.transform.position - transform.position).normalized;
            rb.velocity = new Vector3(direction.x * spd, direction.y * spd);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.activeMonsters--;
        }
    }
    public void SetStats(int stage)
    {
        Debug.Log("SetStats : 호출");
        maxHealth = baseMaxHealth + (baseMaxHealth * (stage / 10));
        currentHealth = maxHealth;
        power = basePower + (basePower * (stage / 10));
        defense = baseDefense + (baseDefense * (stage / 10));
        money = baseMoney * stage;
    }

    // 공격 입을시 데미지

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, BoxSize);
    }*/
}
