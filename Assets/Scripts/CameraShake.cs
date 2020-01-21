using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public float intensity_min = 0.1f;
    public float intensity_max = .75f;

    Vector2 randDir;
    float intensity;

    public void Shake_Camera()
    {
        Vector2 initialPos = transform.position;
        float xRand = Random.Range(-.5f, .5f);
        float s = -1.0f;
        Debug.Log("Shake");
        if (xRand > 0)
            s += xRand;
        else
            s -= xRand;

        float yDist = Random.Range(-2.25f, -1.5f);

        randDir = new Vector2(0, yDist);

        intensity = Random.Range(intensity_min, intensity_max);

        StartCoroutine("MoveCamera");
    }

    IEnumerator MoveCamera()
    {
        Vector2 target = (Vector2)transform.position + randDir;
        Vector2 initialPos = transform.position;

        int MAX = 30;
        int c = 0;
        while((Vector2)transform.position != target)
        {
            c++;
            if (c == MAX)
            {
                Debug.Log("Borken Loooop");
                break;
            }
            Debug.Log("Moving cam");
            transform.position = Vector2.Lerp(transform.position, target, intensity);
            yield return null;
        }
        yield return null;
    }
}
