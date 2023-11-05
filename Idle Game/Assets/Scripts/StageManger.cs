using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageManger : MonoBehaviour
{
    private MonsterSpwan mSpwan;
    public int stage; // ���� ��������
    public int maxStage; // �ִ� ��������
    public TMP_Text maxStageText;

    public int deadMonster = 0;
    private bool stageCleared = false; // �������� Ŭ���� ����

    // �������� ���, �ݺ�
    public GameObject go;
    public GameObject stop;
    public bool isGo; 

    void Awake()
    {
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
        mSpwan = GetComponent<MonsterSpwan>();
        StartCoroutine(StartSpawningMonsters());
    }

    IEnumerator StartSpawningMonsters()
    {
        yield return new WaitForSeconds(3f); // ���� ���� �� 3�� ���
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
            if (mSpwan.spwanMonster == 0 && mSpwan.activeMonsters == 0)
            {
                mSpwan.spwanMonster = 10;
            }
        }
        if (stageCleared)
        {
            if (mSpwan.spwanMonster == 0 && mSpwan.activeMonsters == 0)
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
                mSpwan.spwanMonster = 10;

                // �������� Ŭ���� ó��
                stageCleared = true;
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
