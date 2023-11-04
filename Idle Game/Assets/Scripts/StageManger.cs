using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StageManger : MonoBehaviour
{
    private MonsterSpwan mSpwan;
    public int stage;

    public int deadMonster = 0;
    private bool stageCleared = false; // 스테이지 클리어 여부

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
        yield return new WaitForSeconds(3f); // 게임 시작 후 3초 대기
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

        if (!stageCleared) // 스테이지가 클리어되지 않았을 때만 실행
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
                stage++; // 스테이지 증가
                deadMonster = 0;
                mSpwan.spwanMonster = 10;

                // 스테이지 클리어 처리
                stageCleared = true;
            }
        }
    }
}
