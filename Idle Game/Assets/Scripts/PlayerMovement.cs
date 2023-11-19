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
    private BulletScript bulletScript;

    // �⺻ ü�� ��..
    private float baseMaxHealth; // ü��
    private float basePower; // ���ݷ�
    private float baseDefense; // ����

    // ü�� ��..
    public float maxHealth; // ü�� = �⺻ü�� + ����ü��
    public float currentHealth;
    public float power; // ���ݷ� = �⺻���� + ���۰���
    private float defense; // ���� = �⺻��� + ���۹��
    public int money = 10; // ��

    public int level; // ���۷��� (ü + �� + �� ����)
    public float totalPower; // ���� = ���ݷ�(100%) + ���(50%) + ü��(50%)
    public float damege; // ������ = ���ݷ� - ����

    // ���� ����
    public int upgradeHealth = 1;
    public int upgradePower = 1;
    public int upgradeDefense = 1;

    // �̵�
    public float spd;
    public Vector2 inputVec;

    // ����
    public Transform pos;
    public Vector2 boxSize;
    public Collider2D currentTarget;  // ���� Ÿ��

    // ����
    public GameObject bulletPrefab;
    public Transform shoot;
    public float bulletSpeed;
    public GameObject bullet;

    // ���
    private bool isDead;

    // ��ų
    public GameObject skill1;
    public GameObject skill2;
    public GameObject skill3;
    public GameObject skill4;
    public GameObject skill1Obj;
    public GameObject skill4Obj;
    public float skill1Power;
    public float skill4Power;
    public bool useSkill3;


    // ��ų ��Ÿ��
    public GameObject skill1timeUI;
    public TMP_Text skill1TimeText;
    public GameObject skill2timeUI;
    public TMP_Text skill2TimeText;
    public GameObject skill3timeUI;
    public TMP_Text skill3TimeText;
    public GameObject skill4timeUI;
    public TMP_Text skill4TimeText;
    public float skill1Time = 0;
    public float skill2Time = 0;
    public float skill3Time = 0;
    public float skill4Time = 0;

    // ������ ��� ����
    private float prevWeaponPower;
    private float prevArmorDefense;
    private float prevRingHealth ;

    // �̴ϰ��� ����
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

        upgradeHealth = PlayerPrefs.GetInt("upgradeHealth", 1);
        upgradePower = PlayerPrefs.GetInt("upgradePower", 1);
        upgradeDefense = PlayerPrefs.GetInt("upgradeDefense", 1);
        money = PlayerPrefs.GetInt("money", 10);
    }

    private void Start()
    {
        spd = 0.1f;
        bulletSpeed = 20;

        // �⺻ ���� ����
        baseMaxHealth = 100;
        basePower = 15;
        baseDefense = 1;

        // ���� ����
        SetStats();

        power = basePower;
        StartCoroutine(AutoShoot());
        miniGame = false;
        useSkill3 = false;
    }
     
    private void OnApplicationPause(bool pauseStatus) // ������ �����ɶ� ������ ����
    {
        if (pauseStatus)
        {
            PlayerPrefs.SetInt("upgradeHealth", upgradeHealth);
            PlayerPrefs.SetInt("upgradePower", upgradePower);
            PlayerPrefs.SetInt("upgradeDefense", upgradeDefense);
            PlayerPrefs.SetInt("money", money);
        }
    }

    private void OnApplicationQuit() // ���� ����� ������ ����
    {
        PlayerPrefs.SetInt("upgradeHealth", upgradeHealth);
        PlayerPrefs.SetInt("upgradePower", upgradePower);
        PlayerPrefs.SetInt("upgradeDefense", upgradeDefense);
        PlayerPrefs.SetInt("money", money);
    }

    IEnumerator AutoShoot() // �ڵ� ���
    {
        while (true)
        {
            if (IsValidTarget(currentTarget))
            {
                ShootBullet();
            }
            yield return new WaitForSeconds(0.5f); // ���� �ӵ� ����   
        }
    }

    private void Update()
    {
        // Ű���� �� �߻�
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBullet();
        }*/

        // ���� Ÿ���� ������ ���� ���� ���� �ִ��� Ȯ��
        if (!IsValidTarget(currentTarget) || !IsMonsterInDetectionRange())
        {
            // ���� ���� ������ ���ο� ���͸� ã��
            currentTarget = GetRandomMonster();
        }

        // �� ��ų�� ��Ÿ���� �����մϴ�.
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
        if (skill4Time > 0)
        {
            skill4Time -= Time.deltaTime;

            if (skill4Time <= 0)
            {
                skill4timeUI.SetActive(false);
            }
        }

        skill1TimeText.text = Mathf.RoundToInt(skill1Time).ToString();
        skill2TimeText.text = Mathf.RoundToInt(skill2Time).ToString();
        skill3TimeText.text = Mathf.RoundToInt(skill3Time).ToString();
        skill4TimeText.text = Mathf.RoundToInt(skill4Time).ToString();
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
        Die();
    }

    void Move()
    {
        //inputVec.x = Input.GetAxisRaw("Horizontal"); Ű���� �̵�
        //inputVec.y = Input.GetAxisRaw("Vertical");
        inputVec.x = joystick.Horizontal; // ���̽�ƽ �̵�
        inputVec.y = joystick.Vertical;

        Vector2 nextVec = inputVec.normalized * spd;
        rigib.MovePosition(rigib.position + nextVec);
    }

    void Turn()
    {
        float direction;
        // �Է��� ���ų� ���̽�ƽ�� �߾��϶�
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
            // ���̽�ƽ ȸ��
            direction = joystick.Horizontal;
        }

        float rotationY = direction > 0 ? 0f : 180f;
        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
    }

    private Collider2D GetRandomMonster()  // ���� ���� ����
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

    private bool IsValidTarget(Collider2D target) // ���� ����
    {
        // Ÿ���� null �̰ų� Monster �±װ� ������ false ��ȯ
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

    private void ShootBullet() // ���
    {
        if (IsValidTarget(currentTarget))
        {
            bullet = Instantiate(bulletPrefab, shoot.position, Quaternion.identity);
            bullet.name = "Bullet";
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

                // ���� ���� ����
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
        maxHealth = baseMaxHealth + upgradeHealth;  // �ִ� ü�¸� ����
        power = basePower + upgradePower;
        defense = baseDefense + upgradeDefense;
        totalPower = (int)(power + defense + maxHealth);
        skill1Power = power;
        skill4Power = power * 0.1f;
    }


    public void ChangeEquipment(float newWeaponPower, float newArmorDefense, float newRingHealth)
    {
        // ������ ���� ����
        if (newWeaponPower != 0)
        {
            power -= prevWeaponPower; // ���� ���� ���� ����
            prevWeaponPower = newWeaponPower; // �� ���� ���� ����
            power += prevWeaponPower; // �� ���� ���� �߰�
        }

        // ���� ���� ����
        if (newArmorDefense != 0)
        {
            defense -= prevArmorDefense; // ���� �� ���� ����
            prevArmorDefense = newArmorDefense; // �� �� ���� ����
            defense += prevArmorDefense; // �� �� ���� �߰�
        }

        // ������ ���� ����
        if (newRingHealth != 0)
        {
            maxHealth -= prevRingHealth; // ���� ���� ���� ����
            prevRingHealth = newRingHealth; // �� ���� ���� ����
            maxHealth += prevRingHealth; // �� ���� ���� �߰�
        }

        // ��ü ���� ����
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
            if (!skill1.activeSelf) // ��ų�� �̹� Ȱ��ȭ�Ǿ� �ִ��� Ȯ��
            {
                skill1.SetActive(true);
                StartCoroutine(RotateSkillObject());
                skill1Time = 30f;
                skill1timeUI.SetActive(true); // ��Ÿ�� UI Ȱ��ȭ
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

            skill1Obj.transform.rotation = Quaternion.Euler(skill1Obj.transform.rotation.eulerAngles.x, skill1Obj.transform.rotation.eulerAngles.y, zRotation);

            yield return null;

            elapsedTime += Time.deltaTime;
        }

        skill1Obj.transform.rotation = Quaternion.Euler(skill1Obj.transform.rotation.eulerAngles.x, skill1Obj.transform.rotation.eulerAngles.y, 360);

        skill1.SetActive(false);
    }

    public void Skill2()
    {
        if(skill2Time <= 0)
        {       
            skill2.SetActive(true);
            currentHealth = maxHealth;
            skill2Time = 30f;
            skill2timeUI.SetActive(true); // ��Ÿ�� UI Ȱ��ȭ
            StartCoroutine(Skill2Effect());
        }
    }

    IEnumerator Skill2Effect()
    {
        yield return new WaitForSeconds(1f);
        skill2.SetActive(false);
    }

    public void Skill3()
    {
        if (skill3Time <= 0)
        {
            useSkill3 = true;
            skill3.SetActive(true);
            power += 1000;
            defense += 1000;
            spd += 0.1f;
            bulletSpeed += 10;
            skill3timeUI.SetActive(true); // ��Ÿ�� UI Ȱ��ȭ
            skill3Time = 30;

            StartCoroutine(SkillUpgrade());
        }       
    }

    IEnumerator SkillUpgrade()
    {
        yield return new WaitForSeconds(5f);
        skill3.SetActive(false);
        useSkill3 = false;
        power -= 1000;
        defense -= 1000;
        spd -= 0.1f;
        bulletSpeed -= 10;
    }

    public void Skill4()
    {
        if (skill4Time <= 0)
        {
            for (int i = 0; i < 15; i++)
            {
                float randomX = UnityEngine.Random.Range(-8, 8);
                float randomY = UnityEngine.Random.Range(-4, 4);

                Vector3 randomPosition = new Vector3(gameObject.transform.position.x + randomX, gameObject.transform.position.y + randomY, 0);

                Instantiate(skill4Obj, randomPosition, Quaternion.identity);
            }
            skill4timeUI.SetActive(true);
            skill4Time = 10;
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }*/
}
