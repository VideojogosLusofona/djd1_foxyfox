using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public enum Mode { PositionLock, FeedbackLoop, FixedSpeed };

    public Mode         mode;
    public Transform    target;
    public Vector3      offset;
    public float        cameraSpeed = 0.1f;
    public bool         enforceBounds;
    public Rect         bounds;

    Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector3 newPos = target.position + offset;

        Vector3 delta = newPos - transform.position;

        switch (mode)
        {
            case Mode.PositionLock:
                break;
            case Mode.FeedbackLoop:
                newPos = transform.position + delta * cameraSpeed * Time.fixedDeltaTime;
                break;
            case Mode.FixedSpeed:
                newPos = Vector3.MoveTowards(transform.position, newPos, cameraSpeed * Time.fixedDeltaTime);
                break;
        }
        
        if (enforceBounds)
        {
            float sizeY = camera.orthographicSize;
            float sizeX = sizeY * camera.aspect;

            newPos.x = Mathf.Clamp(newPos.x, bounds.xMin + sizeX, bounds.xMax - sizeX);
            newPos.y = Mathf.Clamp(newPos.y, bounds.yMin + sizeY, bounds.yMax - sizeY);
        }

        newPos.z = transform.position.z;

        transform.position = newPos;
    }

    private void OnDrawGizmosSelected()
    {
        if (enforceBounds)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(bounds.center, bounds.size);
        }
    }
}
