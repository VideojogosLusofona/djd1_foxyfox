using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : Pickup
{
    [SerializeField] float amplitude = 10.0f;
    [SerializeField] float frequency = 1.0f;

    float   timer = 0.0f;
    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        Vector3 newPos = transform.position;

        newPos.y = startPosition.y + amplitude * Mathf.Sin(timer);

        transform.position = newPos;

        timer += Time.deltaTime * frequency;
    }
}
