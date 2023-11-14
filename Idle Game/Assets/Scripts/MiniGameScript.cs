using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.EventSystems.EventTrigger;

public class MiniGameScript : MonoBehaviour
{
    private MiniGameBoss miniGameBoss;
    private StageManager stageManager;
    private MonsterSpawn monsterSpwan;
    private PlayerMovement playerMovement;

    // 미니게임 입장 횟수
    public int ticket;
    public float plusTicket = 600;
    public TMP_Text ticketText;

    // 보스 오브젝트
    public GameObject boss;
    // 보스 소환 위치
    public GameObject bossPoint;
    // 생성한 보스
    private GameObject bossInstance;

    // 플레이어 시작 위치
    public GameObject playerMovePoint;

    // 미니게임 메뉴
    public GameObject miniGameSelectMenu;
    // 미니게임 종료 메뉴
    public GameObject endMiniGame;
    public GameObject miniGameMenu;

    // 게임 시간
    public float gameTime;

    // 데미지 UI
    public GameObject hitDamageUI;
    public TMP_Text hitDamegeText;
    // 남은 시간 
    public float remainingTime;
    public TMP_Text remainingTimeText;
    public bool miniGameStart;

    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        stageManager = FindObjectOfType<StageManager>();
        monsterSpwan = FindObjectOfType<MonsterSpawn>();

        miniGameStart = false;
    }

    void Update()
    {
        if (miniGameStart)
        {
            if (miniGameBoss == null)
            {
                miniGameBoss = GameObject.Find("MiniGameBoss")?.GetComponent<MiniGameBoss>();
            }
            if (miniGameBoss != null)
            {
                if (remainingTime > 0)
                {
                    remainingTime -= Time.deltaTime;
                }

                if (remainingTime <= 0)
                {
                    endMiniGame.SetActive(true);

                    GameObject player = GameObject.Find("Player");
                    player.transform.position = new Vector2(-28, -51);

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

                    //boss.SetActive(false);
                }
            }
        }

        ticketText.text = "입장권 : " + ticket;

        if (ticket <= 1)
        {
            plusTicket -= Time.deltaTime;
            if (plusTicket <= 0)
            {
                ticket++;
                plusTicket = 600;
            }
        }
    }

    public void OnMiniGame()
    {
        if (ticket > 0)
        {
            if (!miniGameStart)
            {
                bossInstance = Instantiate(boss, bossPoint.transform.position, Quaternion.Euler(0, 180, 0));
                bossInstance.name = "MiniGameBoss";
                GameObject player = GameObject.Find("Player");
                playerMovement.currentHealth = playerMovement.maxHealth;
                player.transform.position = playerMovePoint.transform.position;
                miniGameStart = true;
                miniGameSelectMenu.SetActive(false);

                miniGameBoss = GameObject.Find("MiniGameBoss").GetComponent<MiniGameBoss>();
                remainingTime = 30;

                // 입장권 제거
                ticket--;
            }
        }
    }

    public void EndMiniGame()
    {
        GameObject player = GameObject.Find("Player");

        // 미니게임 종료
        playerMovement.miniGame = false;
        miniGameStart = false;

        // UI 비활성화 
        endMiniGame.SetActive(false);
        miniGameMenu.SetActive(false);

        // 플레이어 이동
        playerMovement.currentHealth = playerMovement.maxHealth;
        player.transform.position = new Vector2(0, 0);
        playerMovement.currentTarget = null;

        // 플레이어 돈 획득
        playerMovement.money += (int)miniGameBoss.hitDamege / 10;

        // 스테이지 재시작
        stageManager.restartStage = true;
        monsterSpwan.RemoveAllMonsters();
        stageManager.EndMiniGameSpawningMonsters();

        // 보스 삭제
        Destroy(bossInstance);
    }
}