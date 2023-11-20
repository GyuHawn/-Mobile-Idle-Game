using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartMeun : MonoBehaviour
{
    public TMP_Text text1;
    public TMP_Text text2;

    public float minSize1 = 180f;
    public float maxSize1 = 210f;
    public float minSize2 = 250f;
    public float maxSize2 = 280f;
    public float sizeChangeSpeed = 0.05f;

    private void Update()
    {
        // 텍스트1의 크기 변경
        float newSize1 = Mathf.PingPong(Time.time * sizeChangeSpeed, maxSize1 - minSize1) + minSize1;
        text1.fontSize = newSize1;

        // 텍스트2의 크기 변경
        float newSize2 = Mathf.PingPong(Time.time * sizeChangeSpeed, maxSize2 - minSize2) + minSize2;
        text2.fontSize = newSize2;
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Main");
    }
}
