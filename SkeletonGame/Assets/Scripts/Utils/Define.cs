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
    
    public enum ECharacterState
    {
        Idle,
        Move,
        Jump,
        Climb,
        Dead
    }

    public enum EObjectType
    {
        None,
        Player,
        Npc,
        Interaction,
    }

    public enum EMapType
    {
        None,

    }

    public enum EInputKey
    {
        Left,
        Right,
        Jump,
        Interaction,
    }
}