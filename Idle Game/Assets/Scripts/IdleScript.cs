using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class IdleScript : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private StageManger stageManger;

    public GameObject idleUI;
    public float idleMoney;
    public TMP_Text lastQuitTimeText;
    public TMP_Text idleMoneyText;

    public GameObject ResetUI;

    // ���������� �������� �ٽ� ų������ �ð���� (�� : 100�� ���� �ε� 1�и��� �������� * 1���)
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            // ���� ��׶���� ��ȯ�Ǵ� ���, ���� �ð��� �����մϴ�.
            PlayerPrefs.SetString("������ ���� �ð�", DateTime.Now.ToString());
        }
    }

    private void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        stageManger = GameObject.Find("Manager").GetComponent<StageManger>();

        // ������ ó�� ���۵Ǿ����� Ȯ���մϴ�.
        if (PlayerPrefs.GetInt("GameStarted", 0) == 0)
        {
            // ������ ó�� ���۵Ǿ����Ƿ� GameStarted Ű�� ���� 1�� �����մϴ�.
            PlayerPrefs.SetInt("GameStarted", 1);
        }
        else
        {
            // ������ ó�� ���۵� ���� �ƴϹǷ� Idle UI�� ǥ���մϴ�.
            // ������ ���� �ð� ��������
            string lastQuitTimeStr = PlayerPrefs.GetString("������ ���� �ð�", DateTime.Now.ToString());
            DateTime lastQuitTime = DateTime.Parse(lastQuitTimeStr);

            // �÷��̾ ���������� ������ ������ �� �󸶳� �ð��� �������� ����մϴ�.
            TimeSpan idleDuration = DateTime.Now - lastQuitTime;

            // IdleDuration�� �������� ������ ����ϰ� �����մϴ�.
            idleMoney = CalculateIdleMoney(idleDuration, stageManger.stage);
            playerMovement.money += (int)idleMoney;

            // idleUI Ȱ��ȭ
            idleUI.SetActive(true);

            // ������ �ð� ǥ��
            lastQuitTimeText.text = $"{idleDuration.Minutes}Min {idleDuration.Seconds}Sec";

            // ����߿� ���� �� ǥ��
            idleMoneyText.text = $"{idleMoney}";
        }
    }

    // ������ �ð� �� 1�и��� stage * 5�� ��ŭ ���� �޵��� ����ϴ� �Լ�
    private float CalculateIdleMoney(TimeSpan idleDuration, int stage)
    {
        int totalMinutes = (int)idleDuration.TotalMinutes; // ������ �� ��
        return totalMinutes * stage * 5; // 1�и��� stage * 5��
    }

    public void OffIdleUI()
    {
        idleUI.SetActive(false);
    }

    public void OnResetUI()
    {
        ResetUI.SetActive(true);
    }

    public void ResetData()
    {
        // ��� �����͸� �����մϴ�.
        PlayerPrefs.DeleteAll();

        // �� ������ �ʱⰪ���� �����մϴ�.
        playerMovement.upgradeHealth = 1;
        playerMovement.upgradePower = 1;
        playerMovement.upgradeDefense = 1;
        playerMovement.money = 10;
        stageManger.stage = 1;

        ResetUI.SetActive(false);

        SceneManager.LoadScene("MainMenu");
    }
}