using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera cam;
    Transform target = null;

    Vector2 maxBound, minBound;
    float halfHeight, halfWidth;
    float clampedX, clampedY;

    private void Awake()
    {
        Init();
    }

    private void LateUpdate()
    {
        if(target != null)
            FollowingTarget();
    }

    private void Init()
    {
        cam = Camera.main;
        cam.transform.position = new Vector3(0, 0, -10);

    }

    private void SetCameraBounds(Vector2 _maxBound, Vector2 _minBound)
    {
        minBound = _minBound;
        maxBound = _maxBound;
    }

    private void FollowingTarget()
    {
        clampedX = Mathf.Clamp(target.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
        clampedY = Mathf.Clamp(target.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

        transform.position = new Vector3(clampedX, clampedY, -10);
    }

}
