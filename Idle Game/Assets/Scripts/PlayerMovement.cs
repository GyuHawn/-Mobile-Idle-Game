using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private FixedJoystick joystick;

    // 기본 체력 등..
    private float baseMaxHealth; // 체력
    private float basePower; // 공격력
    private float baseDefense; // 방어력

    // 체력 등..
    public float maxHealth; // 체력 = 기본체력 + 업글체력
    public float currentHealth;
    public float power; // 공격력 = 기본공격 + 업글공격
    private float defense; // 방어력 = 기본방어 + 업글방어
    public int money = 10; // 돈

    public int level; // 업글레벨 (체 + 공 + 방 업글)
    public float totalPower; // 투력 = 공격력(100%) + 방어(50%) + 체력(50%)
    public float damege; // 데미지 = 공격력 - 방어력

    // 업글 레벨
    public int upgradeHealth = 1;
    public int upgradePower = 1;
    public int upgradeDefense = 1;

    // 이동
    public float spd;
    public Vector2 inputVec;

    // 감지
    public Transform pos;
    public Vector2 BoxSize;
    private Collider2D currentTarget;  // 현재 타겟
    public GameObject gun;

    // 공격
    public GameObject bulletPrefab;
    public Transform shoot;
    public float bulletSpeed;

    // 사망
    private bool isDead;

    // 스킬
    public GameObject skill;
    public GameObject skillObj;
    public float skillPower;

    // 스킬 쿨타임
    public GameObject skill1timeUI;
    public TMP_Text skill1TimeText;
    public GameObject skill2timeUI;
    public TMP_Text skill2TimeText;
    public GameObject skill3timeUI;
    public TMP_Text skill3TimeText;
    public float skill1Time = 0;
    public float skill2Time = 0;
    public float skill3Time = 0;

    private Rigidbody2D rigib;

    private void Awake()
    {
        rigib = GetComponent<Rigidbody2D>();
        joystick = FindObjectOfType<FixedJoystick>();
    }

    private void Start()
    {
        spd = 0.1f;
        bulletSpeed = 20;

        // 기본 스탯 설정
        baseMaxHealth = 100;
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
        // 키보드 총 발사
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBullet();
        }*/

        // 타겟이 없을때 새로운 적 타겟
        if (!IsValidTarget(currentTarget))
        {
            currentTarget = GetRandomMonster();
        }

        // 각 스킬의 쿨타임을 관리합니다.
        if (skill1Time > 0) 
        {
            skill1Time -= Time.deltaTime;  
            
            if(skill1Time <= 0)
            {
                skill1timeUI.SetActive(false);
            }
        }
        if (skill2Time > 0)
        {
            skill2Time -= Time.deltaTime;

            if (skill2Time <= 0)
            {
                skill2timeUI.SetActive(false);
            }
        }
        if (skill3Time > 0)
        {
            skill3Time -= Time.deltaTime;

            if (skill3Time <= 0)
            {
                skill3timeUI.SetActive(false);
            }
        }

        skill1TimeText.text = Mathf.RoundToInt(skill1Time).ToString();
        skill2TimeText.text = Mathf.RoundToInt(skill2Time).ToString();
        skill3TimeText.text = Mathf.RoundToInt(skill3Time).ToString();
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
        Die();
    }

    void Move()
    {
        //inputVec.x = Input.GetAxisRaw("Horizontal"); 키보드 이동
        //inputVec.y = Input.GetAxisRaw("Vertical");
        inputVec.x = joystick.Horizontal; // 조이스틱 이동
        inputVec.y = joystick.Vertical;

        Vector2 nextVec = inputVec.normalized * spd;
        rigib.MovePosition(rigib.position + nextVec);
    }

    void Turn()
    {
        if (IsValidTarget(currentTarget))
        {
            float direction = currentTarget.transform.position.x - transform.position.x;
            float rotationY = direction > 0 ? 0f : 180f; // 오른쪽 또는 왼쪽을 바라봅니다.
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
            bullet.GetComponent<BulletScript>().target = currentTarget.gameObject;
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
        level = upgradeHealth + upgradePower + upgradeDefense;
        maxHealth = baseMaxHealth + upgradeHealth;
        currentHealth = maxHealth;
        power = basePower + upgradePower;
        defense = baseDefense + upgradeDefense;
        totalPower = (int)(power + (defense / 0.5f) + (maxHealth / 0.3f));
        skillPower = power;
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

    public void Skill1()
    {
        if (skill1Time <= 0)
        {
            if (!skill.activeSelf) // 스킬이 이미 활성화되어 있는지 확인
            {
                skill.SetActive(true);
                StartCoroutine(RotateSkillObject());
                skill1Time = 60f;
                skill1timeUI.SetActive(true); // 쿨타임 UI 활성화
            }
        }
    }
    IEnumerator RotateSkillObject()
    {
        float elapsedTime = 0f;
        float rotateTime = 1.5f;

        while (elapsedTime < rotateTime)
        {
            float zRotation = Mathf.Lerp(0, 360, elapsedTime / rotateTime);

            skillObj.transform.rotation = Quaternion.Euler(skillObj.transform.rotation.eulerAngles.x, skillObj.transform.rotation.eulerAngles.y, zRotation);

            yield return null;

            elapsedTime += Time.deltaTime;
        }

        skillObj.transform.rotation = Quaternion.Euler(skillObj.transform.rotation.eulerAngles.x, skillObj.transform.rotation.eulerAngles.y, 360);

        skill.SetActive(false);
    }

    public void Skill2()
    {
        if(skill2Time <= 0)
        {         
            currentHealth = maxHealth;
            skill2Time = 120f;
            skill2timeUI.SetActive(true); // 쿨타임 UI 활성화
        }
    }
    public void Skill3()
    {
        if(skill3Time <= 0)
        {
            power += 1000;
            defense += 1000;
            spd += 0.1f;
            bulletSpeed += 10;
            skill3timeUI.SetActive(true); // 쿨타임 UI 활성화
            skill3Time = 120;

            StartCoroutine(SkillUpgrade());
        }       
    }

    IEnumerator SkillUpgrade()
    {
        yield return new WaitForSeconds(5f);
        power -= 1000;
        defense -= 1000;
        spd -= 0.1f;
        bulletSpeed -= 10;
    }


    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, BoxSize);
    }*/
}
