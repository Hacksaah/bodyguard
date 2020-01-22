using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFastFall : MonoBehaviour
{
    public float shakeDuration = 0.3f;

    public VarInt moveDir;

    private float shakeTimer = 0f;

    private float dampingSpeed = 2.0f;

    Vector3 initialPos;
    float xOffset;


    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
    }

    public void RunCameraShake()
    {
        shakeTimer = shakeDuration;
        if (moveDir.value > 0)
            xOffset = -0.015f;
        else if (moveDir.value < 0)
            xOffset = 0.015f;
        else
            xOffset = 0;
        StopAllCoroutines();
        transform.localPosition = initialPos;
        StartCoroutine(ShakeCamera());
    }

    IEnumerator ShakeCamera()
    {
        while(shakeTimer > 0)
        {
            transform.localPosition += new Vector3(xOffset, 0.025f, 0.0f);
            shakeTimer -= Time.deltaTime * dampingSpeed;
            yield return null;
        }
        shakeTimer = 0;
        while(transform.localPosition != initialPos)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, initialPos, 0.075f);
            yield return null;
        }
    }
}
