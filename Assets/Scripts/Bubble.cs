using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private Rigidbody2D rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void StartFloat(float upwardForce, float delay)
    {
        Invoke(nameof(ApplyUpwardForce), delay);
        Destroy(gameObject, 7f); // 버블이 7초 후에 사라짐
    }

    private void ApplyUpwardForce()
    {
        rigid.velocity = Vector2.zero;
        rigid.AddForce(Vector2.up * 2f, ForceMode2D.Impulse);
    }
}
