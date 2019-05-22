using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    public Camera mastercamera;
    public float paralaxScale = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newPos = mastercamera.transform.position * paralaxScale;

        newPos.z = transform.position.z;

        transform.position = newPos;

        transform.position = mastercamera.transform.position * paralaxScale;

    }
}
