using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameScript : MonoBehaviour
{
    private MiniGameBoss miniGameBoss;
    private StageManger stageManager;
    private MonsterSpwan monsterSpwan;

    // �̴ϰ��� �޴�
    public GameObject miniGameMenu;

    // �̴ϰ��� ��
    public GameObject miniGame;

    // �÷��̾� ���� ��ġ
    public GameObject playerMovePoint;

    // ���� �ð�
    public float gameTime;
    private float timer;

    public bool gameStarted;

    void Start()
    {
        gameStarted = false;

        stageManager = FindObjectOfType<StageManger>();
        monsterSpwan = FindObjectOfType<MonsterSpwan>();
        
    }

    void Update()
    {
        if (gameStarted)
        {
            miniGameBoss = GameObject.Find("Penguin").GetComponent<MiniGameBoss>();
            miniGameBoss.remainingTime -= Time.deltaTime;
            if (miniGameBoss.remainingTime <= 0)
            {
                GameObject player = GameObject.Find("Player");
                player.transform.position = new Vector2(0, 0);
                miniGame.SetActive(false);
                gameStarted = false;

                // �������� ����� �κ�
                stageManager.restartStage = true;
                monsterSpwan.RemoveAllMonsters();
                stageManager.StartCoroutine(stageManager.StartSpawningMonsters());
            }
        }
    }

    public void OnMiniGame()
    {
        miniGame.SetActive(true);
        GameObject player = GameObject.Find("Player");
        player.transform.position = playerMovePoint.transform.position;
        gameStarted = true;
        miniGameMenu.SetActive(false);

        miniGameBoss = GameObject.Find("Penguin").GetComponent<MiniGameBoss>();
        miniGameBoss.remainingTime = 60;
    }
}
