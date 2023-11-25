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

    // 공용
    public GameObject inventory; // 장비창

    // 장비별 수치 설정
    public float weaponPower; // 무기 공격력
    public float armorDefense; // 방어구 방어력
    public float ringHealth; // 반지 체력
    public float[] weaponPowerValues = new float[] { 1, 3, 5, 10, 20 };
    public float[] armorDefenseValues = new float[] { 1, 3, 5, 10, 20 };
    public float[] ringHealthValues = new float[] { 1, 3, 5, 10, 20 };

    // 프리팹
    public GameObject[] weaponPrefabs;
    public GameObject[] armorPrefabs;
    public GameObject[] ringPrefabs;

    // 무기
    // 미보유 
    public GameObject noneWeaponNomal; // 노말 무기 미보유
    public GameObject noneWeaponRare; 
    public GameObject noneWeaponUnipue; 
    public GameObject noneWeaponLegend; 
    public GameObject noneWeaponEpic; 

    // 보유
    public GameObject weaponNomal; // 노말 무기
    public GameObject weaponRare; 
    public GameObject weaponUnipue; 
    public GameObject weaponLegend; 
    public GameObject weaponEpic; 

    public int weaponNomalNum; // 노말 무기 개수
    public int weaponRareNum; 
    public int weaponUnipueNum; 
    public int weaponLegendNum; 
    public int weaponEpicNum;

    // 방어구
    // 미보유 
    public GameObject noneArmorNomal; // 노말 방어구 미보유
    public GameObject noneArmorRare;
    public GameObject noneArmorUnipue;
    public GameObject noneArmorLegend;
    public GameObject noneArmorEpic;

    // 보유
    public GameObject armorNomal; // 노말 방어구
    public GameObject armorRare;
    public GameObject armorUnipue;
    public GameObject armorLegend;
    public GameObject armorEpic;

    public int armorNomalNum; // 노말 방어구 개수
    public int armorRareNum;
    public int armorUnipueNum;
    public int armorLegendNum;
    public int armorEpicNum;


    // 반지
    // 미보유 
    public GameObject noneRingNomal; // 노말 반지 미보유
    public GameObject noneRingRare;
    public GameObject noneRingUnipue;
    public GameObject noneRingLegend;
    public GameObject noneRingEpic;

    // 보유
    public GameObject ringNomal; // 노말 반지
    public GameObject ringRare;
    public GameObject ringUnipue;
    public GameObject ringLegend;
    public GameObject ringEpic;

    public int ringNomalNum; // 노말 반지 개수
    public int ringRareNum;
    public int ringUnipueNum;
    public int ringLegendNum;
    public int ringEpicNum;


    // 장착 장비
    public int currentWeapon; // 현재 장착 중인 무기
    public int currentArmor; // 현재 장착 중인 방어구
    public int currentRing; // 현재 장착 중인 방어구
    private GameObject currentWeaponObj; // 현재 장착된 무기 오브젝트
    private GameObject currentArmorObj; // 현재 장착된 방어구 오브젝트
    private GameObject currentRingObj; // 현재 장착된 반지 오브젝트


    // 장비 장착 위치
    public GameObject currentWeaponPos;
    public GameObject currentArmorPos;
    public GameObject currentRingPos;

    // 장비 개수 텍스트
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

    // 장비 효과
    public TMP_Text itemForceText;

    // 이외 오브젝트
    public GameObject miniGame;

    private void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        // 장비 개수 불러오기
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

        // 장비 장착 정보 불러오기
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


    private void OnApplicationPause(bool pauseStatus) // 어플이 정지될때 데이터 저장
    {
        if (pauseStatus)
        {
            // 무기
            PlayerPrefs.SetInt("weaponNomalNum", weaponNomalNum);
            PlayerPrefs.SetInt("weaponRareNum", weaponRareNum);
            PlayerPrefs.SetInt("weaponUnipueNum", weaponUnipueNum);
            PlayerPrefs.SetInt("weaponLegendNum", weaponLegendNum);
            PlayerPrefs.SetInt("weaponEpicNum", weaponEpicNum);

            // 방어구
            PlayerPrefs.SetInt("armorNomalNum", armorNomalNum);
            PlayerPrefs.SetInt("armorRareNum", armorRareNum);
            PlayerPrefs.SetInt("armorUnipueNum", armorUnipueNum);
            PlayerPrefs.SetInt("armorLegendNum", armorLegendNum);
            PlayerPrefs.SetInt("armorEpicNum", armorEpicNum);

            // 반지
            PlayerPrefs.SetInt("ringNomalNum", ringNomalNum);
            PlayerPrefs.SetInt("ringRareNum", ringRareNum);
            PlayerPrefs.SetInt("ringUnipueNum", ringUnipueNum);
            PlayerPrefs.SetInt("ringLegendNum", ringLegendNum);
            PlayerPrefs.SetInt("ringEpicNum", ringEpicNum);

            // 현재 장착중인 장비
            PlayerPrefs.SetInt("currentWeapon", currentWeapon);
            PlayerPrefs.SetInt("currentArmor", currentArmor);
            PlayerPrefs.SetInt("currentRing", currentRing);

            PlayerPrefs.Save();
        }
    }

    void OnApplicationQuit()
    {
        // 무기
        PlayerPrefs.SetInt("weaponNomalNum", weaponNomalNum);
        PlayerPrefs.SetInt("weaponRareNum", weaponRareNum);
        PlayerPrefs.SetInt("weaponUnipueNum", weaponUnipueNum);
        PlayerPrefs.SetInt("weaponLegendNum", weaponLegendNum);
        PlayerPrefs.SetInt("weaponEpicNum", weaponEpicNum);

        // 방어구
        PlayerPrefs.SetInt("armorNomalNum", armorNomalNum);
        PlayerPrefs.SetInt("armorRareNum", armorRareNum);
        PlayerPrefs.SetInt("armorUnipueNum", armorUnipueNum);
        PlayerPrefs.SetInt("armorLegendNum", armorLegendNum);
        PlayerPrefs.SetInt("armorEpicNum", armorEpicNum);

        // 반지
        PlayerPrefs.SetInt("ringNomalNum", ringNomalNum);
        PlayerPrefs.SetInt("ringRareNum", ringRareNum);
        PlayerPrefs.SetInt("ringUnipueNum", ringUnipueNum);
        PlayerPrefs.SetInt("ringLegendNum", ringLegendNum);
        PlayerPrefs.SetInt("ringEpicNum", ringEpicNum);

        // 현재 장착중인 장비
        PlayerPrefs.SetInt("currentWeapon", currentWeapon);
        PlayerPrefs.SetInt("currentArmor", currentArmor);
        PlayerPrefs.SetInt("currentRing", currentRing);

        PlayerPrefs.Save();
    }


    void Update()
    {
        // 무기
        UpdateItem(weaponNomalNum, noneWeaponNomal, weaponNomal);
        UpdateItem(weaponRareNum, noneWeaponRare, weaponRare);
        UpdateItem(weaponUnipueNum, noneWeaponUnipue, weaponUnipue);
        UpdateItem(weaponLegendNum, noneWeaponLegend, weaponLegend);
        UpdateItem(weaponEpicNum, noneWeaponEpic, weaponEpic);

        // 방어구
        UpdateItem(armorNomalNum, noneArmorNomal, armorNomal);
        UpdateItem(armorRareNum, noneArmorRare, armorRare);
        UpdateItem(armorUnipueNum, noneArmorUnipue, armorUnipue);
        UpdateItem(armorLegendNum, noneArmorLegend, armorLegend);
        UpdateItem(armorEpicNum, noneArmorEpic, armorEpic);

        // 반지
        UpdateItem(ringNomalNum, noneRingNomal, ringNomal);
        UpdateItem(ringRareNum, noneRingRare, ringRare);
        UpdateItem(ringUnipueNum, noneRingUnipue, ringUnipue);
        UpdateItem(ringLegendNum, noneRingLegend, ringLegend);
        UpdateItem(ringEpicNum, noneRingEpic, ringEpic);


        // 현재 장비 개수
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

        // 장착중인 장비 효과
        itemForceText.text = "무기 + " + weaponPower + "  방어구 + " + armorDefense + "  반지 + " + ringHealth;
    }

    // 장비 개수 텍스트 업데이트
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

    // 보유 장비
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

    // 무기 장착
    public void EquipWeapon(int weaponIndex)
    {
        audioManager.PlayUseItemSound();
        // 이전에 장착된 무기가 있으면 제거
        if (currentWeaponObj != null)
        {
            Destroy(currentWeaponObj);
        }

        // 새 무기 생성
        currentWeaponObj = Instantiate(weaponPrefabs[weaponIndex], Vector3.zero, Quaternion.identity);
        currentWeaponObj.transform.SetParent(currentWeaponPos.transform, false);

        OnEquipWeapon(weaponIndex);
    }

    // 무기의 공격력 설정
    public void OnEquipWeapon(int weaponIndex)
    {
        weaponPower = weaponPowerValues[weaponIndex];
        playerMovement.ChangeEquipment(weaponPower, 0, 0);
    }

    // 방어구 장착
    void EquipArmor(int armorIndex)
    {
        audioManager.PlayUseItemSound();
        // 이전에 장착된 방어구가 있으면 제거
        if (currentArmorObj != null)
        {
            Destroy(currentArmorObj);
        }

        // 새 방어구 생성
        currentArmorObj = Instantiate(armorPrefabs[armorIndex], Vector3.zero, Quaternion.identity);
        currentArmorObj.transform.SetParent(currentArmorPos.transform, false);

        OnEquipArmor(armorIndex);
    }

    // 방어구의 방어력 설정
    public void OnEquipArmor(int armorIndex)
    {
        armorDefense = armorDefenseValues[armorIndex];
        playerMovement.ChangeEquipment(0, armorDefense, 0);
    }

    // 반지 장착
    void EquipRing(int ringIndex)
    {
        audioManager.PlayUseItemSound();
        // 이전에 장착된 반지가 있으면 제거
        if (currentRingObj != null)
        {
            Destroy(currentRingObj);
        }

        // 새 반지 생성
        currentRingObj = Instantiate(ringPrefabs[ringIndex], Vector3.zero, Quaternion.identity);
        currentRingObj.transform.SetParent(currentRingPos.transform, false);


        OnEquipRing(ringIndex);
    }

    // 반지의 체력 설정
    public void OnEquipRing(int ringIndex)
    {
        ringHealth = ringHealthValues[ringIndex];
        playerMovement.ChangeEquipment(0, 0, ringHealth);
    }

    // 무기 장착 버튼
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

    // 방어구 장착
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

    // 반지 장착
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

    // 장비 합성 버튼
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

    // 장비창 닫기
    public void ExitInventory()
    {
        inventory.SetActive(false);
        miniGame.SetActive(true);
    }
}
