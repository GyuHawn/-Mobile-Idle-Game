using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUpgrade : MonoBehaviour // ��ȭ ���� Ȯ��
{
    private PlayerMovement playerMovement;

    // n. �븻 / r. ���� / u. ����ũ / l. �������� / e. ����
    //����
    public int upgradePercent; // �⺻ ��ȭ��ġ
    public TMP_Text playerMoney;

    // ����
    public int nWeaponUpgrade; // ��ȭ��ġ
    public int rWeaponUpgrade;
    public int uWeaponUpgrade;
    public int lWeaponUpgrade;
    public int eWeaponUpgrade;
    public TMP_Text nWeaponUpgradeText; // ��ȭ��ġ �ؽ�Ʈ
    public TMP_Text rWeaponUpgradeText;
    public TMP_Text uWeaponUpgradeText;
    public TMP_Text lWeaponUpgradeText;
    public TMP_Text eWeaponUpgradeText;

    public float nWeaponPercent; // ��ȭȮ��
    public float rWeaponPercent;
    public float uWeaponPercent;
    public float lWeaponPercent;
    public float eWeaponPercent;
    public TMP_Text nWeaponPercentText; // ��ȭȮ�� �ؽ�Ʈ
    public TMP_Text rWeaponPercentText;
    public TMP_Text uWeaponPercentText;
    public TMP_Text lWeaponPercentText;
    public TMP_Text eWeaponPercentText;

    public int nWeaponUpMoney; // ��ȭ���
    public int rWeaponUpMoney;
    public int uWeaponUpMoney;
    public int lWeaponUpMoney;
    public int eWeaponUpMoney;
    public TMP_Text nWeaponUpMoneyText; // ��ȭ��� �ؽ�Ʈ
    public TMP_Text rWeaponUpMoneyText;
    public TMP_Text uWeaponUpMoneyText;
    public TMP_Text lWeaponUpMoneyText;
    public TMP_Text eWeaponUpMoneyText;

    // ��
    public int nArmorUpgrade;
    public int rArmorUpgrade;
    public int uArmorUpgrade;
    public int lArmorUpgrade;
    public int eArmorUpgrade;
    public TMP_Text nArmorUpgradeText;
    public TMP_Text rArmorUpgradeText;
    public TMP_Text uArmorUpgradeText;
    public TMP_Text lArmorUpgradeText;
    public TMP_Text eArmorUpgradeText;

    public float nArmorPercent;
    public float rArmorPercent;
    public float uArmorPercent;
    public float lArmorPercent;
    public float eArmorPercent;
    public TMP_Text nArmorPercentText;
    public TMP_Text rArmorPercentText;
    public TMP_Text uArmorPercentText;
    public TMP_Text lArmorPercentText;
    public TMP_Text eArmorPercentText;

    public int nArmorUpMoney;
    public int rArmorUpMoney;
    public int uArmorUpMoney;
    public int lArmorUpMoney;
    public int eArmorUpMoney;
    public TMP_Text nArmorUpMoneyText;
    public TMP_Text rArmorUpMoneyText;
    public TMP_Text uArmorUpMoneyText;
    public TMP_Text lArmorUpMoneyText;
    public TMP_Text eArmorUpMoneyText;

    // ����
    public int nRingUpgrade;
    public int rRingUpgrade;
    public int uRingUpgrade;
    public int lRingUpgrade;
    public int eRingUpgrade;
    public TMP_Text nRingUpgradeText;
    public TMP_Text rRingUpgradeText;
    public TMP_Text uRingUpgradeText;
    public TMP_Text lRingUpgradeText;
    public TMP_Text eRingUpgradeText;


    public float nRingPercent;
    public float rRingPercent;
    public float uRingPercent;
    public float lRingPercent;
    public float eRingPercent;
    public TMP_Text nRingPercentText;
    public TMP_Text rRingPercentText;
    public TMP_Text uRingPercentText;
    public TMP_Text lRingPercentText;
    public TMP_Text eRingPercentText;


    public int nRingUpMoney;
    public int rRingUpMoney;
    public int uRingUpMoney;
    public int lRingUpMoney;
    public int eRingUpMoney;
    public TMP_Text nRingUpMoneyText;
    public TMP_Text rRingUpMoneyText;
    public TMP_Text uRingUpMoneyText;
    public TMP_Text lRingUpMoneyText;
    public TMP_Text eRingUpMoneyText;


    public int[] UpMoney; // ��޺� ��� �⺻ ��ġ
    public int[] weaponPower; // ��޺� ���ݷ� ��ġ
    public int[] weaponDefense; // ��޺� ���� ��ġ
    public int[] weaponHealth; // ��޺� ü�� ��ġ

    private void Awake()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void Start()
    {
        UpMoney = new int[] { 10, 15, 20, 25, 30 };
        weaponPower = new int[] { 1, 2, 3, 5, 10 };
        weaponDefense = new int[] { 1, 2, 3, 5, 10 };
        weaponHealth = new int[] { 5, 10, 15, 20, 30 };

        // ��� ���� �ҷ�����
        nWeaponUpgrade = PlayerPrefs.GetInt("nWeaponUpgrade", nWeaponUpgrade);
        rWeaponUpgrade = PlayerPrefs.GetInt("rWeaponUpgrade", rWeaponUpgrade);
        uWeaponUpgrade = PlayerPrefs.GetInt("uWeaponUpgrade", uWeaponUpgrade);
        lWeaponUpgrade = PlayerPrefs.GetInt("lWeaponUpgrade", lWeaponUpgrade);
        eWeaponUpgrade = PlayerPrefs.GetInt("eWeaponUpgrade", eWeaponUpgrade);

        // �� ��ȭ ��ġ �ҷ�����
        nArmorUpgrade = PlayerPrefs.GetInt("nArmorUpgrade", nArmorUpgrade);
        rArmorUpgrade = PlayerPrefs.GetInt("rArmorUpgrade", rArmorUpgrade);
        uArmorUpgrade = PlayerPrefs.GetInt("uArmorUpgrade", uArmorUpgrade);
        lArmorUpgrade = PlayerPrefs.GetInt("lArmorUpgrade", lArmorUpgrade);
        eArmorUpgrade = PlayerPrefs.GetInt("eArmorUpgrade", eArmorUpgrade);

        // ���� ��ȭ ��ġ �ҷ�����
        nRingUpgrade = PlayerPrefs.GetInt("nRingUpgrade", nRingUpgrade);
        rRingUpgrade = PlayerPrefs.GetInt("rRingUpgrade", rRingUpgrade);
        uRingUpgrade = PlayerPrefs.GetInt("uRingUpgrade", uRingUpgrade);
        lRingUpgrade = PlayerPrefs.GetInt("lRingUpgrade", lRingUpgrade);
        eRingUpgrade = PlayerPrefs.GetInt("eRingUpgrade", eRingUpgrade);
    }

    private void OnApplicationPause(bool pauseStatus) // ������ �����ɶ� ������ ����
    {
        if (pauseStatus)
        {
            // ����
            PlayerPrefs.SetInt("nWeaponUpgrade", nWeaponUpgrade);
            PlayerPrefs.SetInt("rWeaponUpgrade", rWeaponUpgrade);
            PlayerPrefs.SetInt("uWeaponUpgrade", uWeaponUpgrade);
            PlayerPrefs.SetInt("lWeaponUpgrade", lWeaponUpgrade);
            PlayerPrefs.SetInt("eWeaponUpgrade", eWeaponUpgrade);

            // ��
            PlayerPrefs.SetInt("nArmorUpgrade", nArmorUpgrade);
            PlayerPrefs.SetInt("rArmorUpgrade", rArmorUpgrade);
            PlayerPrefs.SetInt("uArmorUpgrade", uArmorUpgrade);
            PlayerPrefs.SetInt("lArmorUpgrade", lArmorUpgrade);
            PlayerPrefs.SetInt("eArmorUpgrade", eArmorUpgrade);

            // ����
            PlayerPrefs.SetInt("nRingUpgrade", nRingUpgrade);
            PlayerPrefs.SetInt("rRingUpgrade", rRingUpgrade);
            PlayerPrefs.SetInt("uRingUpgrade", uRingUpgrade);
            PlayerPrefs.SetInt("lRingUpgrade", lRingUpgrade);
            PlayerPrefs.SetInt("eRingUpgrade", eRingUpgrade);

            PlayerPrefs.Save();
        }
    }

    void OnApplicationQuit()
    {
        // ����
        PlayerPrefs.SetInt("nWeaponUpgrade", nWeaponUpgrade);
        PlayerPrefs.SetInt("rWeaponUpgrade", rWeaponUpgrade);
        PlayerPrefs.SetInt("uWeaponUpgrade", uWeaponUpgrade);
        PlayerPrefs.SetInt("lWeaponUpgrade", lWeaponUpgrade);
        PlayerPrefs.SetInt("eWeaponUpgrade", eWeaponUpgrade);

        // ��
        PlayerPrefs.SetInt("nArmorUpgrade", nArmorUpgrade);
        PlayerPrefs.SetInt("rArmorUpgrade", rArmorUpgrade);
        PlayerPrefs.SetInt("uArmorUpgrade", uArmorUpgrade);
        PlayerPrefs.SetInt("lArmorUpgrade", lArmorUpgrade);
        PlayerPrefs.SetInt("eArmorUpgrade", eArmorUpgrade);

        // ����
        PlayerPrefs.SetInt("nRingUpgrade", nRingUpgrade);
        PlayerPrefs.SetInt("rRingUpgrade", rRingUpgrade);
        PlayerPrefs.SetInt("uRingUpgrade", uRingUpgrade);
        PlayerPrefs.SetInt("lRingUpgrade", lRingUpgrade);
        PlayerPrefs.SetInt("eRingUpgrade", eRingUpgrade);

        PlayerPrefs.Save();
    }

    void Update()
    {
        playerMoney.text = playerMovement.money.ToString();

        // ��ȭ �ؽ�Ʈ
        //����
        nWeaponUpgradeText.text = "+" + nWeaponUpgrade.ToString(); // ��ȭ��ġ �ؽ�Ʈ
        rWeaponUpgradeText.text = "+" + rWeaponUpgrade.ToString();
        uWeaponUpgradeText.text = "+" + uWeaponUpgrade.ToString();
        lWeaponUpgradeText.text = "+" + lWeaponUpgrade.ToString();
        eWeaponUpgradeText.text = "+" + eWeaponUpgrade.ToString();

        nWeaponPercent = CalculatePercent(nWeaponUpgrade); // ��ȭ Ȯ��
        rWeaponPercent = CalculatePercent(rWeaponUpgrade);
        uWeaponPercent = CalculatePercent(uWeaponUpgrade);
        lWeaponPercent = CalculatePercent(lWeaponUpgrade);
        eWeaponPercent = CalculatePercent(eWeaponUpgrade);

        nWeaponUpMoney = CalculateCost(nWeaponUpgrade, 0); // ��ȭ ��� 
        rWeaponUpMoney = CalculateCost(rWeaponUpgrade, 1);
        uWeaponUpMoney = CalculateCost(uWeaponUpgrade, 2);
        lWeaponUpMoney = CalculateCost(lWeaponUpgrade, 3);
        eWeaponUpMoney = CalculateCost(eWeaponUpgrade, 4);

        nWeaponPercentText.text = nWeaponPercent.ToString() + "%"; // ��ȭȮ�� �ؽ�Ʈ
        rWeaponPercentText.text = rWeaponPercent.ToString() + "%";
        uWeaponPercentText.text = uWeaponPercent.ToString() + "%";
        lWeaponPercentText.text = lWeaponPercent.ToString() + "%";
        eWeaponPercentText.text = eWeaponPercent.ToString() + "%";

        nWeaponUpMoneyText.text = nWeaponUpMoney.ToString(); // ��ȭ��� �ؽ�Ʈ
        rWeaponUpMoneyText.text = rWeaponUpMoney.ToString();
        uWeaponUpMoneyText.text = uWeaponUpMoney.ToString();
        lWeaponUpMoneyText.text = lWeaponUpMoney.ToString();
        eWeaponUpMoneyText.text = eWeaponUpMoney.ToString();


        // ��
        nArmorUpgradeText.text = "+" + nArmorUpgrade.ToString();
        rArmorUpgradeText.text = "+" + rArmorUpgrade.ToString();
        uArmorUpgradeText.text = "+" + uArmorUpgrade.ToString();
        lArmorUpgradeText.text = "+" + lArmorUpgrade.ToString();
        eArmorUpgradeText.text = "+" + eArmorUpgrade.ToString();

        nArmorPercent = CalculatePercent(nArmorUpgrade);
        rArmorPercent = CalculatePercent(rArmorUpgrade);
        uArmorPercent = CalculatePercent(uArmorUpgrade);
        lArmorPercent = CalculatePercent(lArmorUpgrade);
        eArmorPercent = CalculatePercent(eArmorUpgrade);

        nArmorUpMoney = CalculateCost(nArmorUpgrade, 0);
        rArmorUpMoney = CalculateCost(rArmorUpgrade, 1);
        uArmorUpMoney = CalculateCost(uArmorUpgrade, 2);
        lArmorUpMoney = CalculateCost(lArmorUpgrade, 3);
        eArmorUpMoney = CalculateCost(eArmorUpgrade, 4);

        nArmorPercentText.text = nArmorPercent.ToString() + "%";
        rArmorPercentText.text = rArmorPercent.ToString() + "%";
        uArmorPercentText.text = uArmorPercent.ToString() + "%";
        lArmorPercentText.text = lArmorPercent.ToString() + "%";
        eArmorPercentText.text = eArmorPercent.ToString() + "%";

        nArmorUpMoneyText.text = nArmorUpMoney.ToString();
        rArmorUpMoneyText.text = rArmorUpMoney.ToString();
        uArmorUpMoneyText.text = uArmorUpMoney.ToString();
        lArmorUpMoneyText.text = lArmorUpMoney.ToString();
        eArmorUpMoneyText.text = eArmorUpMoney.ToString();


        // ����
        nRingUpgradeText.text = "+" + nRingUpgrade.ToString();
        rRingUpgradeText.text = "+" + rRingUpgrade.ToString();
        uRingUpgradeText.text = "+" + uRingUpgrade.ToString();
        lRingUpgradeText.text = "+" + lRingUpgrade.ToString();
        eRingUpgradeText.text = "+" + eRingUpgrade.ToString();

        nRingPercent = CalculatePercent(nRingUpgrade);
        rRingPercent = CalculatePercent(rRingUpgrade);
        uRingPercent = CalculatePercent(uRingUpgrade);
        lRingPercent = CalculatePercent(lRingUpgrade);
        eRingPercent = CalculatePercent(eRingUpgrade);

        nRingUpMoney = CalculateCost(nRingUpgrade, 0);
        rRingUpMoney = CalculateCost(rRingUpgrade, 1);
        uRingUpMoney = CalculateCost(uRingUpgrade, 2);
        lRingUpMoney = CalculateCost(lRingUpgrade, 3);
        eRingUpMoney = CalculateCost(eRingUpgrade, 4);

        nRingPercentText.text = nRingPercent.ToString() + "%";
        rRingPercentText.text = rRingPercent.ToString() + "%";
        uRingPercentText.text = uRingPercent.ToString() + "%";
        lRingPercentText.text = lRingPercent.ToString() + "%";
        eRingPercentText.text = eRingPercent.ToString() + "%";

        nRingUpMoneyText.text = nRingUpMoney.ToString();
        rRingUpMoneyText.text = rRingUpMoney.ToString();
        uRingUpMoneyText.text = uRingUpMoney.ToString();
        lRingUpMoneyText.text = lRingUpMoney.ToString();
        eRingUpMoneyText.text = eRingUpMoney.ToString();
    }


    // ��ȭ Ȯ�� ���
    public float CalculatePercent(int upgradeLevel)
    {
        if (upgradeLevel == 0)
            return 100;
        else if (upgradeLevel <= 100)
            return 90 - upgradeLevel / 10 * 10; // 10 ������ ����
        else
            return 1;
    }

    // ��ȭ ��� ����
    public int CalculateCost(int upgradeLevel, int grade)
    {
        return upgradeLevel * UpMoney[grade];
    }

    // ���ݷ� ����
    public int CalculateAttack(int upgradeLevel, int grade)
    {
        return upgradeLevel * weaponPower[grade];
    }

    // ���� ����
    public int CalculateDefense(int upgradeLevel, int grade)
    {
        return upgradeLevel * weaponDefense[grade];
    }

    // ü�� ����
    public int CalculateHealth(int upgradeLevel, int grade)
    {
        return upgradeLevel * weaponHealth[grade];
    }

    // ���� ��ȭ
    public int UpgradeNum(int num, float percent, int grade)
    {
        float randomPercent = Random.Range(0f, 100f);

        if (randomPercent <= percent)
        {
            num++;
        }

        return num;
    }

    // ���� ��ȭ ��ư
    public void NomalWeapon()
    {
        if (playerMovement.money >= nWeaponUpMoney)
        {
            nWeaponUpgrade = UpgradeNum(nWeaponUpgrade, CalculatePercent(nWeaponUpgrade), 0);
            playerMovement.money -= nWeaponUpMoney;
        }
    }

    public void RareWeapon()
    {
        if (playerMovement.money >= rWeaponUpMoney)
        {
            rWeaponUpgrade = UpgradeNum(rWeaponUpgrade, CalculatePercent(rWeaponUpgrade), 1);
            playerMovement.money -= rWeaponUpMoney;
        }
    }

    public void UniqueWeapon()
    {
        if (playerMovement.money >= uWeaponUpMoney)
        {
            uWeaponUpgrade = UpgradeNum(uWeaponUpgrade, CalculatePercent(uWeaponUpgrade), 2);
            playerMovement.money -= uWeaponUpMoney;
        }
    }

    public void LegendWeapon()
    {
        if (playerMovement.money >= lWeaponUpMoney)
        {
            lWeaponUpgrade = UpgradeNum(lWeaponUpgrade, CalculatePercent(lWeaponUpgrade), 3);
            playerMovement.money -= lWeaponUpMoney;
        }
    }

    public void EpicWeapon()
    {
        if (playerMovement.money >= eWeaponUpMoney)
        {
            eWeaponUpgrade = UpgradeNum(eWeaponUpgrade, CalculatePercent(eWeaponUpgrade), 4);
            playerMovement.money -= eWeaponUpMoney;
        }
    }


    // �� ��ȭ ��ư
    public void NormalArmor()
    {
        if (playerMovement.money >= nArmorUpMoney)
        {
            nArmorUpgrade = UpgradeNum(nArmorUpgrade, CalculatePercent(nArmorUpgrade), 0);
            playerMovement.money -= nArmorUpMoney;
        }
    }

    public void RareArmor()
    {
        if (playerMovement.money >= rArmorUpMoney)
        {
            rArmorUpgrade = UpgradeNum(rArmorUpgrade, CalculatePercent(rArmorUpgrade), 1);
            playerMovement.money -= rArmorUpMoney;
        }
    }

    public void UniqueArmor()
    {
        if (playerMovement.money >= uArmorUpMoney)
        {
            uArmorUpgrade = UpgradeNum(uArmorUpgrade, CalculatePercent(uArmorUpgrade), 2);
            playerMovement.money -= uArmorUpMoney;
        }
    }

    public void LegendArmor()
    {
        if (playerMovement.money >= lArmorUpMoney)
        {
            lArmorUpgrade = UpgradeNum(lArmorUpgrade, CalculatePercent(lArmorUpgrade), 3);
            playerMovement.money -= lArmorUpMoney;
        }
    }

    public void EpicArmor()
    {
        if (playerMovement.money >= eArmorUpMoney)
        {
            eArmorUpgrade = UpgradeNum(eArmorUpgrade, CalculatePercent(eArmorUpgrade), 4);
            playerMovement.money -= eArmorUpMoney;
        }
    }


    // ���� ��ȭ ��ư
    public void NormalRing()
    {
        if (playerMovement.money >= nRingUpMoney)
        {
            nRingUpgrade = UpgradeNum(nRingUpgrade, CalculatePercent(nRingUpgrade), 0);
            playerMovement.money -= nRingUpMoney;
        }
    }

    public void RareRing()
    {
        if (playerMovement.money >= rRingUpMoney)
        {
            rRingUpgrade = UpgradeNum(rRingUpgrade, CalculatePercent(rRingUpgrade), 1);
            playerMovement.money -= rRingUpMoney;
        }
    }

    public void UniqueRing()
    {
        if (playerMovement.money >= uRingUpMoney)
        {
            uRingUpgrade = UpgradeNum(uRingUpgrade, CalculatePercent(uRingUpgrade), 2);
            playerMovement.money -= uRingUpMoney;
        }
    }

    public void LegendRing()
    {
        if (playerMovement.money >= lRingUpMoney)
        {
            lRingUpgrade = UpgradeNum(lRingUpgrade, CalculatePercent(lRingUpgrade), 3);
            playerMovement.money -= lRingUpMoney;
        }
    }

    public void EpicRing()
    {
        if (playerMovement.money >= eRingUpMoney)
        {
            eRingUpgrade = UpgradeNum(eRingUpgrade, CalculatePercent(eRingUpgrade), 4);
            playerMovement.money -= eRingUpMoney;
        }
    }
}
