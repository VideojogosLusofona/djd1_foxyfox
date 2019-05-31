using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCR : MonoBehaviour
{
    public Sprite   sprite1;
    public Sprite   sprite2;
    public float    time1;
    public Color    color1;
    public Color    color2;
    public float    time2;
    public Vector3  scale1;
    public Vector3  scale2;
    public float    time3;

    SpriteRenderer spriteRenderer;
    Coroutine      xpto;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        xpto = StartCoroutine(TestA("TestA1", sprite1, sprite2));
        StartCoroutine(TestD("TestA2", color1, color2));
        StartCoroutine(TestC("TestA3", scale1, scale2));
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StopCoroutine(xpto);
        }
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void OnDestroy()
    {
        
    }

    IEnumerator TestA(string n, Sprite sprite1, Sprite sprite2)
    {
        while (true)
        {
            Debug.Log("T = " + Time.time + ": " + n);

            spriteRenderer.sprite = sprite1;

            yield return new WaitForSeconds(time1);

            Debug.Log("T = " + Time.time + ": " + n);

            spriteRenderer.sprite = sprite2;

            yield return new WaitForSeconds(time1);
        }
    }

    IEnumerator TestB(string n, Color color1, Color color2)
    {
        while (true)
        {
            Debug.Log("T = " + Time.time + ": " + n);

            spriteRenderer.color = color1;

            yield return new WaitForSeconds(time2);

            Debug.Log("T = " + Time.time + ": " + n);

            spriteRenderer.color = color2;

            yield return new WaitForSeconds(time2);
        }
    }

    IEnumerator TestC(string n, Vector3 scale1, Vector3 scale2)
    {
        while (true)
        {
            Debug.Log("T = " + Time.time + ": " + n);

            spriteRenderer.transform.localScale = scale1;

            yield return new WaitForSeconds(time3);

            Debug.Log("T = " + Time.time + ": " + n);

            spriteRenderer.transform.localScale = scale2;

            yield return new WaitForSeconds(time3);
        }
    }

    IEnumerator TestD(string n, Color color1, Color color2)
    {
        yield return null;

        Color currentColor = color1;

        float t = 0.0f;

        while (t < 1.0f)
        {
            spriteRenderer.color = Color.Lerp(color1, color2, t);

            t = t + Time.deltaTime;

            yield return null;
        }
    }
}
