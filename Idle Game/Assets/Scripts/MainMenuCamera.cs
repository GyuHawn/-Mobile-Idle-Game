using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    public Vector2 startMove;
    public Vector2 endMove;
    public float spd;

    private void Start()
    {
        startMove = new Vector2(-18, 8);
        endMove = new Vector2(35, 8);
        spd = 0.02f;
    }

    private void Update()
    {
        transform.position += new Vector3(spd, 0, 0);

        if(transform.position.x >= endMove.x)
        {
            transform.position = startMove;
        }
    }
}
