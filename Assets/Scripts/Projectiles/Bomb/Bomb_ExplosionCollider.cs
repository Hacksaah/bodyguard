using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_ExplosionCollider : MonoBehaviour
{
    private CircleCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<CircleCollider2D>();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(DecreaseHitBoxSize());
    }

    IEnumerator DecreaseHitBoxSize()
    {
        col.radius = 2.45f;
        while(col.radius >= 0.5f)
        {
            col.radius -= 0.05f;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
