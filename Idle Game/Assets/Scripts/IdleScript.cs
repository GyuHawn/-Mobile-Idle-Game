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

    // 게임종료한 순간부터 다시 킬때까지 시간계산 (예 : 100분 종료 인데 1분마다 스테이지 * 1골드)
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            // 앱이 백그라운드로 전환되는 경우, 현재 시간을 저장합니다.
            PlayerPrefs.SetString("마지막 종료 시간", DateTime.Now.ToString());
        }
    }

    private void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        stageManger = GameObject.Find("Manager").GetComponent<StageManger>();

        // 게임이 처음 시작되었는지 확인합니다.
        if (PlayerPrefs.GetInt("GameStarted", 0) == 0)
        {
            // 게임이 처음 시작되었으므로 GameStarted 키의 값을 1로 설정합니다.
            PlayerPrefs.SetInt("GameStarted", 1);
        }
        else
        {
            // 게임이 처음 시작된 것이 아니므로 Idle UI를 표시합니다.
            // 마지막 종료 시간 가져오기
            string lastQuitTimeStr = PlayerPrefs.GetString("마지막 종료 시간", DateTime.Now.ToString());
            DateTime lastQuitTime = DateTime.Parse(lastQuitTimeStr);

            // 플레이어가 마지막으로 게임을 종료한 후 얼마나 시간이 지났는지 계산합니다.
            TimeSpan idleDuration = DateTime.Now - lastQuitTime;

            // IdleDuration을 기준으로 보상을 계산하고 적용합니다.
            idleMoney = CalculateIdleMoney(idleDuration, stageManger.stage);
            playerMovement.money += (int)idleMoney;

            // idleUI 활성화
            idleUI.SetActive(true);

            // 종료한 시간 표시
            lastQuitTimeText.text = $"{idleDuration.Minutes}Min {idleDuration.Seconds}Sec";

            // 대기중에 얻은 돈 표시
            idleMoneyText.text = $"{idleMoney}";
        }
    }

    // 종료한 시간 중 1분마다 stage * 5원 만큼 값을 받도록 계산하는 함수
    private float CalculateIdleMoney(TimeSpan idleDuration, int stage)
    {
        int totalMinutes = (int)idleDuration.TotalMinutes; // 종료한 총 분
        return totalMinutes * stage * 5; // 1분마다 stage * 5원
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
        // 모든 데이터를 삭제합니다.
        PlayerPrefs.DeleteAll();

        // 각 변수를 초기값으로 설정합니다.
        playerMovement.upgradeHealth = 1;
        playerMovement.upgradePower = 1;
        playerMovement.upgradeDefense = 1;
        playerMovement.money = 10;
        stageManger.stage = 1;

        ResetUI.SetActive(false);

        SceneManager.LoadScene("MainMenu");
    }
}