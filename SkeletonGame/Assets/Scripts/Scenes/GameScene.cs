using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempStageData
{
    public int StageId = 1;
    public string MapName = "TestMap";
}

public class GameScene : BaseScene
{
    TempStageData tempStageData = null;

    BaseMap map;

    // 임시
    [SerializeField]
    Player player;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        SceneType = Define.EScene.GameScene;

        // 현재 스테이지 데이터 받아오기
        tempStageData = new TempStageData();

        // 맵 생성
        map = Managers.Object.SpawnMap(tempStageData.MapName);

        // 캐릭터 생성
        player = Managers.Object.SpawnObject<Player>(map.PlayerSpawnPoint.position);

        // 위에 싹 다 날려야되고, 시네마틱이 이어지는 등의 이슈(?)가 존재하므로,
        // 캐릭터는 한번만 소환됨

        return true;
    }

    public override void Clear()
    {

    }
}
