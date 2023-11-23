using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusMenu : MonoBehaviour
{
    private PlayerMovement playerMovement;

    // ����â
    public GameObject status;

    // ����
    public TMP_Text healthStatus;
    public TMP_Text powerStatus;
    public TMP_Text defenseStatus;

    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    
    void Update()
    {
        healthStatus.text = "�ִ� ü�� : " + playerMovement.maxHealth.ToString();
        powerStatus.text = "�� �� �� : " + playerMovement.power.ToString();
        defenseStatus.text = "�� �� �� : " + playerMovement.defense.ToString();
    }

    public void OnStatus()
    {
        status.SetActive(!status.activeSelf);
    } 
}
