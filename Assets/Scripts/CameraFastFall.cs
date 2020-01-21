using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFastFall : MonoBehaviour
{
    [SerializeField]
    private Transform camTransform;

    private float shakeDuration = 0f;

    private float shakeMagnitude = 0.6f;

    private float dampingSpeed = 2.0f;

    Vector3 initialPos;


    // Start is called before the first frame update
    void Start()
    {
        initialPos = camTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Shake 1: Is more shakey, goes up and down multiple times randomly before settling
        //0.2f second duration?
        /*
        if (shakeDuration > 0)
        {
            camTransform.localPosition = initialPos + new Vector3(0.0f, Random.value - 0.5f, 0.0f) * 0.6f;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = initialPos;
        }
        */


        //Shake 2: Has a cleaner 'thump', moves smoothly up before falling back down
        //0.2f second duration?

        if (shakeDuration > 0)
        {
            camTransform.localPosition += new Vector3(0.0f, 0.05f, 0.0f);
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else if(shakeDuration <= 0f)
        {
            shakeDuration = 0f;
            while(camTransform.localPosition != initialPos)
            {
                camTransform.localPosition -= new Vector3(0.0f, 0.05f, 0.0f);
            }
        }
        


        //Shake 3: Has a more general feel, vibrates the screen instead of an up and down motion
        //0.4f second duration?
        /*
        if (shakeDuration > 0)
        {
            camTransform.localPosition = initialPos + new Vector3(Random.value - 0.5f, Random.value - 0.5f, 0.0f) * 0.2f;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = initialPos;
        }
        */

        //For testing: Hit space to activate current camera shake
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetShakeDuration(0.2f);
        }
    }

    public void SetShakeDuration(float shakeDur)
    {
        shakeDuration = shakeDur;
    }
}
