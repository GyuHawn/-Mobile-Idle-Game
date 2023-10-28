using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    private PlayerMovement playerMovement;

    // ü�� �����̴�
    Slider slHP;

    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        slHP = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        zeroSlider();
    }

    void zeroSlider()
    {
        float healthRatio = playerMovement.currentHealth / playerMovement.maxHealth; // ���� ü�°� �ִ� ü�� ���� ���

        slHP.value = healthRatio; // �����̴� ���� ü�� ������ ����

        if (healthRatio > 0)
        {
            transform.Find("Fill Area").gameObject.SetActive(true);
        }
        else
            transform.Find("Fill Area").gameObject.SetActive(false);
    }
}
