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
    private StageManager stageManger;
    private AudioManager audioManager;

    public GameObject idleUI;
    public float idleMoney;
    public TMP_Text lastQuitTimeText;
    public TMP_Text idleMoneyText;

    public GameObject ResetUI;

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            // ���� ��׶���� ��ȯ�Ǵ� ���, ���� �ð�����
            PlayerPrefs.SetString("������ ���� �ð�", DateTime.Now.ToString());
        }
    }

    private void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        stageManger = GameObject.Find("Manager").GetComponent<StageManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        // ���� ���� Ȯ��
        if (PlayerPrefs.GetInt("GameStarted", 0) == 0)
        {
            // ������ ó�� ���۵Ǿ� GameStarted Ű ���� 1�� ����
            PlayerPrefs.SetInt("GameStarted", 1);
        }
        else
        {
            // ������ ó�� ���۵� ���� �ƴҶ� Idle UI�� ǥ��
            // ������ ���� �ð� ��������
            string lastQuitTimeStr = PlayerPrefs.GetString("������ ���� �ð�", DateTime.Now.ToString());
            DateTime lastQuitTime = DateTime.Parse(lastQuitTimeStr);

            // �÷��̾ ���������� ���� ���� �� �󸶳� �ð��� �������� ���
            TimeSpan idleDuration = DateTime.Now - lastQuitTime;

            // IdleDuration�� ���� ������ ����� ����
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
        audioManager.PlayButtonSound();
        ResetUI.SetActive(!ResetUI.activeSelf);
    }

    public void ResetData()
    {
        // ��� ������ ����
        PlayerPrefs.DeleteAll();

        // �� ������ �ʱⰪ���� ����
        playerMovement.upgradeHealth = 1;
        playerMovement.upgradePower = 1;
        playerMovement.upgradeDefense = 1;
        playerMovement.money = 10;
        stageManger.stage = 1;

        ResetUI.SetActive(false);

        SceneManager.LoadScene("MainMenu");
    }
}