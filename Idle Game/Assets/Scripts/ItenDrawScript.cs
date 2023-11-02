using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItenDrawScript : MonoBehaviour
{
    private StoreScript storeScript;

    // ����

    // ������
    public GameObject[] weapon; // storeScript.itmeNum�� 1�ϋ�
    public GameObject[] armor; // storeScript.itmeNum�� 2�ϋ�
    public GameObject[] ring; // storeScript.itmeNum�� 3�ϋ�

    // Ȯ�� ����
    public float nomal; // 70%
    public float rare; // 20%
    public float unique; // 7%
    public float Legend; // 2.5%
    public float epic; // 0.5%

    // 1��
    // ������ġ
    public GameObject oneDrawPoint;

    // 10��
    // ������ġ
    public GameObject[] TenDrawPoint;

    // ������ ������ ����
    private List<GameObject> createdItems = new List<GameObject>();

    void Start()
    {
        storeScript = GameObject.Find("Manager").GetComponent<StoreScript>();
        
        // Ȯ�� ������ ���� Ȯ�� �Ҵ�
        nomal = 0.7f; // ��ü 70%
        rare = 0.9f; // ��ü 90%
        unique = 0.97f; // ��ü 97%
        Legend = 0.995f; // ��ü 99.5%
        epic = 1.0f; // ��ü 100%
    }

    
    void Update()
    {
        
    }

    public void OneDraw()
    {
        // Ȯ���� ���� ���� ������ ����
        float randomValue = Random.value; // 0.0f ~ 1.0f ������ ������

        GameObject selectedItem = null;
        GameObject[] items;

        // storeScript.itemNum ���� ���� ������ Ÿ�� ����
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

        if (randomValue < nomal) // 70% Ȯ��
        {
            selectedItem = items[0]; // �븻 ������ ����
        }
        else if (randomValue < rare) // 20% Ȯ��
        {
            selectedItem = items[1]; // ���� ������ ����
        }
        else if (randomValue < unique) // 7% Ȯ��
        {
            selectedItem = items[2]; // ����ũ ������ ����
        }
        else if (randomValue < Legend) // 2.5% Ȯ��
        {
            selectedItem = items[3]; // ������ ������ ����
        }
        else // 0.5% Ȯ��
        {
            selectedItem = items[4]; // ���� ������ ����
        }

        // ������ ����
        GameObject createdItem = Instantiate(selectedItem, Vector3.zero, Quaternion.identity);
        createdItem.transform.SetParent(oneDrawPoint.transform, false);
        createdItem.transform.localPosition = Vector3.zero;
        createdItems.Add(createdItem); // ����Ʈ�� ������ �߰�
    }


    public void TenDraw()
    {
        // �������� 10�� ����
        for (int i = 0; i < 10; i++)
        {
            // Ȯ���� ���� ���� ������ ����
            float randomValue = Random.value; // 0.0f ~ 1.0f ������ ������

            GameObject selectedItem = null;
            GameObject[] items;

            // storeScript.itemNum ���� ���� ������ Ÿ�� ����
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

            Debug.Log(randomValue);
            if (randomValue < nomal) // 70% Ȯ��
            {
                selectedItem = items[0]; // �븻 ������ ����
            }
            else if (randomValue < rare) // 20% Ȯ��
            {
                selectedItem = items[1]; // ���� ������ ����
            }
            else if (randomValue < unique) // 7% Ȯ��
            {
                selectedItem = items[2]; // ����ũ ������ ����
            }
            else if (randomValue < Legend) // 2.5% Ȯ��
            {
                selectedItem = items[3]; // ������ ������ ����
            }
            else // 0.5% Ȯ��
            {
                selectedItem = items[4]; // ���� ������ ����
            }

            // ������ ����
            GameObject createdItem = Instantiate(selectedItem, Vector3.zero, Quaternion.identity);
            createdItem.transform.SetParent(TenDrawPoint[i].transform, false);
            createdItem.transform.localPosition = Vector3.zero;
            createdItems.Add(createdItem); // ����Ʈ�� ������ �߰�
        }
    }


    // �̱� �� �������� ���ư���
    public void ShopScreen()
{
    // ����Ʈ�� ��� ������ �ı�
    foreach (GameObject item in createdItems)
    {
        if (item != null)
        {
            Destroy(item);
        }
    }
    // ����Ʈ ����
    createdItems.Clear();

    storeScript.shop.SetActive(true);
    storeScript.oneDraw.SetActive(false);
    storeScript.TenDraw.SetActive(false);
}
}
