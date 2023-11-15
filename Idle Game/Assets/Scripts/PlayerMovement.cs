using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private FixedJoystick joystick;
    private MiniGameBoss miniGameBoss;
    private MiniGameScript miniGameScript;
    private AudioManager audioManager;

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
    public Vector2 boxSize;
    public Collider2D currentTarget;  // 현재 타겟

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

    // 착용한 장비 스탯
    private float prevWeaponPower;
    private float prevArmorDefense;
    private float prevRingHealth ;

    // 미니게임 입장
    public bool miniGame = false;
    public GameObject miniGameMoney;
    public TMP_Text miniGamePlayerDie;

    private Rigidbody2D rigib;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        miniGameScript = GameObject.Find("Manager").GetComponent<MiniGameScript>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        rigib = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        joystick = FindObjectOfType<FixedJoystick>();

        // 게임이 시작될 때 PlayerPrefs에서 스탯 값을 불러온다
        // 만약 저장된 값이 없다면 기본값을 사용
        upgradeHealth = PlayerPrefs.GetInt("upgradeHealth", 1);
        upgradePower = PlayerPrefs.GetInt("upgradePower", 1);
        upgradeDefense = PlayerPrefs.GetInt("upgradeDefense", 1);
        money = PlayerPrefs.GetInt("money", 10);
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
        miniGame = false;
    }
     
    private void OnApplicationPause(bool pauseStatus) // 어플이 정지될때 데이터 저장
    {
        if (pauseStatus)
        {
            PlayerPrefs.SetInt("upgradeHealth", upgradeHealth);
            PlayerPrefs.SetInt("upgradePower", upgradePower);
            PlayerPrefs.SetInt("upgradeDefense", upgradeDefense);
            PlayerPrefs.SetInt("money", money);
        }
    }

    private void OnApplicationQuit() // 어플 종료시 데이터 저장
    {
        PlayerPrefs.SetInt("upgradeHealth", upgradeHealth);
        PlayerPrefs.SetInt("upgradePower", upgradePower);
        PlayerPrefs.SetInt("upgradeDefense", upgradeDefense);
        PlayerPrefs.SetInt("money", money);
    }

    IEnumerator AutoShoot() // 자동 사격
    {
        while (true)
        {
            if (IsValidTarget(currentTarget))
            {
                ShootBullet();
            }
            yield return new WaitForSeconds(0.5f); // 공격 속도 조절   
        }
    }

    private void Update()
    {
        // 키보드 총 발사
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBullet();
        }*/

        // 현재 타겟이 여전히 감지 범위 내에 있는지 확인
        if (!IsValidTarget(currentTarget) || !IsMonsterInDetectionRange())
        {
            // 감지 범위 내에서 새로운 몬스터를 찾음
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
        float direction;
        // 입력이 없거나 조이스틱이 중앙일때
        if (joystick.Horizontal == 0 && joystick.Vertical == 0)
        {
            if (IsValidTarget(currentTarget))
            {
                direction = currentTarget.transform.position.x - transform.position.x;
            }
            else
            {
                return;
            }
        }
        else
        {
            // 조이스틱 회전
            direction = joystick.Horizontal;
        }

        float rotationY = direction > 0 ? 0f : 180f;
        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
    }

    private Collider2D GetRandomMonster()  // 랜덤 몬스터 추적
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(pos.position, boxSize, 0f);

        List<Collider2D> monsters = new List<Collider2D>();

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Monster") || collider.CompareTag("Boss") || collider.CompareTag("MiniGame"))
            {
                monsters.Add(collider);
            }
        }
            
        return monsters.Count > 0 ? monsters[Random.Range(0, monsters.Count)] : null;
    }

    private bool IsValidTarget(Collider2D target) // 몬스터 감지
    {
        // 타겟이 null 이거나 Monster 태그가 없으면 false 반환
        return target != null && (target.CompareTag("Monster") || target.CompareTag("Boss") || target.CompareTag("MiniGame"));
    }

    private bool IsMonsterInDetectionRange()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(pos.position, boxSize, 0f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Monster") || collider.CompareTag("Boss") || collider.CompareTag("MiniGame"))
            {
                return true;
            }
        }

        return false;
    }

    private void ShootBullet() // 사격
    {
        if (IsValidTarget(currentTarget))
        {
            GameObject bullet = Instantiate(bulletPrefab, shoot.position, Quaternion.identity);
            bullet.GetComponent<BulletScript>().target = currentTarget.gameObject;
            bullet.GetComponent<Rigidbody2D>().velocity = (currentTarget.transform.position - shoot.position).normalized * bulletSpeed;
            audioManager.PlayAttackSound();
        }
    }

    void Die()
    {
        if ((!isDead && currentHealth <= 0) && !miniGame)
        {
            audioManager.PlayDieSound();
            miniGameScript.miniGameStart = false;
            isDead = true;
            transform.position = new Vector2(0, 0);
            MonsterSpawn spawner = GameObject.Find("Manager").GetComponent<MonsterSpawn>();
            StageManager stageManger = GameObject.Find("Manager").GetComponent<StageManager>();

            if (spawner != null)
            {
                spawner.RemoveAllMonsters();
            }

            stageManger.deadMonster = 0;
            currentHealth = maxHealth;
            isDead = false;
        }
        if ((!isDead && currentHealth <= 0) && miniGame)
        {
            if (miniGameBoss != null)
            {
                audioManager.PlayDieSound();
                miniGameMoney.SetActive(true);
                isDead = true;

                miniGameBoss = GameObject.Find("MiniGameBoss").GetComponent<MiniGameBoss>();
                miniGameScript.hitDamageUI.SetActive(false);

                // 보스 관련 삭제
                GameObject miniBoss = GameObject.Find("MiniGameBoss");

                foreach (GameObject skill in miniGameBoss.skills)
                {
                    Destroy(skill);
                }
                miniGameBoss.skills.Clear();

                foreach (var skillEnermy in miniGameBoss.skillEnermy1)
                {
                    SpriteRenderer sprRenderer = skillEnermy.GetComponent<SpriteRenderer>();
                    if (sprRenderer != null)
                    {
                        sprRenderer.enabled = false;
                    }
                }
                foreach (var skillEnermy in miniGameBoss.skillEnermy2)
                {
                    SpriteRenderer sprRenderer = skillEnermy.GetComponent<SpriteRenderer>();
                    if (sprRenderer != null)
                    {
                        sprRenderer.enabled = false;
                    }
                }
                Destroy(miniBoss);

                miniGame = false;
                miniGameScript.miniGameStart = false;

                miniGamePlayerDie.text = "+" + ((int)miniGameBoss.hitDamege / 10).ToString();
                money += (int)miniGameBoss.hitDamege / 10;

                transform.position = new Vector2(0, 0);
                isDead = false;

                StartCoroutine(MiniGameDieMoney());
            }
        }
    }

    IEnumerator MiniGameDieMoney()
    {
        yield return new WaitForSeconds(1f);
        miniGameMoney.SetActive(false);
    }

    public void SetStats()
    {
        level = (upgradeHealth + upgradePower + upgradeDefense) - 2;
        maxHealth = baseMaxHealth + upgradeHealth;  // 최대 체력만 증가
        power = basePower + upgradePower;
        defense = baseDefense + upgradeDefense;
        totalPower = (int)(power + defense + maxHealth);
        skillPower = power;
    }


    public void ChangeEquipment(float newWeaponPower, float newArmorDefense, float newRingHealth)
    {
        // 무기의 스탯 변경
        if (newWeaponPower != 0)
        {
            power -= prevWeaponPower; // 이전 무기 스탯 제거
            prevWeaponPower = newWeaponPower; // 새 무기 스탯 저장
            power += prevWeaponPower; // 새 무기 스탯 추가
        }

        // 방어구의 스탯 변경
        if (newArmorDefense != 0)
        {
            defense -= prevArmorDefense; // 이전 방어구 스탯 제거
            prevArmorDefense = newArmorDefense; // 새 방어구 스탯 저장
            defense += prevArmorDefense; // 새 방어구 스탯 추가
        }

        // 반지의 스탯 변경
        if (newRingHealth != 0)
        {
            maxHealth -= prevRingHealth; // 이전 반지 스탯 제거
            prevRingHealth = newRingHealth; // 새 반지 스탯 저장
            maxHealth += prevRingHealth; // 새 반지 스탯 추가
        }

        // 전체 투력 적용
        totalPower = (int)(power + defense + maxHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Monster") || collision.gameObject.CompareTag("Boss") || collision.gameObject.CompareTag("MiniGame"))
        {
            MonsterScript monsterScript = collision.gameObject.GetComponent<MonsterScript>();

            if (monsterScript != null)
            {
                damege = monsterScript.power - defense;
                currentHealth -= damege;
                audioManager.PlayHitSound();
                StartCoroutine(FlashWhite());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (miniGame)
        {
            if (miniGameBoss == null)
            {
                miniGameBoss = GameObject.Find("MiniGameBoss")?.GetComponent<MiniGameBoss>();
            }
            if (miniGameBoss != null)
            {
                if (collision.gameObject.CompareTag("BossSkill"))
                {
                    currentHealth -= miniGameBoss.damage;
                    audioManager.PlayHitSound();
                    StartCoroutine(FlashWhite());
                }
            }

        }
    }

    IEnumerator FlashWhite()
    {
        if (gameObject.CompareTag("Player"))
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
        }
    }

    public void Mini()
    {
        miniGame = true;
        currentTarget = null;
        StartCoroutine(IncreaseDamageOverTime());
    }

    IEnumerator IncreaseDamageOverTime()
    {
        while (miniGame)
        {
            yield return new WaitForSeconds(10f);
            miniGameBoss.damage += 5;
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
        Gizmos.DrawWireCube(pos.position, boxSize);
    }*/
}
