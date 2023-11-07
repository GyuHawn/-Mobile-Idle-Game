using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameBoss : MonoBehaviour
{
    public float damage = 10;

    // ��ų ������
    public GameObject skill1;

    // ��ų �߻�
    public GameObject[] skillPoint1;
    public GameObject[] skillEnermy1;
    public GameObject[] skillPoint2;
    public GameObject[] skillEnermy2;

    private int currentSkillIndex1 = 0;
    private int currentSkillIndex2 = 0;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();

        StartCoroutine(SelectPattern());
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
}