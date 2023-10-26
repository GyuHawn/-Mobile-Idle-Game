 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    public MonsterSpwan spawner;
    private StageManger sManager;

    // �⺻ ü�� ��..
    private float baseMaxHealth; // ü��
    private float basePower; // ���ݷ�
    private float baseDefense; // ����
    private int baseMoney; // ����

    // ü�� ��..
    public float maxHealth; // ü�� = �⺻ü�� + (�⺻ü��* (��������/10)) 
    public float currentHealth;
    public float power; // ���ݷ� = �⺻���� + (�⺻���� * (��������/10)) 
    public float defense; // ���� = �⺻��� + (�⺻���* (��������/10))
    public int money; // ���� �� = �⺻ �� + (�⺻��* (��������/10))

    private float damege; // ������ = ���ݷ� - ����

    // ����
    public Transform pos;
    public Vector2 BoxSize;
    public LayerMask playerLayer;

    // �̵�
    public float spd;

    private Rigidbody2D rb;

    void Start()
    {
        spawner = GameObject.Find("Manager").GetComponent<MonsterSpwan>();
        sManager = GameObject.Find("Manager").GetComponent<StageManger>();
        rb = GetComponent<Rigidbody2D>();   
        spd = 5;

        // �⺻ ���� ����
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
        Debug.Log("SetStats : ȣ��");
        maxHealth = baseMaxHealth + (baseMaxHealth * (stage / 10));
        currentHealth = maxHealth;
        power = basePower + (basePower * (stage / 10));
        defense = baseDefense + (baseDefense * (stage / 10));
        money = baseMoney * stage;
    }

    // ���� ������ ������

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, BoxSize);
    }*/
}
