using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        SceneType = Define.EScene.GameScene;

        // 카메라 컨트롤러, 플레이어 컨트롤러 등등 연결

        return true;
    }

    public override void Clear()
    {

    }
}
