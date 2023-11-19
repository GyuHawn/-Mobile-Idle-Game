using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public float delay;
    public GameObject target;

    SpriteRenderer spriteRenderer;

    private void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        delay = 3f;
    }

    private void Update()
    {
        if (target == null) 
        {
            Destroy(gameObject);
            return;
        }

        Destroy(gameObject, delay);

        if (playerMovement.useSkill3)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            spriteRenderer.color = Color.yellow;
            StartCoroutine(SkillUpgrade());
        }
    }

    IEnumerator SkillUpgrade()
    {
        yield return new WaitForSeconds(5f);
        transform.localScale = new Vector3(0.3f, 0.3f, 1f);
        spriteRenderer.color = Color.red;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster") || collision.gameObject.CompareTag("Boss") || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("MiniGame"))
        {
            Destroy(gameObject);
        }
    }
}