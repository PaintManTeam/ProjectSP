using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    public enum EScene
    {
        Unknown,
        TitleScene,
        LobbyScene,
        GameScene,
    }

    public enum ELayer
    {
        Default = 0,
        TransparentFX = 1,
        IgnoreRaycast = 2,
        Dummy = 3,
        Water = 4,
        UI = 5,
        Background = 6,

    }

    public enum EObjectType
    {
        None,
        Player,

    }

    public enum EMapType
    {
        None,

    }

    public enum EInputKey
    {
        Left = 0,
        Right = 1,
        Jump = 2,

    }
}