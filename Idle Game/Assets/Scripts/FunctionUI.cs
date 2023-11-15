using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FunctionUI : MonoBehaviour
{
    private AudioManager audioManager;

    public GameObject upgradeMenu;
    public GameObject shopMenu;
    public GameObject inventoryMenu;
    public GameObject miniGameMenu;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    public void OnUpgrade()
    {
        audioManager.PlayButtonSound();
        upgradeMenu.SetActive(!upgradeMenu.activeSelf);
    }
    public void OnShop()
    {
        audioManager.PlayButtonSound();
        shopMenu.SetActive(!shopMenu.activeSelf);
    }
    public void OnInventory()
    {
        audioManager.PlayButtonSound();
        inventoryMenu.SetActive(!inventoryMenu.activeSelf);
    }
    public void OnMiniGameMenu()
    {
        audioManager.PlayButtonSound();
        miniGameMenu.SetActive(!miniGameMenu.activeSelf);
    }
}
