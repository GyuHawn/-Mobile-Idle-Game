using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class InventoryScript : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private AudioManager audioManager;

    // ����
    public GameObject inventory; // ���â

    // ��� ��ġ ����
    public float weaponPower; // ���� ���ݷ�
    public float armorDefense; // �� ����
    public float ringHealth; // ���� ü��
    public float[] weaponPowerValues = new float[] { 1, 3, 5, 10, 20 };
    public float[] armorDefenseValues = new float[] { 1, 3, 5, 10, 20 };
    public float[] ringHealthValues = new float[] { 1, 3, 5, 10, 20 };

    // ������
    public GameObject[] weaponPrefabs;
    public GameObject[] armorPrefabs;
    public GameObject[] ringPrefabs;

    // ����
    // �̺��� 
    public GameObject noneWeaponNomal; // �븻 ���� �̺���
    public GameObject noneWeaponRare; 
    public GameObject noneWeaponUnipue; 
    public GameObject noneWeaponLegend; 
    public GameObject noneWeaponEpic; 

    // ����
    public GameObject weaponNomal; // �븻 ����
    public GameObject weaponRare; 
    public GameObject weaponUnipue; 
    public GameObject weaponLegend; 
    public GameObject weaponEpic; 

    public int weaponNomalNum; // �븻 ���� ����
    public int weaponRareNum; 
    public int weaponUnipueNum; 
    public int weaponLegendNum; 
    public int weaponEpicNum;

    // ��
    // �̺��� 
    public GameObject noneArmorNomal; // �븻 �� �̺���
    public GameObject noneArmorRare;
    public GameObject noneArmorUnipue;
    public GameObject noneArmorLegend;
    public GameObject noneArmorEpic;

    // ����
    public GameObject armorNomal; // �븻 ��
    public GameObject armorRare;
    public GameObject armorUnipue;
    public GameObject armorLegend;
    public GameObject armorEpic;

    public int armorNomalNum; // �븻 �� ����
    public int armorRareNum;
    public int armorUnipueNum;
    public int armorLegendNum;
    public int armorEpicNum;


    // ����
    // �̺��� 
    public GameObject noneRingNomal; // �븻 ���� �̺���
    public GameObject noneRingRare;
    public GameObject noneRingUnipue;
    public GameObject noneRingLegend;
    public GameObject noneRingEpic;

    // ����
    public GameObject ringNomal; // �븻 ����
    public GameObject ringRare;
    public GameObject ringUnipue;
    public GameObject ringLegend;
    public GameObject ringEpic;

    public int ringNomalNum; // �븻 ���� ����
    public int ringRareNum;
    public int ringUnipueNum;
    public int ringLegendNum;
    public int ringEpicNum;


    // ���� ���
    public int currentWeapon; // ���� ���� ���� ����
    public int currentArmor; // ���� ���� ���� ��
    public int currentRing; // ���� ���� ���� ��
    private GameObject currentWeaponObj; // ���� ������ ���� ������Ʈ
    private GameObject currentArmorObj; // ���� ������ �� ������Ʈ
    private GameObject currentRingObj; // ���� ������ ���� ������Ʈ


    // ��� ���� ��ġ
    public GameObject currentWeaponPos;
    public GameObject currentArmorPos;
    public GameObject currentRingPos;

    // ��� ���� �ؽ�Ʈ
    public TMP_Text weaponNomalNumText;
    public TMP_Text weaponRareNumText;
    public TMP_Text weaponUniqueNumText;
    public TMP_Text weaponLegendNumText;
    public TMP_Text weaponEpicNumText;

    public TMP_Text armorNomalNumText;
    public TMP_Text armorRareNumText;
    public TMP_Text armorUniqueNumText;
    public TMP_Text armorLegendNumText;
    public TMP_Text armorEpicNumText;

    public TMP_Text ringNomalNumText;
    public TMP_Text ringRareNumText;
    public TMP_Text ringUniqueNumText;
    public TMP_Text ringLegendNumText;
    public TMP_Text ringEpicNumText;

    // ��� ȿ��
    public TMP_Text itemForceText;

    // �̿� ������Ʈ
    public GameObject miniGame;

    private void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        // ��� ���� �ҷ�����
        weaponNomalNum = PlayerPrefs.GetInt("weaponNomalNum", weaponNomalNum);
        weaponRareNum = PlayerPrefs.GetInt("weaponRareNum", weaponRareNum);
        weaponUnipueNum = PlayerPrefs.GetInt("weaponUnipueNum", weaponUnipueNum);
        weaponLegendNum = PlayerPrefs.GetInt("weaponLegendNum", weaponLegendNum);
        weaponEpicNum = PlayerPrefs.GetInt("weaponEpicNum", weaponEpicNum);

        armorNomalNum = PlayerPrefs.GetInt("armorNomalNum", armorNomalNum);
        armorRareNum = PlayerPrefs.GetInt("armorRareNum", armorRareNum);
        armorUnipueNum = PlayerPrefs.GetInt("armorUnipueNum", armorUnipueNum);
        armorLegendNum = PlayerPrefs.GetInt("armorLegendNum", armorLegendNum);
        armorEpicNum = PlayerPrefs.GetInt("armorEpicNum", armorEpicNum);

        ringNomalNum = PlayerPrefs.GetInt("ringNomalNum", ringNomalNum);
        ringRareNum = PlayerPrefs.GetInt("ringRareNum", ringRareNum);
        ringUnipueNum = PlayerPrefs.GetInt("ringUnipueNum", ringUnipueNum);
        ringLegendNum = PlayerPrefs.GetInt("ringLegendNum", ringLegendNum);
        ringEpicNum = PlayerPrefs.GetInt("ringEpicNum", ringEpicNum);

        // ��� ���� ���� �ҷ�����
        currentWeapon = PlayerPrefs.GetInt("currentWeapon", currentWeapon);
        switch (currentWeapon)
        {
            case 1:
                EquipWeapon(0);
                break;
            case 2:
                EquipWeapon(1);
                break;
            case 3:
                EquipWeapon(2);
                break;
            case 4:
                EquipWeapon(3);
                break;
            case 5:
                EquipWeapon(4);
                break;
        }

        currentArmor = PlayerPrefs.GetInt("currentArmor", currentArmor);
        switch (currentArmor)
        {
            case 1:
                EquipArmor(0);
                break;
            case 2:
                EquipArmor(1);
                break;
            case 3:
                EquipArmor(2);
                break;
            case 4:
                EquipArmor(3);
                break;
            case 5:
                EquipArmor(4);
                break;
        }

        currentRing = PlayerPrefs.GetInt("currentRing", currentRing);
        switch (currentRing)
        {
            case 1:
                EquipRing(0);
                break;
            case 2:
                EquipRing(1);
                break;
            case 3:
                EquipRing(2);
                break;
            case 4:
                EquipRing(3);
                break;
            case 5:
                EquipRing(4);
                break;
        }
    }


    private void OnApplicationPause(bool pauseStatus) // ������ �����ɶ� ������ ����
    {
        if (pauseStatus)
        {
            // ����
            PlayerPrefs.SetInt("weaponNomalNum", weaponNomalNum);
            PlayerPrefs.SetInt("weaponRareNum", weaponRareNum);
            PlayerPrefs.SetInt("weaponUnipueNum", weaponUnipueNum);
            PlayerPrefs.SetInt("weaponLegendNum", weaponLegendNum);
            PlayerPrefs.SetInt("weaponEpicNum", weaponEpicNum);

            // ��
            PlayerPrefs.SetInt("armorNomalNum", armorNomalNum);
            PlayerPrefs.SetInt("armorRareNum", armorRareNum);
            PlayerPrefs.SetInt("armorUnipueNum", armorUnipueNum);
            PlayerPrefs.SetInt("armorLegendNum", armorLegendNum);
            PlayerPrefs.SetInt("armorEpicNum", armorEpicNum);

            // ����
            PlayerPrefs.SetInt("ringNomalNum", ringNomalNum);
            PlayerPrefs.SetInt("ringRareNum", ringRareNum);
            PlayerPrefs.SetInt("ringUnipueNum", ringUnipueNum);
            PlayerPrefs.SetInt("ringLegendNum", ringLegendNum);
            PlayerPrefs.SetInt("ringEpicNum", ringEpicNum);

            // ���� �������� ���
            PlayerPrefs.SetInt("currentWeapon", currentWeapon);
            PlayerPrefs.SetInt("currentArmor", currentArmor);
            PlayerPrefs.SetInt("currentRing", currentRing);

            PlayerPrefs.Save();
        }
    }

    void OnApplicationQuit()
    {
        // ����
        PlayerPrefs.SetInt("weaponNomalNum", weaponNomalNum);
        PlayerPrefs.SetInt("weaponRareNum", weaponRareNum);
        PlayerPrefs.SetInt("weaponUnipueNum", weaponUnipueNum);
        PlayerPrefs.SetInt("weaponLegendNum", weaponLegendNum);
        PlayerPrefs.SetInt("weaponEpicNum", weaponEpicNum);

        // ��
        PlayerPrefs.SetInt("armorNomalNum", armorNomalNum);
        PlayerPrefs.SetInt("armorRareNum", armorRareNum);
        PlayerPrefs.SetInt("armorUnipueNum", armorUnipueNum);
        PlayerPrefs.SetInt("armorLegendNum", armorLegendNum);
        PlayerPrefs.SetInt("armorEpicNum", armorEpicNum);

        // ����
        PlayerPrefs.SetInt("ringNomalNum", ringNomalNum);
        PlayerPrefs.SetInt("ringRareNum", ringRareNum);
        PlayerPrefs.SetInt("ringUnipueNum", ringUnipueNum);
        PlayerPrefs.SetInt("ringLegendNum", ringLegendNum);
        PlayerPrefs.SetInt("ringEpicNum", ringEpicNum);

        // ���� �������� ���
        PlayerPrefs.SetInt("currentWeapon", currentWeapon);
        PlayerPrefs.SetInt("currentArmor", currentArmor);
        PlayerPrefs.SetInt("currentRing", currentRing);

        PlayerPrefs.Save();
    }


    void Update()
    {
        // ����
        UpdateItem(weaponNomalNum, noneWeaponNomal, weaponNomal);
        UpdateItem(weaponRareNum, noneWeaponRare, weaponRare);
        UpdateItem(weaponUnipueNum, noneWeaponUnipue, weaponUnipue);
        UpdateItem(weaponLegendNum, noneWeaponLegend, weaponLegend);
        UpdateItem(weaponEpicNum, noneWeaponEpic, weaponEpic);

        // ��
        UpdateItem(armorNomalNum, noneArmorNomal, armorNomal);
        UpdateItem(armorRareNum, noneArmorRare, armorRare);
        UpdateItem(armorUnipueNum, noneArmorUnipue, armorUnipue);
        UpdateItem(armorLegendNum, noneArmorLegend, armorLegend);
        UpdateItem(armorEpicNum, noneArmorEpic, armorEpic);

        // ����
        UpdateItem(ringNomalNum, noneRingNomal, ringNomal);
        UpdateItem(ringRareNum, noneRingRare, ringRare);
        UpdateItem(ringUnipueNum, noneRingUnipue, ringUnipue);
        UpdateItem(ringLegendNum, noneRingLegend, ringLegend);
        UpdateItem(ringEpicNum, noneRingEpic, ringEpic);


        // ���� ��� ����
        UpdateItemText(weaponNomalNum, weaponNomalNumText);
        UpdateItemText(weaponRareNum, weaponRareNumText);
        UpdateItemText(weaponUnipueNum, weaponUniqueNumText);
        UpdateItemText(weaponLegendNum, weaponLegendNumText);
        UpdateItemText(weaponEpicNum, weaponEpicNumText);

        UpdateItemText(armorNomalNum, armorNomalNumText);
        UpdateItemText(armorRareNum, armorRareNumText);
        UpdateItemText(armorUnipueNum, armorUniqueNumText);
        UpdateItemText(armorLegendNum, armorLegendNumText);
        UpdateItemText(armorEpicNum, armorEpicNumText);

        UpdateItemText(ringNomalNum, ringNomalNumText);
        UpdateItemText(ringRareNum, ringRareNumText);
        UpdateItemText(ringUnipueNum, ringUniqueNumText);
        UpdateItemText(ringLegendNum, ringLegendNumText);
        UpdateItemText(ringEpicNum, ringEpicNumText);

        // �������� ��� ȿ��
        itemForceText.text = "���� + " + weaponPower + "  �� + " + armorDefense + "  ���� + " + ringHealth;
    }

    // ��� ���� �ؽ�Ʈ ������Ʈ
    void UpdateItemText(int itemNum, TMP_Text itemNumText)
    {
        itemNumText.text = itemNum.ToString() + " / 100";

        if (itemNum >= 100)
        {
            itemNumText.color = Color.yellow;
        }
        else
        {
            itemNumText.color = Color.black;
        }
    }

    // ���� ���
    void UpdateItem(int itemNum, GameObject noneItem, GameObject item)
    {
        if (itemNum != 0)
        {
            noneItem.SetActive(false);
            item.SetActive(true);
        }
        else
        {
            noneItem.SetActive(true);
            item.SetActive(false);
        }
    }

    // ���� ����
    public void EquipWeapon(int weaponIndex)
    {
        audioManager.PlayUseItemSound();
        // ������ ������ ���Ⱑ ������ ����
        if (currentWeaponObj != null)
        {
            Destroy(currentWeaponObj);
        }

        // �� ���� ����
        currentWeaponObj = Instantiate(weaponPrefabs[weaponIndex], Vector3.zero, Quaternion.identity);
        currentWeaponObj.transform.SetParent(currentWeaponPos.transform, false);

        OnEquipWeapon(weaponIndex);
    }

    // ������ ���ݷ� ����
    public void OnEquipWeapon(int weaponIndex)
    {
        weaponPower = weaponPowerValues[weaponIndex];
        playerMovement.ChangeEquipment(weaponPower, 0, 0);
    }

    // �� ����
    void EquipArmor(int armorIndex)
    {
        audioManager.PlayUseItemSound();
        // ������ ������ ���� ������ ����
        if (currentArmorObj != null)
        {
            Destroy(currentArmorObj);
        }

        // �� �� ����
        currentArmorObj = Instantiate(armorPrefabs[armorIndex], Vector3.zero, Quaternion.identity);
        currentArmorObj.transform.SetParent(currentArmorPos.transform, false);

        OnEquipArmor(armorIndex);
    }

    // ���� ���� ����
    public void OnEquipArmor(int armorIndex)
    {
        armorDefense = armorDefenseValues[armorIndex];
        playerMovement.ChangeEquipment(0, armorDefense, 0);
    }

    // ���� ����
    void EquipRing(int ringIndex)
    {
        audioManager.PlayUseItemSound();
        // ������ ������ ������ ������ ����
        if (currentRingObj != null)
        {
            Destroy(currentRingObj);
        }

        // �� ���� ����
        currentRingObj = Instantiate(ringPrefabs[ringIndex], Vector3.zero, Quaternion.identity);
        currentRingObj.transform.SetParent(currentRingPos.transform, false);


        OnEquipRing(ringIndex);
    }

    // ������ ü�� ����
    public void OnEquipRing(int ringIndex)
    {
        ringHealth = ringHealthValues[ringIndex];
        playerMovement.ChangeEquipment(0, 0, ringHealth);
    }

    // ���� ���� ��ư
    public void NomalWeapon()
    {
        currentWeapon = 1;
        EquipWeapon(0);
    }
    public void RareWeapon()
    {
        currentWeapon = 2;
        EquipWeapon(1);
    }
    public void UniqueWeapon()
    {
        currentWeapon = 3;
        EquipWeapon(2);
    }
    public void LegendWeapon()
    {
        currentWeapon = 4;
        EquipWeapon(3);
    }
    public void EpicWeapon()
    {
        currentWeapon = 5;
        EquipWeapon(4);
    }

    // �� ����
    public void NomalArmor()
    {
        currentArmor = 1;
        EquipArmor(0);
    }
    public void RareArmor()
    {
        currentArmor = 2;
        EquipArmor(1);
    }
    public void UniqueArmor()
    {
        currentArmor = 3;
        EquipArmor(2);
    }
    public void LegendArmor()
    {
        currentArmor = 4;
        EquipArmor(3);
    }
    public void EpicArmor()
    {
        currentArmor = 5;
        EquipArmor(4);
    }

    // ���� ����
    public void NomalRing()
    {
        currentRing = 1;
        EquipRing(0);
    }
    public void RareRing()
    {
        currentRing = 2;
        EquipRing(1);
    }
    public void UniqueRing()
    {
        currentRing = 3;
        EquipRing(2);
    }
    public void LegendRing()
    {
        currentRing = 4;
        EquipRing(3);
    }
    public void EpicRing()
    {
        currentRing = 5;
        EquipRing(4);
    }

    // ��� �ռ� ��ư
    public void NextRareWeapon()
    {
        if (weaponNomalNum >= 100)
        {
            weaponNomalNum -= 100;
            weaponRareNum++;
        }
    }

    public void NextUniqueWeapon()
    {
        if (weaponRareNum >= 100)
        {
            weaponRareNum -= 100;
            weaponUnipueNum++;
        }
    }

    public void NextLegendWeapon()
    {
        if (weaponUnipueNum >= 100)
        {
            weaponUnipueNum -= 100;
            weaponLegendNum++;
        }
    }

    public void NextEpicWeapon()
    {
        if (weaponLegendNum >= 100)
        {
            weaponLegendNum -= 100;
            weaponEpicNum++;
        }
    }
    public void NextRareArmor()
    {
        if (armorNomalNum >= 100)
        {
            armorNomalNum -= 100;
            armorRareNum++;
        }
    }

    public void NextUniqueArmor()
    {
        if (armorRareNum >= 100)
        {
            armorRareNum -= 100;
            armorUnipueNum++;
        }
    }

    public void NextLegendArmor()
    {
        if (armorUnipueNum >= 100)
        {
            armorUnipueNum -= 100;
            armorLegendNum++;
        }
    }

    public void NextEpicArmor()
    {
        if (armorLegendNum >= 100)
        {
            armorLegendNum -= 100;
            armorEpicNum++;
        }
    }

    public void NextRareRing()
    {
        if (ringNomalNum >= 100)
        {
            ringNomalNum -= 100;
            ringRareNum++;
        }
    }

    public void NextUniqueRing()
    {
        if (ringRareNum >= 100)
        {
            ringRareNum -= 100;
            ringUnipueNum++;
        }
    }

    public void NextLegendRing()
    {
        if (ringUnipueNum >= 100)
        {
            ringUnipueNum -= 100;
            ringLegendNum++;
        }
    }

    public void NextEpicRing()
    {
        if (ringLegendNum >= 100)
        {
            ringLegendNum -= 100;
            ringEpicNum++;
        }
    }

    // ���â �ݱ�
    public void ExitInventory()
    {
        inventory.SetActive(false);
        miniGame.SetActive(true);
    }
}
