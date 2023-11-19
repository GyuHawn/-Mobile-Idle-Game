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
    public List<GameObject> skills = new List<GameObject>(); // 생성된 스킬;
    public GameObject skill1;

    // 스킬 발사
    public GameObject[] skillPoint1;
    public GameObject[] skillEnermy1;
    public GameObject[] skillPoint2;
    public GameObject[] skillEnermy2;

    public int currentSkillIndex1 = 0;
    public int currentSkillIndex2 = 0;

    // 입은 데미지
    public float hitDamege;
    //public GameObject hitDamageUI;
    //public TMP_Text hitDamegeText;

    // 남은 시간 
   // public float remainingTime;
    //public TMP_Text remainingTimeText;

    private Animator anim;

    private void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        miniGameScript = GameObject.Find("Manager").GetComponent<MiniGameScript>();

        anim = GetComponent<Animator>();

        // 스킬 사용위치 할당
        skillPoint1 = new GameObject[3];
        skillPoint1[0] = GameObject.Find("SkillPoint1");
        skillPoint1[1] = GameObject.Find("SkillPoint2");
        skillPoint1[2] = GameObject.Find("SkillPoint3");

        skillPoint2 = new GameObject[2];
        skillPoint2[0] = GameObject.Find("SkillPoint4");
        skillPoint2[1] = GameObject.Find("SkillPoint5");

        // 스킬 위헙지역 할당
        skillEnermy1 = new GameObject[3];
        skillEnermy1[0] = GameObject.Find("Skill1Enermy");
        skillEnermy1[1] = GameObject.Find("Skill2Enermy");
        skillEnermy1[2] = GameObject.Find("Skill3Enermy");

        skillEnermy2 = new GameObject[2];
        skillEnermy2[0] = GameObject.Find("Skill4Enermy");
        skillEnermy2[1] = GameObject.Find("Skill5Enermy");

        StartCoroutine(SelectPattern());
    }

    private void Update()
    {
        if (miniGameScript.miniGameStart)
        {
            if(hitDamege >= 0)
            {
                miniGameScript.hitDamageUI.SetActive(true);
                miniGameScript.hitDamegeText.text = "획득머니 : " + ((int)hitDamege / 10).ToString();
                miniGameScript.remainingTimeText.text = "남은시간 : " + ((int)miniGameScript.remainingTime).ToString();
            }
        }
        else 
        {
            hitDamege = 0;
            miniGameScript.hitDamageUI.SetActive(false);
        }
    }

    IEnumerator SelectPattern()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);

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
                StartCoroutine(ActEnermy(skillEnermy1[currentSkillIndex1], 0.5f));

                GameObject skill = Instantiate(skill1, skillPoint1[currentSkillIndex1].transform.position, Quaternion.Euler(0, 180, 0));
                skill.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0);
                skills.Add(skill);
                currentSkillIndex1++;
            }

            yield return new WaitForSeconds(0.5f);

            if (currentSkillIndex2 < skillPoint2.Length)
            {
                StartCoroutine(ActEnermy(skillEnermy2[currentSkillIndex2], 0.5f));

                GameObject skill = Instantiate(skill1, skillPoint2[currentSkillIndex2].transform.position, Quaternion.identity);
                skill.GetComponent<Rigidbody2D>().velocity = new Vector2(5, 0);
                skills.Add(skill);
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
            StartCoroutine(ActEnermy(skillEnermy1[randomIndex1], 0.5f));
            GameObject skill = Instantiate(skill1, skillPoint1[randomIndex1].transform.position, Quaternion.Euler(0, 180, 0));
            skill.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0);
            skills.Add(skill);
            yield return new WaitForSeconds(1f);

            int randomIndex2 = Random.Range(0, skillPoint2.Length);
            StartCoroutine(ActEnermy(skillEnermy2[randomIndex2], 0.5f));
            skill = Instantiate(skill1, skillPoint2[randomIndex2].transform.position, Quaternion.identity);
            skill.GetComponent<Rigidbody2D>().velocity = new Vector2(5, 0);
            skills.Add(skill);
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
            StartCoroutine(ActEnermy(skillEnermy1[i], 0.5f));
            GameObject skill = Instantiate(skill1, skillPoint1[i].transform.position, Quaternion.Euler(0, 180, 0));
            skill.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0);
            skills.Add(skill);
        }

        yield return new WaitForSeconds(2f);

        for (int i = 0; i < skillPoint2.Length; i++)
        {
            StartCoroutine(ActEnermy(skillEnermy2[i], 0.5f));
            GameObject skill = Instantiate(skill1, skillPoint2[i].transform.position, Quaternion.identity);
            skill.GetComponent<Rigidbody2D>().velocity = new Vector2(5, 0);
            skills.Add(skill);
        }
    }

    IEnumerator ActEnermy(GameObject enermy, float time)
    {
        SpriteRenderer sprRenderer = enermy.GetComponent<SpriteRenderer>();
        if (sprRenderer != null)
        {
            sprRenderer.enabled = true;
            yield return new WaitForSeconds(time);
            sprRenderer.enabled = false;
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
            hitDamege += playerMovement.skill1Power;
        }
        if (collision.gameObject.CompareTag("Skill4"))
        {
            hitDamege += playerMovement.skill4Power;
        }
    }
}