using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDebugScene : BaseScene
{
    [SerializeField, ReadOnly] Player player;
    [SerializeField, ReadOnly] StageRoot currStage;

    [Header("스테이지 ID")]
    [SerializeField] private int stageId = 1;

    [Header("스테이지 섹션 ID")]
    [SerializeField] private int stageSectionId = 1;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        SceneType = Define.EScene.StageDebugScene;

        return true;
    }

    private void Start()
    {
        GameObject go = Managers.Resource.Instantiate(PrefabPath.STAGE_PATH + $"/Stage {stageId}");

        if(go == null)
        {
            Debug.LogError($"Stage {stageId} 은 없는 스테이지입니다.");
            UnityEditor.EditorApplication.isPlaying = false;
        }

        // 스테이지 소환
        currStage = go.GetComponent<StageRoot>();

        // 캐릭터 소환
        player = Managers.Object.SpawnObject<Player>(Vector3.zero);

        currStage.StartStage(player, stageSectionId);
    }

    public override void Clear()
    {

    }
}
