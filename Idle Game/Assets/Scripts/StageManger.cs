using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageManger : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private MonsterSpwan monsterSpwan;

    public int stage; // 현재 스테이지
    public int maxStage; // 최대 스테이지
    public TMP_Text maxStageText;

    public int deadMonster = 0;
    private bool stageCleared = false; // 스테이지 클리어 여부

    // 스테이지 등반, 반복
    public GameObject go;
    public GameObject stop;
    public bool isGo;

    public bool restartStage = false; // 스테이지 재시작 여부

    void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        monsterSpwan = GetComponent<MonsterSpwan>();

        stage = PlayerPrefs.GetInt("stage", 1);
        maxStage = PlayerPrefs.GetInt("maxStage", 1);

        isGo = true;
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            PlayerPrefs.SetInt("stage", stage);
            PlayerPrefs.SetInt("maxStage", maxStage);
        }
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("stage", stage);
    }


    void Start()
    {      
        StartCoroutine(StartSpawningMonsters());
    }

    public IEnumerator StartSpawningMonsters()
    {
        yield return new WaitForSeconds(3f); // 게임 시작 후 3초 대기

        if (restartStage)
        {
            deadMonster = 0;
            monsterSpwan.spwanMonster = 10;
            restartStage = false;
        }
    }

    public void EndMiniGameSpawningMonsters() // 미니게임 종료후 다시 설정
    {
        if (restartStage)
        {
            deadMonster = 0;
            monsterSpwan.spwanMonster = 10;
            restartStage = false;
        }
    }

    void Update()
    {
        maxStageText.text = "Max Stage : " + maxStage;

        if (deadMonster >= 10)
        {
            stageCleared = true;
        }
        else
        {
            stageCleared = false;
        }

        if (!stageCleared) // 스테이지가 클리어되지 않았을 때만 실행
        {
            if (monsterSpwan.spwanMonster == 0 && monsterSpwan.activeMonsters == 0)
            {
                monsterSpwan.spwanMonster = 10;
            }
        }
        if (stageCleared)
        {
            if (monsterSpwan.spwanMonster == 0 && monsterSpwan.activeMonsters == 0)
            {
                if (isGo)
                {
                    stage++; // 스테이지 증가

                    // 최대 도달 스테이지
                    if (stage > maxStage)
                    {
                        maxStage = stage;
                    }
                }

                deadMonster = 0;
                monsterSpwan.spwanMonster = 10;

                // 스테이지 클리어 처리
                stageCleared = true;
                playerMovement.currentHealth = playerMovement.maxHealth;

                restartStage = true; // 스테이지 재시작 설정
                StartCoroutine(StartSpawningMonsters()); // 스테이지 재시작
            }
        }
    }

    public void StageStop()
    {
        go.SetActive(false);
        stop.SetActive(true);
        isGo = false;
    }
    public void StageGo()
    {
        go.SetActive(true);
        stop.SetActive(false);
        isGo = true;
    }
}
