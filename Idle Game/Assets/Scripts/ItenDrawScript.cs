using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItenDrawScript : MonoBehaviour
{
    private InventoryScript inventoryScript;
    private StoreScript storeScript;

    // 공용

    // 프리팹
    public GameObject[] weapon; // storeScript.itmeNum이 1일떄
    public GameObject[] armor; // storeScript.itmeNum이 2일떄
    public GameObject[] ring; // storeScript.itmeNum이 3일떄

    // 확률 변수
    public float nomal; // 70%
    public float rare; // 20%
    public float unique; // 7%
    public float Legend; // 2.5%
    public float epic; // 0.5%

    // 1뽑
    // 생성위치
    public GameObject oneDrawPoint;

    // 10뽑
    // 생성위치
    public GameObject[] TenDrawPoint;

    // 생성된 아이템 저장
    private List<GameObject> createdItems = new List<GameObject>();

    void Start()
    {
        inventoryScript = GameObject.Find("ItemManager").GetComponent<InventoryScript>();
        storeScript = GameObject.Find("ItemManager").GetComponent<StoreScript>();
        
        // 확률 변수에 누적 확률 할당
        nomal = 0.7f; // 전체 70%
        rare = 0.9f; // 전체 90%
        unique = 0.97f; // 전체 97%
        Legend = 0.995f; // 전체 99.5%
        epic = 1.0f; // 전체 100%
    }

    public void OneDraw()
    {
        // 확률에 따른 랜덤 아이템 선택
        float randomValue = Random.value; // 0.0f ~ 1.0f 사이의 랜덤값

        GameObject selectedItem = null;
        GameObject[] items;
        int itemIndex = 0; // 아이템 인덱스 추가


        // storeScript.itemNum 값에 따라 아이템 타입 선택
        switch (storeScript.itemNum)
        {
            case 1:
                items = weapon;
                break;
            case 2:
                items = armor;
                break;
            case 3:
                items = ring;
                break;
            default:
                return;
        }

        if (randomValue < nomal) // 70% 확률
        {
            selectedItem = items[0]; // 노말 아이템 선택
            itemIndex = 0;
        }
        else if (randomValue < rare) // 20% 확률
        {
            selectedItem = items[1]; // 레어 아이템 선택
            itemIndex = 1;
        }
        else if (randomValue < unique) // 7% 확률
        {
            selectedItem = items[2]; // 유니크 아이템 선택
            itemIndex = 2;
        }
        else if (randomValue < Legend) // 2.5% 확률
        {
            selectedItem = items[3]; // 레전드 아이템 선택
            itemIndex = 3;
        }
        else // 0.5% 확률
        {
            selectedItem = items[4]; // 에픽 아이템 선택
            itemIndex = 4;
        }

        // 아이템 생성
        GameObject createdItem = Instantiate(selectedItem, Vector3.zero, Quaternion.identity);
        createdItem.transform.SetParent(oneDrawPoint.transform, false);
        createdItem.transform.localPosition = Vector3.zero;
        createdItems.Add(createdItem); // 리스트에 아이템 추가

        UpdateInventory(itemIndex); // 추가: 인벤토리 업데이트
    }


    public void TenDraw()
    {
        // 아이템을 10번 생성
        for (int i = 0; i < 10; i++)
        {
            // 확률에 따른 랜덤 아이템 선택
            float randomValue = Random.value; // 0.0f ~ 1.0f 사이의 랜덤값

            GameObject selectedItem = null;
            GameObject[] items;
            int itemIndex = 0; // 아이템 인덱스 추가

            // storeScript.itemNum 값에 따라 아이템 타입 선택
            switch (storeScript.itemNum)
            {
                case 1:
                    items = weapon;
                    break;
                case 2:
                    items = armor;
                    break;
                case 3:
                    items = ring;
                    break;
                default:
                    return;
            }

            if (randomValue < nomal) // 70% 확률
            {
                selectedItem = items[0]; // 노말 아이템 선택
                itemIndex = 0;
            }
            else if (randomValue < rare) // 20% 확률
            {
                selectedItem = items[1]; // 레어 아이템 선택
                itemIndex = 1;
            }
            else if (randomValue < unique) // 7% 확률
            {
                selectedItem = items[2]; // 유니크 아이템 선택
                itemIndex = 2;
            }
            else if (randomValue < Legend) // 2.5% 확률
            {
                selectedItem = items[3]; // 레전드 아이템 선택
                itemIndex = 3;
            }
            else // 0.5% 확률
            {
                selectedItem = items[4]; // 에픽 아이템 선택
                itemIndex = 4;
            }

            // 아이템 생성
            GameObject createdItem = Instantiate(selectedItem, Vector3.zero, Quaternion.identity);
            createdItem.transform.SetParent(TenDrawPoint[i].transform, false);
            createdItem.transform.localPosition = Vector3.zero;
            createdItems.Add(createdItem); // 리스트에 아이템 추가

            UpdateInventory(itemIndex); // 추가: 인벤토리 업데이트
        }
    }

    void UpdateInventory(int itemIndex) // 획득한 장비 장비창에 적용
    {
        switch (storeScript.itemNum)
        {
            case 1: // 무기
                switch (itemIndex)
                {
                    case 0: inventoryScript.weaponNomalNum++; break;
                    case 1: inventoryScript.weaponRareNum++; break;
                    case 2: inventoryScript.weaponUnipueNum++; break;
                    case 3: inventoryScript.weaponLegendNum++; break;
                    case 4: inventoryScript.weaponEpicNum++; break;
                }
                break;
            case 2: // 방어구
                switch (itemIndex)
                {
                    case 0: inventoryScript.armorNomalNum++; break;
                    case 1: inventoryScript.armorRareNum++; break;
                    case 2: inventoryScript.armorUnipueNum++; break;
                    case 3: inventoryScript.armorLegendNum++; break;
                    case 4: inventoryScript.armorEpicNum++; break;
                }
                break;
            case 3: // 반지
                switch (itemIndex)
                {
                    case 0: inventoryScript.ringNomalNum++; break;
                    case 1: inventoryScript.ringRareNum++; break;
                    case 2: inventoryScript.ringUnipueNum++; break;
                    case 3: inventoryScript.ringLegendNum++; break;
                    case 4: inventoryScript.ringEpicNum++; break;
                }
                break;
        }
    }

    // 뽑기 후 상점으로 돌아가기
    public void ShopScreen()
{
    // 리스트의 모든 아이템 파괴
    foreach (GameObject item in createdItems)
    {
        if (item != null)
        {
            Destroy(item);
        }
    }
    // 리스트 비우기
    createdItems.Clear();

    storeScript.shop.SetActive(true);
    storeScript.oneDraw.SetActive(false);
    storeScript.TenDraw.SetActive(false);
}
}
