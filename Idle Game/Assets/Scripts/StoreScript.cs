using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreScript : MonoBehaviour
{
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
        itemNum = 1;
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
