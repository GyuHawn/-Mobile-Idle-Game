using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private MonsterSpawn monsterSpwan;
    private AudioManager audioManager;

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
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        monsterSpwan = GetComponent<MonsterSpawn>();

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
        StartSpawningMonsters();
    }

    public void StartSpawningMonsters()
    {
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
        maxStageText.text = "최대 스테이지 : " + maxStage;

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
                audioManager.PlayStageClearSound();
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
                StartSpawningMonsters(); // 스테이지 재시작
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