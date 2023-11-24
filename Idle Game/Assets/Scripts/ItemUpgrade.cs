using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUpgrade : MonoBehaviour // 강화 증가 확인
{
    private PlayerMovement playerMovement;

    // n. 노말 / r. 레어 / u. 유니크 / l. 레전더리 / e. 에픽
    //공용
    public int upgradePercent; // 기본 강화수치
    public TMP_Text playerMoney;

    // 무기
    public int nWeaponUpgrade; // 강화수치
    public int rWeaponUpgrade;
    public int uWeaponUpgrade;
    public int lWeaponUpgrade;
    public int eWeaponUpgrade;
    public TMP_Text nWeaponUpgradeText; // 강화수치 텍스트
    public TMP_Text rWeaponUpgradeText;
    public TMP_Text uWeaponUpgradeText;
    public TMP_Text lWeaponUpgradeText;
    public TMP_Text eWeaponUpgradeText;

    public float nWeaponPercent; // 강화확률
    public float rWeaponPercent;
    public float uWeaponPercent;
    public float lWeaponPercent;
    public float eWeaponPercent;
    public TMP_Text nWeaponPercentText; // 강화확률 텍스트
    public TMP_Text rWeaponPercentText;
    public TMP_Text uWeaponPercentText;
    public TMP_Text lWeaponPercentText;
    public TMP_Text eWeaponPercentText;

    public int nWeaponUpMoney; // 강화비용
    public int rWeaponUpMoney;
    public int uWeaponUpMoney;
    public int lWeaponUpMoney;
    public int eWeaponUpMoney;
    public TMP_Text nWeaponUpMoneyText; // 강화비용 텍스트
    public TMP_Text rWeaponUpMoneyText;
    public TMP_Text uWeaponUpMoneyText;
    public TMP_Text lWeaponUpMoneyText;
    public TMP_Text eWeaponUpMoneyText;

    // 방어구
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

    // 반지
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


    public int[] UpMoney; // 등급별 비용 기본 수치
    public int[] weaponPower; // 등급별 공격력 수치
    public int[] weaponDefense; // 등급별 방어력 수치
    public int[] weaponHealth; // 등급별 체력 수치

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

        // 장비 개수 불러오기
        nWeaponUpgrade = PlayerPrefs.GetInt("nWeaponUpgrade", nWeaponUpgrade);
        rWeaponUpgrade = PlayerPrefs.GetInt("rWeaponUpgrade", rWeaponUpgrade);
        uWeaponUpgrade = PlayerPrefs.GetInt("uWeaponUpgrade", uWeaponUpgrade);
        lWeaponUpgrade = PlayerPrefs.GetInt("lWeaponUpgrade", lWeaponUpgrade);
        eWeaponUpgrade = PlayerPrefs.GetInt("eWeaponUpgrade", eWeaponUpgrade);

        // 방어구 강화 수치 불러오기
        nArmorUpgrade = PlayerPrefs.GetInt("nArmorUpgrade", nArmorUpgrade);
        rArmorUpgrade = PlayerPrefs.GetInt("rArmorUpgrade", rArmorUpgrade);
        uArmorUpgrade = PlayerPrefs.GetInt("uArmorUpgrade", uArmorUpgrade);
        lArmorUpgrade = PlayerPrefs.GetInt("lArmorUpgrade", lArmorUpgrade);
        eArmorUpgrade = PlayerPrefs.GetInt("eArmorUpgrade", eArmorUpgrade);

        // 반지 강화 수치 불러오기
        nRingUpgrade = PlayerPrefs.GetInt("nRingUpgrade", nRingUpgrade);
        rRingUpgrade = PlayerPrefs.GetInt("rRingUpgrade", rRingUpgrade);
        uRingUpgrade = PlayerPrefs.GetInt("uRingUpgrade", uRingUpgrade);
        lRingUpgrade = PlayerPrefs.GetInt("lRingUpgrade", lRingUpgrade);
        eRingUpgrade = PlayerPrefs.GetInt("eRingUpgrade", eRingUpgrade);
    }

    private void OnApplicationPause(bool pauseStatus) // 어플이 정지될때 데이터 저장
    {
        if (pauseStatus)
        {
            // 무기
            PlayerPrefs.SetInt("nWeaponUpgrade", nWeaponUpgrade);
            PlayerPrefs.SetInt("rWeaponUpgrade", rWeaponUpgrade);
            PlayerPrefs.SetInt("uWeaponUpgrade", uWeaponUpgrade);
            PlayerPrefs.SetInt("lWeaponUpgrade", lWeaponUpgrade);
            PlayerPrefs.SetInt("eWeaponUpgrade", eWeaponUpgrade);

            // 방어구
            PlayerPrefs.SetInt("nArmorUpgrade", nArmorUpgrade);
            PlayerPrefs.SetInt("rArmorUpgrade", rArmorUpgrade);
            PlayerPrefs.SetInt("uArmorUpgrade", uArmorUpgrade);
            PlayerPrefs.SetInt("lArmorUpgrade", lArmorUpgrade);
            PlayerPrefs.SetInt("eArmorUpgrade", eArmorUpgrade);

            // 반지
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
        // 무기
        PlayerPrefs.SetInt("nWeaponUpgrade", nWeaponUpgrade);
        PlayerPrefs.SetInt("rWeaponUpgrade", rWeaponUpgrade);
        PlayerPrefs.SetInt("uWeaponUpgrade", uWeaponUpgrade);
        PlayerPrefs.SetInt("lWeaponUpgrade", lWeaponUpgrade);
        PlayerPrefs.SetInt("eWeaponUpgrade", eWeaponUpgrade);

        // 방어구
        PlayerPrefs.SetInt("nArmorUpgrade", nArmorUpgrade);
        PlayerPrefs.SetInt("rArmorUpgrade", rArmorUpgrade);
        PlayerPrefs.SetInt("uArmorUpgrade", uArmorUpgrade);
        PlayerPrefs.SetInt("lArmorUpgrade", lArmorUpgrade);
        PlayerPrefs.SetInt("eArmorUpgrade", eArmorUpgrade);

        // 반지
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

        // 강화 텍스트
        //무기
        nWeaponUpgradeText.text = "+" + nWeaponUpgrade.ToString(); // 강화수치 텍스트
        rWeaponUpgradeText.text = "+" + rWeaponUpgrade.ToString();
        uWeaponUpgradeText.text = "+" + uWeaponUpgrade.ToString();
        lWeaponUpgradeText.text = "+" + lWeaponUpgrade.ToString();
        eWeaponUpgradeText.text = "+" + eWeaponUpgrade.ToString();

        nWeaponPercent = CalculatePercent(nWeaponUpgrade); // 강화 확률
        rWeaponPercent = CalculatePercent(rWeaponUpgrade);
        uWeaponPercent = CalculatePercent(uWeaponUpgrade);
        lWeaponPercent = CalculatePercent(lWeaponUpgrade);
        eWeaponPercent = CalculatePercent(eWeaponUpgrade);

        nWeaponUpMoney = CalculateCost(nWeaponUpgrade, 0); // 강화 비용 
        rWeaponUpMoney = CalculateCost(rWeaponUpgrade, 1);
        uWeaponUpMoney = CalculateCost(uWeaponUpgrade, 2);
        lWeaponUpMoney = CalculateCost(lWeaponUpgrade, 3);
        eWeaponUpMoney = CalculateCost(eWeaponUpgrade, 4);

        nWeaponPercentText.text = nWeaponPercent.ToString() + "%"; // 강화확률 텍스트
        rWeaponPercentText.text = rWeaponPercent.ToString() + "%";
        uWeaponPercentText.text = uWeaponPercent.ToString() + "%";
        lWeaponPercentText.text = lWeaponPercent.ToString() + "%";
        eWeaponPercentText.text = eWeaponPercent.ToString() + "%";

        nWeaponUpMoneyText.text = nWeaponUpMoney.ToString(); // 강화비용 텍스트
        rWeaponUpMoneyText.text = rWeaponUpMoney.ToString();
        uWeaponUpMoneyText.text = uWeaponUpMoney.ToString();
        lWeaponUpMoneyText.text = lWeaponUpMoney.ToString();
        eWeaponUpMoneyText.text = eWeaponUpMoney.ToString();


        // 방어구
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


        // 반지
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


    // 강화 확률 계산
    public float CalculatePercent(int upgradeLevel)
    {
        if (upgradeLevel == 0)
            return 100;
        else if (upgradeLevel <= 100)
            return 90 - upgradeLevel / 10 * 10; // 10 단위로 감소
        else
            return 1;
    }

    // 강화 비용 증가
    public int CalculateCost(int upgradeLevel, int grade)
    {
        return upgradeLevel * UpMoney[grade];
    }

    // 공격력 증가
    public int CalculateAttack(int upgradeLevel, int grade)
    {
        return upgradeLevel * weaponPower[grade];
    }

    // 방어력 증가
    public int CalculateDefense(int upgradeLevel, int grade)
    {
        return upgradeLevel * weaponDefense[grade];
    }

    // 체력 증가
    public int CalculateHealth(int upgradeLevel, int grade)
    {
        return upgradeLevel * weaponHealth[grade];
    }

    // 무기 강화
    public int UpgradeNum(int num, float percent, int grade)
    {
        float randomPercent = Random.Range(0f, 100f);

        if (randomPercent <= percent)
        {
            num++;
        }

        return num;
    }

    // 무기 강화 버튼
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


    // 방어구 강화 버튼
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


    // 반지 강화 버튼
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
