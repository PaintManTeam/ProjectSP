using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Data
{
    // 다이얼로그 데이터는 따로 분리해서 관리할 예정
    #region DialogueDatas
    [Serializable]
    public class DialogueData
    {
        public int DataId;
        public string Name;
        public string Dialogue;
    }

    [Serializable]
    public class DialogueDataLoader : ILoader<int, DialogueData>
    {
        public List<DialogueData> dialogues = new List<DialogueData>();

        public Dictionary<int, DialogueData> MakeDict()
        {
            Dictionary<int, DialogueData> dict = new Dictionary<int, DialogueData>();
            foreach (DialogueData dialogue in dialogues)
                dict.Add(dialogue.DataId, dialogue);
            return dict;
        }
    }
    #endregion

    // 에디터에서 자동으로 세팅 및 저장 되는 데이터
    #region StageGroupData
    [Serializable]
    public class StageGroupData
    {
        public int StageId;
        public GimmickSectionData GimmickData;
        public CinematicSectionData CinematicSection;

        public StageGroupData(int stageId, GimmickSectionData gimmickData, CinematicSectionData cinematicSection)
        {
            StageId = stageId;
            GimmickData = gimmickData;
            CinematicSection = cinematicSection;
        }
    }

    [Serializable]
    public class StageGroupDataLoader : ILoader<int, StageGroupData>
    {
        public List<StageGroupData> stageGroups = new List<StageGroupData>();

        public Dictionary<int, StageGroupData> MakeDict()
        {
            Dictionary<int, StageGroupData> dict = new Dictionary<int, StageGroupData>();
            
            // 이 부분을 잘 채워야 하는데?
            
            return dict;
        }
    }

    [Serializable]
    public class GimmickSectionData
    {
        public int SectionId;
        public List<GimmickComponentData> GimmickComponentDatas;

        public GimmickSectionData(int sectionId, List<GimmickComponentData> gimmickComponentDatas)
        {
            SectionId = sectionId;
            GimmickComponentDatas = gimmickComponentDatas;
        }
    }

    [Serializable]
    public class GimmickComponentData
    {
        public int GimmickObjectId;
        public List<int> ActiveObjectConditionList; // 오브젝트 Id
        public List<int> GimmickReadyConditionList; // 오브젝트 Id

        public GimmickComponentData(int gimmickObjectId, List<int> activeObjectConditionList, List<int> gimmickReadyConditionList)
        {
            GimmickObjectId = gimmickObjectId;
            ActiveObjectConditionList = activeObjectConditionList;
            GimmickReadyConditionList = gimmickReadyConditionList;
        }
    }

    [Serializable]
    public class CinematicSectionData
    {
        public int SectionId;
        public List<CinematicComponentData> CinematicComponentDatas;

        public CinematicSectionData(int sectionId, List<CinematicComponentData> cinematicComponentDatas)
        {
            SectionId = sectionId;
            CinematicComponentDatas = cinematicComponentDatas;
        }
    }

    [Serializable]
    public class CinematicComponentData
    {
        public int CinematicObjectId;

        public CinematicComponentData(int cinematicObjectId)
        {
            CinematicObjectId = cinematicObjectId;
        }
    }
    #endregion
}