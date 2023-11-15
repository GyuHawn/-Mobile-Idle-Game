using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreScript : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private AudioManager audioManager;

    // 플레이어 돈
    public TMP_Text playerMoney;

    // 현재 뽑을 장비
    public int itemNum; // 1 - 무기, 2 - 방어구, 3 - 반지

    // 상점 장비 전환
    public GameObject weaponMenu;
    public GameObject armorMenu;
    public GameObject ringMenu;

    // 상점, 뽑기창
    public GameObject shopAll; // 전체 상점창
    public GameObject shop; // 상점창
    public GameObject oneDraw;
    public GameObject TenDraw;
    
    // 상점 닫기
    
    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        itemNum = 1;
    }

    private void Update()
    {
        playerMoney.text = playerMovement.money.ToString();
    }

    // 상점 장비 전환
    public void WeaponShop()
    {
        weaponMenu.SetActive(true);
        armorMenu.SetActive(false);
        ringMenu.SetActive(false);
        itemNum = 1;
    }
    public void ArmorShop()
    {
        weaponMenu.SetActive(false);
        armorMenu.SetActive(true);
        ringMenu.SetActive(false);
        itemNum = 2;
    }
    public void RingShop()
    {
        weaponMenu.SetActive(false);
        armorMenu.SetActive(false);
        ringMenu.SetActive(true);
        itemNum = 3;
    }

    // 창 전환
    public void OneDrawScreen()
    {
        if (playerMovement.money >= 100)
        {
            audioManager.PlayItemGrawSound();
            oneDraw.SetActive(true);
            TenDraw.SetActive(false);
            shop.SetActive(false);
            playerMovement.money -= 100;
        }
    }
    public void TenDrawScreen()
    {
        if (playerMovement.money >= 1000)
        {
            audioManager.PlayItemGrawSound();
            oneDraw.SetActive(false);
            TenDraw.SetActive(true);
            shop.SetActive(false);
            playerMovement.money -= 1000;
        }
    }

    public void ExitShop()
    {
        shopAll.SetActive(false);
    }
}
