using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManger : MonoBehaviour
{
    private MonsterSpwan mSpwan;
    public int stage = 1;

    void Start()
    {
        mSpwan = GetComponent<MonsterSpwan>();
        StartCoroutine(StartSpawningMonsters());
    }

    IEnumerator StartSpawningMonsters()
    {
        yield return new WaitForSeconds(3f); // ���� ���� �� 3�� ���
        mSpwan.spwanMonster = 10; // ���� ��ȯ ���� ����
    }

    void Update()
    {
        Debug.Log("stage : " + stage);
        if (mSpwan.spwanMonster == 0 && mSpwan.activeMonsters == 0)
        {
            stage++;
            mSpwan.spwanMonster = 10;
        }
    }
}
