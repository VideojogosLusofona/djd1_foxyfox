using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public enum Mode { Feedback, Linear };

    public Mode         mode;
    public Transform    target;
    public Vector3      offset;
    public float        cameraSpeed = 0.1f;
    public bool         enforceBounds = false;
    public Rect         bounds;

    Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        Vector3 newPos = target.position + offset;

        if (mode == Mode.Feedback)
        {
            Vector3 delta = newPos - transform.position;

            newPos = transform.position + delta * cameraSpeed * Time.fixedDeltaTime;
        }
        else if (mode == Mode.Linear)
        {
            newPos = Vector3.MoveTowards(transform.position, newPos, cameraSpeed * Time.fixedDeltaTime);
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

    private void OnDrawGizmos()
    {
        if (enforceBounds)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(bounds.center, new Vector3(bounds.width, bounds.height, 0.0f));
        }
    }
}
