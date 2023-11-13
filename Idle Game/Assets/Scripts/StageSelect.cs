using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageSelect : MonoBehaviour
{
    private StageManger stageManager;
    private MonsterSpwan monsterSpwan;

    public GameObject selectStage;

    public TMP_InputField stageInput;

    void Start()
    {
        stageManager = GameObject.Find("Manager").GetComponent<StageManger>();
        monsterSpwan = GameObject.Find("Manager").GetComponent<MonsterSpwan>();
    }

    public void OnSelectStage()
    {
        selectStage.SetActive(!selectStage.activeSelf);
    }

    public void StageChange()
    {
        ChangeStage(stageInput.text);
    }

    private void ChangeStage(string stageNumberString)
    {
        int stageNumber;

        if (int.TryParse(stageNumberString, out stageNumber))
        {
            // 최대 스테이지보다 높은 스테이지로 이동없도록.
            if (stageNumber > stageManager.maxStage)
            {
                return;
            }

            // 스테이지 변경 전에 모든 몬스터를 제거
            monsterSpwan.RemoveAllMonsters();

            stageManager.stage = stageNumber;
            stageManager.deadMonster = 0; // 죽은 몬스터 수 초기화
            monsterSpwan.spwanMonster = 10; // 생성해야 할 몬스터 수 초기화

            monsterSpwan.RemoveAllMonsters();

            selectStage.SetActive(false);
        }
    }



}
