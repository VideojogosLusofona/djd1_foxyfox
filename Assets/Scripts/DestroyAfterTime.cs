using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] float timeToLive = 1.0f;

    void Update()
    {
        timeToLive -= Time.deltaTime;

        if (timeToLive < 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
