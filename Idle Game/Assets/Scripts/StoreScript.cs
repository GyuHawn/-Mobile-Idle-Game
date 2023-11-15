using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreScript : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private AudioManager audioManager;

    // �÷��̾� ��
    public TMP_Text playerMoney;

    // ���� ���� ���
    public int itemNum; // 1 - ����, 2 - ��, 3 - ����

    // ���� ��� ��ȯ
    public GameObject weaponMenu;
    public GameObject armorMenu;
    public GameObject ringMenu;

    // ����, �̱�â
    public GameObject shopAll; // ��ü ����â
    public GameObject shop; // ����â
    public GameObject oneDraw;
    public GameObject TenDraw;
    
    // ���� �ݱ�
    
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

    // ���� ��� ��ȯ
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

    // â ��ȯ
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
