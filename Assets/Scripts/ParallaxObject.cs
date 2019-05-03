using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxObject : MonoBehaviour
{
    public Camera masterCamera;
    public float  parallaxFactor = 0.5f;

    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - masterCamera.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = masterCamera.transform.position * parallaxFactor + offset;
    }
}
