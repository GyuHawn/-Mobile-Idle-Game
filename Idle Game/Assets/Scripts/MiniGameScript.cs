using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.EventSystems.EventTrigger;

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
    // ���� ��ȯ ��ġ
    public GameObject bossPoint;
    // ������ ����
    private GameObject bossInstance;

    // �÷��̾� ���� ��ġ
    public GameObject playerMovePoint;

    // �̴ϰ��� �޴�
    public GameObject miniGameSelectMenu;
    // �̴ϰ��� ���� �޴�
    public GameObject endMiniGame;
    public GameObject miniGameMenu;

    // ���� �ð�
    public float gameTime;

    // ������ UI
    public GameObject hitDamageUI;
    public TMP_Text hitDamegeText;
    // ���� �ð� 
    public float remainingTime;
    public TMP_Text remainingTimeText;
    public bool miniGameStart;

    void Start()
    {
        ticket = 2;
        miniGameStart = false;
        remainingTime = 30;

        stageManager = FindObjectOfType<StageManger>();
        monsterSpwan = FindObjectOfType<MonsterSpwan>();

    }

    void Update()
    {
        if (miniGameStart)
        {
            if (miniGameBoss == null)
            {
                miniGameBoss = GameObject.Find("MiniGameBoss")?.GetComponent<MiniGameBoss>();
            }
            if (miniGameBoss != null)
            {
                if (remainingTime > 0)
                {
                    remainingTime -= Time.deltaTime;
                }

                if (remainingTime <= 0)
                {
                    endMiniGame.SetActive(true);

                    GameObject player = GameObject.Find("Player");
                    player.transform.position = new Vector2(-28, -51);

                    foreach (GameObject skill in miniGameBoss.skills)
                    {
                        Destroy(skill);
                    }

                    miniGameBoss.skills.Clear();
                    foreach (var skillEnermy in miniGameBoss.skillEnermy1)
                    {
                        SpriteRenderer sprRenderer = skillEnermy.GetComponent<SpriteRenderer>();
                        if (sprRenderer != null)
                        {
                            sprRenderer.enabled = false;
                        }
                    }
                    foreach (var skillEnermy in miniGameBoss.skillEnermy2)
                    {
                        SpriteRenderer sprRenderer = skillEnermy.GetComponent<SpriteRenderer>();
                        if (sprRenderer != null)
                        {
                            sprRenderer.enabled = false;
                        }
                    }

                    //boss.SetActive(false);
                }
            }
        }

        ticketText.text = "����� : " + ticket;

        if (ticket <= 1)
        {
            plusTicket -= Time.deltaTime;
            if (plusTicket <= 0)
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
            if (!miniGameStart)
            {
                bossInstance = Instantiate(boss, bossPoint.transform.position, Quaternion.Euler(0, 180, 0));
                bossInstance.name = "MiniGameBoss";
                GameObject player = GameObject.Find("Player");
                player.transform.position = playerMovePoint.transform.position;
                miniGameStart = true;
                miniGameSelectMenu.SetActive(false);

                miniGameBoss = GameObject.Find("MiniGameBoss").GetComponent<MiniGameBoss>();
                remainingTime = 60;

                // ����� ����
                ticket--;
            }
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
        endMiniGame.SetActive(false);
        miniGameMenu.SetActive(false);

        // �÷��̾� �̵�
        player.transform.position = new Vector2(0, 0);
        playerMovement.currentTarget = null;

        // �÷��̾� �� ȹ��
        playerMovement.money += (int)miniGameBoss.hitDamege / 10;

        // �������� �����
        stageManager.restartStage = true;
        monsterSpwan.RemoveAllMonsters();
        stageManager.EndMiniGameSpawningMonsters();

        // ���� ����
        Destroy(bossInstance);
    }
}