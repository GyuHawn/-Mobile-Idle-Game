using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreScript : MonoBehaviour
{
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
        itemNum = 1;
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
        oneDraw.SetActive(true);
        TenDraw.SetActive(false);
        shop.SetActive(false);
    }
    public void TenDrawScreen()
    {
        oneDraw.SetActive(false);
        TenDraw.SetActive(true);
        shop.SetActive(false);
    }

    public void ExitShop()
    {
        shopAll.SetActive(false);
    }
}
