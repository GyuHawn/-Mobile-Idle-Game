using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    private StageManager smanager;

    public GameObject[] spwaner;
    public GameObject[] monsters;
    public GameObject[] boss;

    public int spwanMonster;

    private int spawnIndex = 0;  // 소환 위치
    public bool isSpawning = false; // 현재 소환 중인지 표시하는 변수
    public int activeMonsters = 0; // 활성화된 몬스터 개수

    void Start()
    {
        smanager = GetComponent<StageManager>();
        spwanMonster = 0;
    }

    void Update()
    {
        if (spwanMonster > 0 && !isSpawning)
        {
            spawnIndex = 0;
            StartCoroutine(SpawnMonsters());
        }
    }

    IEnumerator SpawnMonsters()
    {
        isSpawning = true;

        // 보스 포함 몬스터 수 계산
        int spawnCount = Mathf.Min(spwaner.Length, Mathf.Min(spwanMonster, 10));

        for (int i = 0; i < spawnCount; i++)
        {
            GameObject monsterObj;
            if (i == spawnCount - 1 && spawnCount == 10) // 마지막 몬스터인 경우,  spawnCount가 10일때만 보스 소환
            {
                monsterObj = Instantiate(boss[smanager.stage % boss.Length], spwaner[spawnIndex].transform.position, Quaternion.identity);
                monsterObj.transform.localScale = new Vector3(3, 3, 3);
            }
            else
            {
                monsterObj = Instantiate(monsters[smanager.stage % monsters.Length], spwaner[spawnIndex].transform.position, Quaternion.identity);            
            }

            MonsterScript monsterScript = monsterObj.GetComponent<MonsterScript>();
            monsterScript.spawner = this;

            activeMonsters++;
            spwanMonster--;

            spawnIndex = (spawnIndex + 1) % spwaner.Length;

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