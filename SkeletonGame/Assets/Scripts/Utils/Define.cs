using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Define
{
    public enum EScene
    {
        Unknown,
        StageDebugScene,
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
        Idle,
        Move,
        Jump,
        FallDown,
        Climb,
        Interaction,
        EnterPortal, // Move 모션
        ComeOutPortal, // Move 모션
        
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
        None,
        CinematicSection, // 시네마틱 연출
        GimmickSection, // 기믹 퍼즐
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
        None,
        Interaction, // 상호 작용을 하면 기믹 발동
        Collision, // 닿을 경우 기믹 발동
    }

    public enum EInteractionType
    {
        None,
        EndMotion, // 상호 작용 모션이 끝나면 상호작용
        Dialogue, // 대화
        Portal, // 특정 위치로 순간이동
    }

    public enum EGimmickCollisionObjectType
    {
        None,
        TestCollsion,
    }

    public enum EGimmickInteractionObjectType
    {
        None,

        Dialogue,
        Portal,
        Destroy,

    }

    public enum EGimmickObjectState
    {
        StandBy, // 소환만 된 상태 (비활성화)
        Ready, // 기믹 수행이 가능한 상태 (활성화)
        Complete, // 기믹 완료 상태
    }

    public enum EMapType
    {
        None,
        TestMap
    }
}