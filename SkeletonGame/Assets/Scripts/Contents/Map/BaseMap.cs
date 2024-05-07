using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class BaseMap : InitBase
{
    [SerializeField] public EMapType MapType { get; private set; }

    public SpriteRenderer boundMapSprite;

    public Vector2 MinBound { get; private set; }
    public Vector2 MaxBound { get; private set; }

    public List<LayerMap> layers = new List<LayerMap>();

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        if (boundMapSprite == null)
            Debug.LogError("BoundMap를 참조하고 있지 않습니다.!");

        MinBound = new Vector2(boundMapSprite.bounds.min.x, boundMapSprite.bounds.min.y);
        MaxBound = new Vector2(boundMapSprite.bounds.max.x, boundMapSprite.bounds.max.y);

        /*
        // 바로 아래에 있는 자식들에게 접근해 레이어 맵을 전부 가져옴
        foreach (Transform child in transform)
        {
            LayerMap map = child.GetComponent<LayerMap>();

            if (map != null)
                layers.Add(map);
        }
        */

        return true;
    }

    // 임시
    private void Start()
    {
        SetInfo();
    }

    public virtual void SetInfo()
    {
        Camera.main.GetComponent<CameraController>().SetCameraBounds(this);
    }
}
