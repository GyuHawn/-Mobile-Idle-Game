using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUpgrade : MonoBehaviour // 강화 증가 확인
{
    private InventoryScript inventoryScript;

    // n. 노말 / r. 레어 / u. 유니크 / l. 레전더리 / e. 에픽

    //공용
    public int upgradePercent; // 기본 강화 수치

    // 무기
    public int nWeaponUpgrade; // 강화 수치
    public int rWeaponUpgrade;
    public int uWeaponUpgrade;
    public int lWeaponUpgrade;
    public int eWeaponUpgrade;

    public TMP_Text nWeaponUpgradeText; // 강화수치 텍스트
    public TMP_Text rWeaponUpgradeText;
    public TMP_Text uWeaponUpgradeText;
    public TMP_Text lWeaponUpgradeText;
    public TMP_Text eWeaponUpgradeText;

    public float nWeaponPercent; // 강화 확률
    public float rWeaponPercent;
    public float uWeaponPercent;
    public float lWeaponPercent;
    public float eWeaponPercent;

    public int nWeaponUpMoney; // 강화 비용
    public int rWeaponUpMoney;
    public int uWeaponUpMoney;
    public int lWeaponUpMoney;
    public int eWeaponUpMoney;

    public int[] WeaponUpMoney; // 등급별 비용 기본 수치
    public int[] weaponPower; // 등급별 공격력 수치

    void Start()
    {
        inventoryScript = GameObject.Find("ItemManager").GetComponent<InventoryScript>();

        WeaponUpMoney = new int[] { 10, 15, 20, 25, 30 };
        weaponPower = new int[] { 1, 2, 3, 5, 10 };
    }

    void Update()
    {
        // 강화 텍스트
        //무기
        nWeaponUpgradeText.text = nWeaponUpgrade.ToString();
        rWeaponUpgradeText.text = rWeaponUpgrade.ToString();
        uWeaponUpgradeText.text = uWeaponUpgrade.ToString();
        lWeaponUpgradeText.text = lWeaponUpgrade.ToString();
        eWeaponUpgradeText.text = eWeaponUpgrade.ToString();

        // 방어구

        // 반지


    }

    // 강화 확률 계산
    public float CalculatePercent(int upgradeLevel)
    {
        if (upgradeLevel == 0)
            return 100;
        else if (upgradeLevel <= 10)
            return 90;
        else if (upgradeLevel <= 20)
            return 80;
        else if (upgradeLevel <= 30)
            return 70;
        else
            return 1;
    }

    // 강화 비용 증가
    public int CalculateCost(int upgradeLevel, int grade)
    {
        return upgradeLevel * WeaponUpMoney[grade];
    }

    // 공격력 증가
    public int CalculateAttack(int upgradeLevel, int grade)
    {
        return upgradeLevel * weaponPower[grade];
    }

    // 무기 강화
    public int WeaponUpgrade(int num, float percent, int grade)
    {
        float randomPercent = Random.Range(0f, 100f);

        if (randomPercent <= percent)
        {
            num++;
        }

        return num;
    }

    // 각 등급별 강화 버튼
    public void NomalWeapon()
    {
        nWeaponUpgrade = WeaponUpgrade(nWeaponUpgrade, CalculatePercent(nWeaponUpgrade), 0);
    }
    public void RareWeapon()
    {
        rWeaponUpgrade = WeaponUpgrade(rWeaponUpgrade, CalculatePercent(rWeaponUpgrade), 1);
    }
    public void UniqueWeapon()
    {
        uWeaponUpgrade = WeaponUpgrade(uWeaponUpgrade, CalculatePercent(uWeaponUpgrade), 2);
    }
    public void LegendWeapon()
    {
        lWeaponUpgrade = WeaponUpgrade(lWeaponUpgrade, CalculatePercent(lWeaponUpgrade), 3);
    }
    public void EpicWeapon()
    {
        eWeaponUpgrade = WeaponUpgrade(eWeaponUpgrade, CalculatePercent(eWeaponUpgrade), 4);
    }
}

/*

    // 방어구
    public int nArmorUpgrade;
    public int rArmorUpgrade;
    public int uArmorUpgrade;
    public int lArmorUpgrade;
    public int eArmorUpgrade;

    public float nArmorPercent;
    public float rArmorPercent;
    public float uArmorPercent;
    public float lArmorPercent;
    public float eArmorPercent;

    public int nArmorUpMoney;
    public int rArmorUpMoney;
    public int uArmorUpMoney;
    public int lArmorUpMoney;
    public int eArmorUpMoney;

    // 반지
    public int nRingUpgrade;
    public int rRingUpgrade;
    public int uRingUpgrade;
    public int lRingUpgrade;
    public int eRingUpgrade;

    public float nRingPercent;
    public float rRingPercent;
    public float uRingPercent;
    public float lRingPercent;
    public float eRingPercent;

    public int nRingUpMoney;
    public int rRingUpMoney;
    public int uRingUpMoney;
    public int lRingUpMoney;
    public int eRingUpMoney;
*/