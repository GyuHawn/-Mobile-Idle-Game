using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FunctionUI : MonoBehaviour
{
    public GameObject upgradeMenu;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnUpgrade()
    {
        upgradeMenu.SetActive(!upgradeMenu.activeSelf);
    }
}
