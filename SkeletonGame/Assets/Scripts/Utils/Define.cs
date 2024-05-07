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
        GimmickObject,
        
    }

    public enum ECreatureType
    {
        Player,
        Npc,
    }

    public enum ECreatureState
    {
        Idle, // loop
        Move, // loop
        Jump,
        FallDown,
        Climb, // loop
        Interaction, // loop
        Dead
    }

    public enum EPlayerType
    {
        TestPlayer,

    }

    public enum ENpcType
    {
        TestNpc,

    }

    public enum EGimmickObjectType
    {
        Interaction, // 상호 작용을 하면 기믹 발동
        Trigger, // 닿을 경우 기믹 발동
    }

    public enum EGimmickObjectState
    {
        StandBy, // 소환만 된 상태
        Ready, // 기믹 수행이 가능한 상태
        Complete, // 기믹 완료 상태
    }

    public enum EMapType
    {
        None,
        TestMap
    }
}