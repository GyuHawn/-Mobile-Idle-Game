using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageManger : MonoBehaviour
{
    private MonsterSpwan mSpwan;
    public int stage; // 현재 스테이지
    public int maxStage; // 최대 스테이지
    public TMP_Text maxStageText;

    public int deadMonster = 0;
    private bool stageCleared = false; // 스테이지 클리어 여부

    // 스테이지 등반, 반복
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
        yield return new WaitForSeconds(3f); // 게임 시작 후 3초 대기
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
                    stage++; // 스테이지 증가

                    // 최대 도달 스테이지
                    if (stage > maxStage)
                    {
                        maxStage = stage;
                    }
                }
                deadMonster = 0;
                mSpwan.spwanMonster = 10;

                // 스테이지 클리어 처리
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
