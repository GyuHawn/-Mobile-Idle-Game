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

    private int spawnIndex = 0;  // ��ȯ ��ġ
    public bool isSpawning = false; // ���� ��ȯ ������ ǥ���ϴ� ����
    public int activeMonsters = 0; // Ȱ��ȭ�� ���� ����

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

        // ���� ���� ���� �� ���
        int spawnCount = Mathf.Min(spwaner.Length, Mathf.Min(spwanMonster, 10));

        for (int i = 0; i < spawnCount; i++)
        {
            GameObject monsterObj;
            if (i == spawnCount - 1 && spawnCount == 10) // ������ ������ ���,  spawnCount�� 10�϶��� ���� ��ȯ
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