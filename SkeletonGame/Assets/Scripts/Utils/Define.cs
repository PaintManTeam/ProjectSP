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
        Creature,
        Interaction,
    }

    public enum ECreatureType
    {
        Player,
        Npc,
    }

    public enum ECreatureState
    {
        Idle,
        Move,
        Jump,
        Climb,
        Dead
    }

    public enum EInteractionObjectType
    {

    }

    public enum EInteractionState
    {

    }

    public enum EMapType
    {
        None,
        TestMap
    }
}