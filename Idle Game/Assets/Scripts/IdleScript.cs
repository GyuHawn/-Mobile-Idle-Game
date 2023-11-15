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
            // 앱이 백그라운드로 전환되는 경우, 현재 시간저장
            PlayerPrefs.SetString("마지막 종료 시간", DateTime.Now.ToString());
        }
    }

    private void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        stageManger = GameObject.Find("Manager").GetComponent<StageManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        // 게임 시작 확인
        if (PlayerPrefs.GetInt("GameStarted", 0) == 0)
        {
            // 게임이 처음 시작되어 GameStarted 키 값을 1로 설정
            PlayerPrefs.SetInt("GameStarted", 1);
        }
        else
        {
            // 게임이 처음 시작된 것이 아닐때 Idle UI를 표시
            // 마지막 종료 시간 가져오기
            string lastQuitTimeStr = PlayerPrefs.GetString("마지막 종료 시간", DateTime.Now.ToString());
            DateTime lastQuitTime = DateTime.Parse(lastQuitTimeStr);

            // 플레이어가 마지막으로 게임 종료 후 얼마나 시간이 지났는지 계산
            TimeSpan idleDuration = DateTime.Now - lastQuitTime;

            // IdleDuration을 기준 보상을 계산후 적용
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
        audioManager.PlayButtonSound();
        ResetUI.SetActive(!ResetUI.activeSelf);
    }

    public void ResetData()
    {
        // 모든 데이터 삭제
        PlayerPrefs.DeleteAll();

        // 각 변수를 초기값으로 설정
        playerMovement.upgradeHealth = 1;
        playerMovement.upgradePower = 1;
        playerMovement.upgradeDefense = 1;
        playerMovement.money = 10;
        stageManger.stage = 1;

        ResetUI.SetActive(false);

        SceneManager.LoadScene("MainMenu");
    }
}