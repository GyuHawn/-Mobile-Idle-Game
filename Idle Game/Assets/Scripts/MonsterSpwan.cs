using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterSpwan : MonoBehaviour
{
    private StageManger smanager;

    public GameObject[] spwaner;
    public GameObject[] monsters;

    public int spwanMonster;

    public bool isSpawning = false; // ���� ��ȯ ������ ǥ���ϴ� ����
    public int activeMonsters = 0; // Ȱ��ȭ�� ���� ����

    void Start()
    {
        smanager = GetComponent<StageManger>();
        spwanMonster = 0;
        StartCoroutine(SpawnMonsters());
    }

    void Update()
    {
        if (spwanMonster > 0 && !isSpawning)
        {
            StartCoroutine(SpawnMonsters());
        }
    }

    IEnumerator SpawnMonsters()
    {
        isSpawning = true;

        int spawnCount = Mathf.Min(spwaner.Length, spwanMonster);
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject monsterObj = Instantiate(monsters[smanager.stage % monsters.Length], spwaner[i].transform.position, Quaternion.identity);
            MonsterScript monsterScript = monsterObj.GetComponent<MonsterScript>();
            monsterScript.spawner = this;

            // Immediately set stats after instantiation
            monsterScript.SetStats(smanager.stage);

            activeMonsters++;
            spwanMonster--;

            yield return new WaitForSeconds(0.5f);
        }

        isSpawning = false;
    }
}