using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUpgrade : MonoBehaviour // ��ȭ ���� Ȯ��
{
    private InventoryScript inventoryScript;

    // n. �븻 / r. ���� / u. ����ũ / l. �������� / e. ����

    //����
    public int upgradePercent; // �⺻ ��ȭ ��ġ

    // ����
    public int nWeaponUpgrade; // ��ȭ ��ġ
    public int rWeaponUpgrade;
    public int uWeaponUpgrade;
    public int lWeaponUpgrade;
    public int eWeaponUpgrade;

    public TMP_Text nWeaponUpgradeText; // ��ȭ��ġ �ؽ�Ʈ
    public TMP_Text rWeaponUpgradeText;
    public TMP_Text uWeaponUpgradeText;
    public TMP_Text lWeaponUpgradeText;
    public TMP_Text eWeaponUpgradeText;

    public float nWeaponPercent; // ��ȭ Ȯ��
    public float rWeaponPercent;
    public float uWeaponPercent;
    public float lWeaponPercent;
    public float eWeaponPercent;

    public int nWeaponUpMoney; // ��ȭ ���
    public int rWeaponUpMoney;
    public int uWeaponUpMoney;
    public int lWeaponUpMoney;
    public int eWeaponUpMoney;

    public int[] WeaponUpMoney; // ��޺� ��� �⺻ ��ġ
    public int[] weaponPower; // ��޺� ���ݷ� ��ġ

    void Start()
    {
        inventoryScript = GameObject.Find("ItemManager").GetComponent<InventoryScript>();

        WeaponUpMoney = new int[] { 10, 15, 20, 25, 30 };
        weaponPower = new int[] { 1, 2, 3, 5, 10 };
    }

    void Update()
    {
        // ��ȭ �ؽ�Ʈ
        //����
        nWeaponUpgradeText.text = nWeaponUpgrade.ToString();
        rWeaponUpgradeText.text = rWeaponUpgrade.ToString();
        uWeaponUpgradeText.text = uWeaponUpgrade.ToString();
        lWeaponUpgradeText.text = lWeaponUpgrade.ToString();
        eWeaponUpgradeText.text = eWeaponUpgrade.ToString();

        // ��

        // ����


    }

    // ��ȭ Ȯ�� ���
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

    // ��ȭ ��� ����
    public int CalculateCost(int upgradeLevel, int grade)
    {
        return upgradeLevel * WeaponUpMoney[grade];
    }

    // ���ݷ� ����
    public int CalculateAttack(int upgradeLevel, int grade)
    {
        return upgradeLevel * weaponPower[grade];
    }

    // ���� ��ȭ
    public int WeaponUpgrade(int num, float percent, int grade)
    {
        float randomPercent = Random.Range(0f, 100f);

        if (randomPercent <= percent)
        {
            num++;
        }

        return num;
    }

    // �� ��޺� ��ȭ ��ư
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

    // ��
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

    // ����
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