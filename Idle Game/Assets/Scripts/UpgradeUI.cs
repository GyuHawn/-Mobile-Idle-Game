using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public TMP_Text HealthUpMoney;
    public TMP_Text HealthLevel;
    public TMP_Text PowerUpMoney;
    public TMP_Text PowerLevel;
    public TMP_Text DefenseUpMoney;
    public TMP_Text DefenseLevel;

    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

   
    void Update()
    {
        HealthUpMoney.text = (10 * playerMovement.upgradeHealth).ToString();
        HealthLevel.text = playerMovement.upgradeHealth.ToString();

        PowerUpMoney.text = (10 * playerMovement.upgradePower).ToString();
        PowerLevel.text = playerMovement.upgradePower.ToString();

        DefenseUpMoney.text = (10 * playerMovement.upgradeDefense).ToString();
        DefenseLevel.text = playerMovement.upgradeDefense.ToString();
    }

    public void HealthUP()
    {
        int cost = (10 * (playerMovement.upgradeHealth));
        if (playerMovement.money >= cost)
        {
            playerMovement.upgradeHealth++;
            playerMovement.SetStats();
            playerMovement.money -= cost;
        }
        else
        {
            Debug.Log("µ∑∫Œ¡∑");
        }
    }

    public void PowerUP()
    {
        int cost = (10 * (playerMovement.upgradePower));
        if (playerMovement.money >= cost)
        {
            playerMovement.upgradePower++;
            playerMovement.SetStats();
            playerMovement.money -= cost;
        }
        else
        {
            Debug.Log("µ∑∫Œ¡∑");
        }
    }

    public void DefenseUP()
    {
        int cost = (10 * (playerMovement.upgradeDefense));
        if (playerMovement.money >= cost)
        {
            playerMovement.upgradeDefense++;
            playerMovement.SetStats();
            playerMovement.money -= cost;
        }
        else
        {
            Debug.Log("µ∑∫Œ¡∑");
        }
    }
}
