using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxCameraCtrl : MonoBehaviour
{
    public Camera masterCamera;
    public float  parallaxScale = 0.1f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newPos = masterCamera.transform.position * parallaxScale;

        newPos.z = transform.position.z;

        transform.position = newPos;
    }
}
