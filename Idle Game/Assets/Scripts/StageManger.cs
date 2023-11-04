using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StageManger : MonoBehaviour
{
    private MonsterSpwan mSpwan;
    public int stage;

    public int deadMonster = 0;
    private bool stageCleared = false; // �������� Ŭ���� ����

    void Awake()
    {
        stage = PlayerPrefs.GetInt("stage", 1);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            PlayerPrefs.SetInt("stage", stage);
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
        if(deadMonster >= 10)
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
            if(mSpwan.spwanMonster == 0 && mSpwan.activeMonsters == 0)
            {
                stage++; // �������� ����
                deadMonster = 0;
                mSpwan.spwanMonster = 10;

                // �������� Ŭ���� ó��
                stageCleared = true;
            }
        }
    }
}
