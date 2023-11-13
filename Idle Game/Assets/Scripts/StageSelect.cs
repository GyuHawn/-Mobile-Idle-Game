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
            // �ִ� ������������ ���� ���������� �̵�������.
            if (stageNumber > stageManager.maxStage)
            {
                return;
            }

            // �������� ���� ���� ��� ���͸� ����
            monsterSpwan.RemoveAllMonsters();

            stageManager.stage = stageNumber;
            stageManager.deadMonster = 0; // ���� ���� �� �ʱ�ȭ
            monsterSpwan.spwanMonster = 10; // �����ؾ� �� ���� �� �ʱ�ȭ

            monsterSpwan.RemoveAllMonsters();

            selectStage.SetActive(false);
        }
    }



}
