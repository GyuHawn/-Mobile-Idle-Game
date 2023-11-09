using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameScript : MonoBehaviour
{
    private MiniGameBoss miniGameBoss;
    private StageManger stageManager;
    private MonsterSpwan monsterSpwan;

    // 미니게임 메뉴
    public GameObject miniGameMenu;

    // 미니게임 맵
    public GameObject miniGame;

    // 플레이어 시작 위치
    public GameObject playerMovePoint;

    // 게임 시간
    public float gameTime;
    private float timer;

    public bool gameStarted;

    void Start()
    {
        gameStarted = false;

        stageManager = FindObjectOfType<StageManger>();
        monsterSpwan = FindObjectOfType<MonsterSpwan>();
        
    }

    void Update()
    {
        if (gameStarted)
        {
            miniGameBoss = GameObject.Find("Penguin").GetComponent<MiniGameBoss>();
            miniGameBoss.remainingTime -= Time.deltaTime;
            if (miniGameBoss.remainingTime <= 0)
            {
                GameObject player = GameObject.Find("Player");
                player.transform.position = new Vector2(0, 0);
                miniGame.SetActive(false);
                gameStarted = false;

                // 스테이지 재시작 부분
                stageManager.restartStage = true;
                monsterSpwan.RemoveAllMonsters();
                stageManager.StartCoroutine(stageManager.StartSpawningMonsters());
            }
        }
    }

    public void OnMiniGame()
    {
        miniGame.SetActive(true);
        GameObject player = GameObject.Find("Player");
        player.transform.position = playerMovePoint.transform.position;
        gameStarted = true;
        miniGameMenu.SetActive(false);

        miniGameBoss = GameObject.Find("Penguin").GetComponent<MiniGameBoss>();
        miniGameBoss.remainingTime = 60;
    }
}
