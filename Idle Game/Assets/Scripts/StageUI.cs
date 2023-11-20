using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageUI : MonoBehaviour
{
    private StageManager stageManger;

    public TMP_Text currentStage; // 현재 스테이지
    public TMP_Text currentMonsterNum; // 몬스터 수
    

    // 슬라이더
    Slider slHP;

    void Start()
    {
        stageManger = GameObject.Find("Manager").GetComponent<StageManager>();
        slHP = GetComponent<Slider>();
    }


    void Update()
    {
        currentStage.text = "스테이지" + stageManger.stage;
        currentMonsterNum.text = stageManger.deadMonster.ToString() + " / 10"; 
        zeroSlider();
    }

    void zeroSlider()
    {
        slHP.value = ((float)stageManger.deadMonster / 10);
        if (slHP.value > 0)
        {
            transform.Find("Fill Area").gameObject.SetActive(true);
        }
        else
            transform.Find("Fill Area").gameObject.SetActive(false);
    }
}
