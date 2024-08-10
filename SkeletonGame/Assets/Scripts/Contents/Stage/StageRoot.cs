using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageRoot : InitBase
{
    [SerializeField, ReadOnly] List<BaseStageCorner> cornerList;
    
    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Reset();

        return true;
    }

    private void Reset()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            BaseStageCorner stageCorner = transform.GetChild(i).GetComponent<BaseStageCorner>();

            if (stageCorner != null)
            {
                cornerList.Add(stageCorner); 
                stageCorner.SetInfo(cornerList.Count);
            }
        }
    }

    public void StartStage(int cornerNum = 1)
    {

    }
}
