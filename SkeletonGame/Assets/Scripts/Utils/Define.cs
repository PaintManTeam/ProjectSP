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
        Ground = 7,
        Object = 8,
        Player = 9,
    }

    public enum ETag
    {
        Untagged,
        
        MainCamera,
        Player,
        GameController,

        Ground,
        Interaction,
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
        None,
        Idle, // loop
        Move, // loop
        Jump,
        FallDown,
        Climb, // loop
        Interaction, // loop
        EnterPortal,
        ComeOutPortal,
        
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

    public enum EStageSectionType
    {
        Cinematic, // 시네마틱 연출
        GimmickPuzzle, // 기믹 퍼즐
    }

    public enum ECinematicType
    {
        MoveCamera,
        ZoomOutInCamera,
        FadeOutInScreen,
        PlayerAction,

    }

    public enum EGimmickType
    {
        Interaction, // 상호 작용을 하면 기믹 발동
        Trigger, // 닿을 경우 기믹 발동
    }

    public enum EInteractionType
    {
        None,
        Dialogue, // 대화
        Portal, // 특정 위치로 순간이동
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