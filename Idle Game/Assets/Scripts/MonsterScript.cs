using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public MonsterSpawn spawner;
    private StageManager sManager;

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
    public static int money; // 몬스터 돈 = 기본 돈 + (기본돈* (스테이지/10))

    private float damege; // 데미지 = 공격력 - 방어력

    // 감지
    public Transform pos;
    public Vector2 boxSize;
    public LayerMask playerLayer;

    // 이동
    public float spd;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spawner = GameObject.Find("Manager").GetComponent<MonsterSpawn>();
        sManager = GameObject.Find("Manager").GetComponent<StageManager>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spd = 5;

        bool isBoss = gameObject.CompareTag("Boss");

        if (isBoss)
        {
            // Boss일 경우, 기본 스탯 5배
            baseMaxHealth = 50;
            basePower = 25;
            baseDefense = 5;
            baseMoney = 50;
        }
        else
        {
            // 기본 스탯 설정
            baseMaxHealth = 10;
            basePower = 5;
            baseDefense = 0;
            baseMoney = 10;
        }

        SetStats(sManager.stage);
    }

    void Update()
    {
        var playerObject = GameObject.Find("Player");
        if (playerObject != null)
            playerMovement = playerObject.GetComponent<PlayerMovement>();
        else
            return;

        // 데미지 설정
        damege = playerMovement.power - defense;


        Collider2D playerInBox = Physics2D.OverlapBox(pos.position, boxSize, 0f, playerLayer);
        if (playerInBox != null)
        {
            Vector3 direction = (playerInBox.transform.position - transform.position).normalized;
            rb.velocity = new Vector3(direction.x * spd, direction.y * spd);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        if(currentHealth <= 0)
        {
            Destroy(gameObject);
            sManager.deadMonster++;
            playerMovement.money += money;
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
        maxHealth = baseMaxHealth + (stage * 2);
        currentHealth = maxHealth;
        power = basePower + (stage * 2);
        defense = baseDefense + (stage * 2);
        money = baseMoney * stage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            currentHealth -= damege;
            StartCoroutine(FlashWhite());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Skill1"))
        {
            currentHealth -= playerMovement.skillPower;
        }
    }

    IEnumerator FlashWhite()
    {
        if (gameObject.CompareTag("Monster"))
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
        }     
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }*/
}
