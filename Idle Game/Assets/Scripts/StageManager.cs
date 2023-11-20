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

    public int stage; // ���� ��������
    public int maxStage; // �ִ� ��������
    public TMP_Text maxStageText;

    public int deadMonster = 0;
    private bool stageCleared = false; // �������� Ŭ���� ����

    // �������� ���, �ݺ�
    public GameObject go;
    public GameObject stop;
    public bool isGo;

    public bool restartStage = false; // �������� ����� ����

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

    public void EndMiniGameSpawningMonsters() // �̴ϰ��� ������ �ٽ� ����
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
        maxStageText.text = "�ִ� �������� : " + maxStage;

        if (deadMonster >= 10)
        {
            stageCleared = true;
        }
        else
        {
            stageCleared = false;
        }

        if (!stageCleared) // ���������� Ŭ������� �ʾ��� ���� ����
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
                    stage++; // �������� ����

                    // �ִ� ���� ��������
                    if (stage > maxStage)
                    {
                        maxStage = stage;
                    }
                }

                deadMonster = 0;
                monsterSpwan.spwanMonster = 10;

                // �������� Ŭ���� ó��
                stageCleared = true;
                playerMovement.currentHealth = playerMovement.maxHealth;

                restartStage = true; // �������� ����� ����
                StartSpawningMonsters(); // �������� �����
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