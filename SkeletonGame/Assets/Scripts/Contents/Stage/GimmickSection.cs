using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGimmickElement
{

}

public class GimmickSection : StageSectionBase
{
    [SerializeField] // 확인용
    List<int> InstanceIDList = new List<int>();

    [SerializeField] List<IGimmickElement> GimmickSectionList = new List<IGimmickElement>();

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        

        return true;
    }

#if UNITY_EDITOR

    public void GenerateGimmickObject()
    {

    }

    public void UpdateGimmickObject()
    {

    }



#endif
}
