using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiniGameScript : MonoBehaviour
{
    private MiniGameBoss miniGameBoss;
    private StageManger stageManager;
    private MonsterSpwan monsterSpwan;

    // �̴ϰ��� ���� Ƚ��
    public int ticket;
    public float plusTicket = 60;
    public TMP_Text ticketText;

    // ���� ������Ʈ
    public GameObject boss;

    // �̴ϰ��� �޴�
    public GameObject miniGameSelectMenu;
    // �̴ϰ��� ���� �޴�
    public GameObject endMiniGame;
    public GameObject miniGameMenu;

    // �̴ϰ��� ��
    public GameObject miniGame;

    // �÷��̾� ���� ��ġ
    public GameObject playerMovePoint;

    // ���� �ð�
    public float gameTime;

    public bool miniGameStart;

    void Start()
    {
        miniGameStart = false;

        stageManager = FindObjectOfType<StageManger>();
        monsterSpwan = FindObjectOfType<MonsterSpwan>();

    }

    void Update()
    {
        if (miniGameStart)
        {
            if (miniGameBoss == null)
            {
                miniGameBoss = GameObject.Find("Penguin")?.GetComponent<MiniGameBoss>();
            }
            if (miniGameBoss != null)
            {
                miniGameBoss.remainingTime -= Time.deltaTime;
                if (miniGameBoss.remainingTime <= 0)
                {
                    endMiniGame.SetActive(true);
                    boss.SetActive(false);
                }
            }
        }

        ticketText.text = "����� : " + ticket;

        if (ticket <= 1)
        {
            plusTicket -= Time.deltaTime;
            if(plusTicket <= 0)
            {
                ticket++;
                plusTicket = 60;
            }
        }
    }

    public void OnMiniGame()
    {
        if (ticket > 0)
        {
            miniGame.SetActive(true);
            GameObject player = GameObject.Find("Player");
            player.transform.position = playerMovePoint.transform.position;
            miniGameStart = true;
            miniGameSelectMenu.SetActive(false);

            miniGameBoss = GameObject.Find("Penguin").GetComponent<MiniGameBoss>();
            miniGameBoss.remainingTime = 60;

            // ����� ����
            ticket--;
        }
    }

    public void EndMiniGame()
    {
        GameObject player = GameObject.Find("Player");
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        // �̴ϰ��� ����
        playerMovement.miniGame = false;
        miniGameStart = false;

        // UI ��Ȱ��ȭ
        miniGame.SetActive(false);
        endMiniGame.SetActive(false);
        miniGameMenu.SetActive(false);

        // �÷��̾� �̵�
        player.transform.position = new Vector2(0, 0);
        playerMovement.currentTarget = null;

        // �������� �����
        stageManager.restartStage = true;
        monsterSpwan.RemoveAllMonsters();
        stageManager.EndMiniGameSpawningMonsters();

        // �÷��̾� �� ȹ��
        playerMovement.money += (int)miniGameBoss.hitDamege / 10;

    }
}
