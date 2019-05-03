using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxCamera : MonoBehaviour
{
    public Camera   masterCamera;
    public Vector2  parallaxFactor = new Vector2(1,1);

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 newPos = masterCamera.transform.position * parallaxFactor;

        newPos.x = masterCamera.transform.position.x * parallaxFactor.x;
        newPos.y = masterCamera.transform.position.y * parallaxFactor.y;
        newPos.z = transform.position.z;

        transform.position = newPos;
    }
}
