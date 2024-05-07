using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera cam;
    BaseMap map = null;

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
        if(_target != null && map != null)
        {
            FollowingTarget();
        }
    }

    private void Init()
    {
        cam = Camera.main;
        cam.transform.position = new Vector3(0, 0, -10);
        cam.orthographicSize = 1.5f;
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;
    }

    public void SetCameraBounds(BaseMap map)
    {
        this.map = map;
        minBound = map.MinBound;
        maxBound = map.MaxBound;
    }

    private void FollowingTarget()
    {
        clampedX = Mathf.Clamp(_target.GetCenterPosition().x, minBound.x + halfWidth, maxBound.x - halfWidth);
        clampedY = Mathf.Clamp(_target.GetCenterPosition().y, minBound.y + halfHeight, maxBound.y - halfHeight);

        transform.position = new Vector3(clampedX, clampedY, -10);
    }

}
