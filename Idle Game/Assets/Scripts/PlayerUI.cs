using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public TMP_Text currentMoney;
    public TMP_Text currentLevel;
    public TMP_Text currentTotalPower;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    
    void Update()
    {
        currentMoney.text = playerMovement.money.ToString();
        currentLevel.text = "Lv. " + playerMovement.level.ToString();
        currentTotalPower.text = "Power. " + playerMovement.totalPower.ToString();  
    }
}
