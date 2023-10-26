using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveMent : MonoBehaviour
{
    public GameObject target;

    private void Start()
    {
        target = GameObject.Find("Player");
    }

    void Update()
    {
        Vector3 targetPosition = target.transform.position;
        targetPosition.z = transform.position.z;

        transform.position = targetPosition;
    }
}