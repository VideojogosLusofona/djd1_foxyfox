using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxObj : MonoBehaviour
{
    public Camera masterCamera;
    public float paralaxScale = 0.1f;

    Vector3 originalPosition;


    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position - masterCamera.transform.position;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = masterCamera.transform.position * paralaxScale + originalPosition;
    }
}
