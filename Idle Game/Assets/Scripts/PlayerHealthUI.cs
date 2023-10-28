using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    private PlayerMovement playerMovement;

    // 체력 슬라이더
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
        float healthRatio = playerMovement.currentHealth / playerMovement.maxHealth; // 현재 체력과 최대 체력 비율 계산

        slHP.value = healthRatio; // 슬라이더 값을 체력 비율로 설정

        if (healthRatio > 0)
        {
            transform.Find("Fill Area").gameObject.SetActive(true);
        }
        else
            transform.Find("Fill Area").gameObject.SetActive(false);
    }
}
