using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    private MonsterScript monsterScript;

    public float moveSpd;
    public float transparSpd;
    public float destroyTime;
    public float damage;

    TextMeshPro text;
    Color transpar;

    void Start()
    {
        text = GetComponent<TextMeshPro>();

        transpar = text.color;
        text.text = damage.ToString();

        Invoke("DestroyObj", destroyTime);
    }

    void Update()
    {
        transform.Translate(new Vector3(0, moveSpd * Time.deltaTime, 0));
        transpar.a = Mathf.Lerp(transpar.a, 0, Time.deltaTime * transparSpd);
        text.color = transpar;
    }

    private void DestroyObj()
    {
        Destroy(gameObject);
    }
}
