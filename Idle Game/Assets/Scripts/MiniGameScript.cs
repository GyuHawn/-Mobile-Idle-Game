using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniGameScript : MonoBehaviour
{
    private MiniGameBoss miniGameBoss;
    private StageManger stageManager;
    private MonsterSpwan monsterSpwan;

    // 미니게임 입장 횟수
    public int ticket;
    public float plusTicket = 60;
    public TMP_Text ticketText;

    // 보스 오브젝트
    public GameObject boss;

    // 미니게임 메뉴
    public GameObject miniGameSelectMenu;
    // 미니게임 종료 메뉴
    public GameObject endMiniGame;
    public GameObject miniGameMenu;

    // 미니게임 맵
    public GameObject miniGame;

    // 플레이어 시작 위치
    public GameObject playerMovePoint;

    // 게임 시간
    public float gameTime;

    public bool miniGameStart;

    void Start()
    {
        miniGameStart = false;

        stageManager = FindObjectOfType<StageManger>();
        monsterSpwan = FindObjectOfType<MonsterSpwan>();

    }

    void Update()
    {
        if (miniGameStart)
        {
            if (miniGameBoss == null)
            {
                miniGameBoss = GameObject.Find("Penguin")?.GetComponent<MiniGameBoss>();
            }
            if (miniGameBoss != null)
            {
                miniGameBoss.remainingTime -= Time.deltaTime;
                if (miniGameBoss.remainingTime <= 0)
                {
                    endMiniGame.SetActive(true);
                    boss.SetActive(false);
                }
            }
        }

        ticketText.text = "입장권 : " + ticket;

        if (ticket <= 1)
        {
            plusTicket -= Time.deltaTime;
            if(plusTicket <= 0)
            {
                ticket++;
                plusTicket = 60;
            }
        }
    }

    public void OnMiniGame()
    {
        if (ticket > 0)
        {
            miniGame.SetActive(true);
            GameObject player = GameObject.Find("Player");
            player.transform.position = playerMovePoint.transform.position;
            miniGameStart = true;
            miniGameSelectMenu.SetActive(false);

            miniGameBoss = GameObject.Find("Penguin").GetComponent<MiniGameBoss>();
            miniGameBoss.remainingTime = 60;

            // 입장권 제거
            ticket--;
        }
    }

    public void EndMiniGame()
    {
        GameObject player = GameObject.Find("Player");
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        // 미니게임 종료
        playerMovement.miniGame = false;
        miniGameStart = false;

        // UI 비활성화
        miniGame.SetActive(false);
        endMiniGame.SetActive(false);
        miniGameMenu.SetActive(false);

        // 플레이어 이동
        player.transform.position = new Vector2(0, 0);
        playerMovement.currentTarget = null;

        // 스테이지 재시작
        stageManager.restartStage = true;
        monsterSpwan.RemoveAllMonsters();
        stageManager.EndMiniGameSpawningMonsters();

        // 플레이어 돈 획득
        playerMovement.money += (int)miniGameBoss.hitDamege / 10;

    }
}
