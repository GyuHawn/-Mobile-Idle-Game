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
        yield return new WaitForSeconds(3f); // ���� ���� �� 3�� ���

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
        maxStageText.text = "Max Stage : " + maxStage;

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
                StartCoroutine(StartSpawningMonsters()); // �������� �����
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
