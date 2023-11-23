using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusMenu : MonoBehaviour
{
    private PlayerMovement playerMovement;

    // 스탯창
    public GameObject status;

    // 스탯
    public TMP_Text healthStatus;
    public TMP_Text powerStatus;
    public TMP_Text defenseStatus;

    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    
    void Update()
    {
        healthStatus.text = "최대 체력 : " + playerMovement.maxHealth.ToString();
        powerStatus.text = "공 격 력 : " + playerMovement.power.ToString();
        defenseStatus.text = "방 어 력 : " + playerMovement.defense.ToString();
    }

    public void OnStatus()
    {
        status.SetActive(!status.activeSelf);
    } 
}
