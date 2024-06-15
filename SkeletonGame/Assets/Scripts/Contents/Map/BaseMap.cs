using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class BaseMap : InitBase
{
    public EMapType MapType { get; private set; }

    [SerializeField]
    SpriteRenderer boundMapSprite;

    // 지울 예정
    public Transform PlayerSpawnPoint { get; protected set; }

    public Vector2 MinBound { get; private set; }
    public Vector2 MaxBound { get; private set; }

    private List<LayerMap> layers = new List<LayerMap>();

    private void OnEnable()
    {
        SetInfo(0); // 나중에 처리 해줘야 함 (임시)
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        if (boundMapSprite == null)
            boundMapSprite = Util.FindChild(gameObject, "BoundMap").GetComponent<SpriteRenderer>();

        MinBound = new Vector2(boundMapSprite.bounds.min.x, boundMapSprite.bounds.min.y);
        MaxBound = new Vector2(boundMapSprite.bounds.max.x, boundMapSprite.bounds.max.y);

        PlayerSpawnPoint = Util.FindChild(gameObject, "PlayerSpawnPoint").GetComponent<Transform>();

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

    public virtual void SetInfo(int templateId)
    {
        Camera.main.GetComponent<CameraController>().SetCameraBounds(this);
    }
}
