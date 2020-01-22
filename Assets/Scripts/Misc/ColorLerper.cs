using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerper : MonoBehaviour
{
    public VarColor color1;
    public VarColor color2;

    public float speed = 1.0f;
    private float startTime;

    private SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        startTime = Time.time;
        StartCoroutine(LerpColor2());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator LerpColor2()
    {
        while(rend.color != color2.value)
        {
            float t = (Time.time - startTime) * speed;
            rend.color = Color.Lerp(color1.value, color2.value, t);
            yield return null;
        }
        startTime = Time.time;
        StartCoroutine(LerpColor1());
    }

    IEnumerator LerpColor1()
    {
        while (rend.color != color1.value)
        {
            float t = (Time.time - startTime) * speed;
            rend.color = Color.Lerp(color2.value, color1.value, t);
            yield return null;
        }
        startTime = Time.time;
        StartCoroutine(LerpColor2());
    }
}
