using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class BaseMap : MonoBehaviour
{
    [SerializeField] public EMapType MapType { get; private set; }

    public SpriteRenderer backgroundMapSprite;

    public Vector2 minBound;
    public Vector2 maxBound;

    private void Awake()
    {
        minBound = new Vector2(backgroundMapSprite.bounds.min.x, backgroundMapSprite.bounds.min.y);
        maxBound = new Vector2(backgroundMapSprite.bounds.max.x, backgroundMapSprite.bounds.max.y);
    }
}
