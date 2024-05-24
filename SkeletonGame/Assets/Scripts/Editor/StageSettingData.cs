using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

#if UNITY_EDITOR

public class StagePrefabsData
{
    public int DataId; // 순서 ( 0 ~ N )
    public EStageSectionType SectionType;
}

#region Cinematic
public class CinematicGroupData
{
    public ECinematicType DataType; // 시네마틱 연출 타입
    public int DataKeyId; // 세부 테이블 Id
}

#endregion

#region GimmickPuzzle
public class GimmickPuzzleGroupData
{
    public EInteractionType DataType; // 시네마틱 테이블 오브젝트 데이터 타입
    public int DataKeyId; // 세부 테이블 Id
}

public class StageObjectDataBase
{
    public int DataId; // DataKeyId
}
#endregion

#endif