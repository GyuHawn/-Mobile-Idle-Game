using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniGameBoss : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private MiniGameScript miniGameScript;

    public float damage = 10;

    // 스킬 프리팹
    public GameObject skill1;

    // 스킬 발사
    public GameObject[] skillPoint1;
    public GameObject[] skillEnermy1;
    public GameObject[] skillPoint2;
    public GameObject[] skillEnermy2;

    private int currentSkillIndex1 = 0;
    private int currentSkillIndex2 = 0;

    // 입은 데미지
    public float hitDamege;
    public GameObject hitDamageUI;
    public TMP_Text hitDamegeText;

    // 남은 시간 
    public float remainingTime;
    public TMP_Text remainingTimeText;

    private Animator anim;

    private void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        miniGameScript = GameObject.Find("Manager").GetComponent<MiniGameScript>();

        anim = GetComponent<Animator>();

        remainingTime = 60;
        StartCoroutine(SelectPattern());
    }

    private void Update()
    {
        if (miniGameScript.gameStarted)
        {
            if(hitDamege >= 0)
            {
                hitDamageUI.SetActive(true);
                hitDamegeText.text = "획득머니 : " + ((int)hitDamege / 10).ToString();
                remainingTimeText.text = "남은시간 : " + ((int)remainingTime).ToString();
            }
        }
        else 
        {
            hitDamege = 0;
            hitDamageUI.SetActive(false);
        }
    }

    IEnumerator SelectPattern()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            int pattern = Random.Range(0, 3);
            switch (pattern)
            {
                case 0:
                    StartCoroutine(Pattern1());
                    break;
                case 1:
                    StartCoroutine(Pattern2());
                    break;
                case 2:
                    StartCoroutine(Pattern3());
                    break;
            }
        }
    }

    IEnumerator Pattern1()
    {
        anim.SetBool("Attack", true);
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("Attack", false);
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (currentSkillIndex1 < skillPoint1.Length)
            {
                StartCoroutine(ActivateEnermy(skillEnermy1[currentSkillIndex1], 0.5f));

                GameObject skill = Instantiate(skill1, skillPoint1[currentSkillIndex1].transform.position, Quaternion.Euler(0, 180, 0));
                skill.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0);

                StartCoroutine(DestroyAfterReach(skill, -10));

                currentSkillIndex1++;
            }

            yield return new WaitForSeconds(0.5f);

            if (currentSkillIndex2 < skillPoint2.Length)
            {
                StartCoroutine(ActivateEnermy(skillEnermy2[currentSkillIndex2], 0.5f));

                GameObject skill = Instantiate(skill1, skillPoint2[currentSkillIndex2].transform.position, Quaternion.identity);
                skill.GetComponent<Rigidbody2D>().velocity = new Vector2(5, 0);

                StartCoroutine(DestroyAfterReach(skill, 10));

                currentSkillIndex2++;
            }
        }
    }

    IEnumerator Pattern2()
    {
        anim.SetBool("Attack", true);
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("Attack", false);

        for (int i = 0; i < 5; i++)
        {
            int randomIndex1 = Random.Range(0, skillPoint1.Length);
            StartCoroutine(ActivateEnermy(skillEnermy1[randomIndex1], 0.5f));
            GameObject skill = Instantiate(skill1, skillPoint1[randomIndex1].transform.position, Quaternion.Euler(0, 180, 0));
            skill.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0);
            StartCoroutine(DestroyAfterReach(skill, -10));
            yield return new WaitForSeconds(1f);

            int randomIndex2 = Random.Range(0, skillPoint2.Length);
            StartCoroutine(ActivateEnermy(skillEnermy2[randomIndex2], 0.5f));
            skill = Instantiate(skill1, skillPoint2[randomIndex2].transform.position, Quaternion.identity);
            skill.GetComponent<Rigidbody2D>().velocity = new Vector2(5, 0);
            StartCoroutine(DestroyAfterReach(skill, 10));
            yield return new WaitForSeconds(1f);
        }
    }


    IEnumerator Pattern3()
    {
        anim.SetBool("Attack", true);
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("Attack", false);

        for (int i = 0; i < skillPoint1.Length; i++)
        {
            StartCoroutine(ActivateEnermy(skillEnermy1[i], 0.5f));
            GameObject skill = Instantiate(skill1, skillPoint1[i].transform.position, Quaternion.Euler(0, 180, 0));
            skill.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0);
            StartCoroutine(DestroyAfterReach(skill, -10));
        }
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < skillPoint2.Length; i++)
        {
            StartCoroutine(ActivateEnermy(skillEnermy2[i], 0.5f));
            GameObject skill = Instantiate(skill1, skillPoint2[i].transform.position, Quaternion.identity);
            skill.GetComponent<Rigidbody2D>().velocity = new Vector2(5, 0);
            StartCoroutine(DestroyAfterReach(skill, 10));
        }
    }

    IEnumerator ActivateEnermy(GameObject enermy, float time)
    {
        enermy.SetActive(true);
        yield return new WaitForSeconds(time);
        enermy.SetActive(false);
    }

    IEnumerator DestroyAfterReach(GameObject skill, float x)
    {
        while (true)
        {
            if ((x < 0 && skill.transform.position.x <= x) || (x > 0 && skill.transform.position.x >= x))
            {
                Destroy(skill);
                break;
            }
            yield return null;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hitDamege += playerMovement.power;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Skill1"))
        {
            hitDamege += playerMovement.skillPower;
        }
    }
}