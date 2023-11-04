using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterSpwan : MonoBehaviour
{
    private StageManger smanager;

    public GameObject[] spwaner;
    public GameObject[] monsters;
    public GameObject[] boss;

    public int spwanMonster;

    public bool isSpawning = false; // 현재 소환 중인지 표시하는 변수
    public int activeMonsters = 0; // 활성화된 몬스터 개수

    void Start()
    {
        smanager = GetComponent<StageManger>();
        spwanMonster = 0;
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
            GameObject monsterObj;
            if (i == spawnCount - 1) // 마지막 몬스터인 경우
            {
                monsterObj = Instantiate(boss[smanager.stage % boss.Length], spwaner[i].transform.position, Quaternion.identity);
                monsterObj.transform.localScale = new Vector3(3, 3, 3);
            }
            else
            {
                monsterObj = Instantiate(monsters[smanager.stage % monsters.Length], spwaner[i].transform.position, Quaternion.identity);
            }

            MonsterScript monsterScript = monsterObj.GetComponent<MonsterScript>();
            monsterScript.spawner = this;

            activeMonsters++;
            spwanMonster--;

            yield return new WaitForSeconds(0.5f);
        }

        isSpawning = false;
    }


    public void RemoveAllMonsters()
    {
        foreach (GameObject monster in GameObject.FindGameObjectsWithTag("Monster"))
        {
            Destroy(monster);
        }

        foreach (GameObject boss in GameObject.FindGameObjectsWithTag("Boss"))
        {
            Destroy(boss);
        }
    }
}