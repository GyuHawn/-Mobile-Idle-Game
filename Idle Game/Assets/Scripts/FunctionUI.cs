using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FunctionUI : MonoBehaviour
{
    public GameObject upgradeMenu;
    public GameObject shopMenu;
    public GameObject inventoryMenu;
    public GameObject miniGameMenu;

    public void OnUpgrade()
    {
        upgradeMenu.SetActive(!upgradeMenu.activeSelf);
    }
    public void OnShop()
    {
        shopMenu.SetActive(!shopMenu.activeSelf);
    }
    public void OnInventory()
    {
        inventoryMenu.SetActive(!inventoryMenu.activeSelf);
    }
    public void OnMiniGameMenu()
    {
        miniGameMenu.SetActive(!miniGameMenu.activeSelf);
    }
}
