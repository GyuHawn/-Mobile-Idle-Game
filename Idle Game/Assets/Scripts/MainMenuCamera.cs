using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class MainMenuCamera : MonoBehaviour
{
    public Vector2 startMove;
    public Vector2 endMove;
    public float spd;

    private void Start()
    {
        startMove = new Vector2(-14, 8);
        endMove = new Vector2(29.5f, 8);
        spd = 0.01f;
    }

    private void Update()
    {
        transform.position += new Vector3(spd, 0, 0);

        if (transform.position.x >= endMove.x)
        {
            transform.position = startMove;
        }
    }
}
