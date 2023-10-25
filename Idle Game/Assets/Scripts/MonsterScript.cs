 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    // 체력 등..
    public float maxHealth;  // 미사용
    public float currentHealth; // 미샤용
    public float damage; // 미사용
    public float defense; // 미사용

    // 감지
    public Transform pos;
    public Vector2 BoxSize;
    public LayerMask playerLayer;

    // 이동
    public float spd;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        spd = 10;
    }

    void Update()
    {
        Collider2D playerInBox = Physics2D.OverlapBox(pos.position, BoxSize, 0f, playerLayer);

        if (playerInBox != null)
        {
            Vector3 direction = (playerInBox.transform.position - transform.position).normalized;
            rb.velocity = new Vector3(direction.x * spd, direction.y * spd);
        }
        else
        {
            rb.velocity = Vector3.zero; // Player가 없다면 정지
        }


    }

    // 공격 입을시 데미지

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, BoxSize);
    }
}
