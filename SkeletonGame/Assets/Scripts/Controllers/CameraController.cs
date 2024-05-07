using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera cam;
    BaseMap map;

    public BaseObject _target;
    public BaseObject Target
    {
        get { return _target; }
        set { _target = value; }
    }

    public Vector2 maxBound, minBound;
    public float halfHeight, halfWidth;
    public float clampedX, clampedY;

    private void Awake()
    {
        Init();
    }

    private void LateUpdate()
    {
        if(_target != null)
        {
            FollowingTarget();
        }
    }

    private void Init()
    {
        cam = Camera.main;
        cam.transform.position = new Vector3(0, 0, -10);

        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;
    }

    public void SetCameraBounds(Vector2 _minBound, Vector2 _maxBound)
    {
        minBound = _minBound;
        maxBound = _maxBound;
    }

    private void FollowingTarget()
    {
        clampedX = Mathf.Clamp(_target.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
        clampedY = Mathf.Clamp(_target.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

        transform.position = new Vector3(clampedX, clampedY, -10);
    }

}
