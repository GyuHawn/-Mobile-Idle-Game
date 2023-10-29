using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FunctionUI : MonoBehaviour
{
    public GameObject upgradeMenu;

    public void OnUpgrade()
    {
        upgradeMenu.SetActive(!upgradeMenu.activeSelf);
    }
}
