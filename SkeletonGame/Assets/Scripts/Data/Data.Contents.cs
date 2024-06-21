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
    public class JDialogueData
    {
        public int DataId;
        public string Name;
        public string Dialogue;
    }

    [Serializable]
    public class JDialogueDataLoader : ILoader<int, JDialogueData>
    {
        public List<JDialogueData> dialogues = new List<JDialogueData>();

        public Dictionary<int, JDialogueData> MakeDict()
        {
            Dictionary<int, JDialogueData> dict = new Dictionary<int, JDialogueData>();
            foreach (JDialogueData dialogue in dialogues)
                dict.Add(dialogue.DataId, dialogue);
            return dict;
        }
    }
    #endregion

    // 에디터에서 자동으로 세팅 및 저장 되는 데이터
    #region StageData
    [Serializable]
    public class JStageData
    {
        public int StageId;
        public Dictionary<int, JSectionData> SectionDict;

        public JStageData(int stageId, Dictionary<int, JSectionData> sectionDict)
        {
            StageId = stageId;
            SectionDict = sectionDict;
        }
    }

    [Serializable]
    public class JSectionData
    {
        public int SectionId;
        public Dictionary<int, JSectionDataBase> SectionDict;

        public JSectionData(int sectionId, Dictionary<int, JSectionDataBase> sectionDict)
        {
            SectionId = sectionId;
            SectionDict = sectionDict;
        }
    }

    [Serializable] public class JSectionDataBase { }

    #region JGimmickSectionData
    [Serializable]
    public class JGimmickSectionData : JSectionDataBase
    {
        public List<JGimmickComponentData> GimmickComponentDatas;

        public JGimmickSectionData(List<JGimmickComponentData> gimmickComponentDatas)
        {
            GimmickComponentDatas = gimmickComponentDatas;
        }
    }

    [Serializable]
    public class JGimmickComponentData
    {
        public int GimmickObjectId;
        public List<int> ActiveObjectConditionList; // 오브젝트 Id
        public List<int> GimmickReadyConditionList; // 오브젝트 Id

        public JGimmickComponentData(int gimmickObjectId, List<int> activeObjectConditionList, List<int> gimmickReadyConditionList)
        {
            GimmickObjectId = gimmickObjectId;
            ActiveObjectConditionList = activeObjectConditionList;
            GimmickReadyConditionList = gimmickReadyConditionList;
        }
    }

    [Serializable]
    public class JGimmickComponentDataLoader : ILoader<int, JGimmickComponentData>
    {
        public List<JGimmickComponentData> gimmickComponents = new List<JGimmickComponentData>();

        public Dictionary<int, JGimmickComponentData> MakeDict()
        {
            Dictionary<int, JGimmickComponentData> dict = new Dictionary<int, JGimmickComponentData>();
            foreach (JGimmickComponentData gimmickComponent in gimmickComponents)
                dict.Add(gimmickComponent.GimmickObjectId, gimmickComponent);
            return dict;
        }
    }
    #endregion

    #region JCinematicSectionData
    [Serializable]
    public class JCinematicSectionData : JSectionDataBase
    {
        public List<JCinematicComponentData> CinematicComponentDataList;

        public JCinematicSectionData(List<JCinematicComponentData> cinematicComponentDataList)
        {
            CinematicComponentDataList = cinematicComponentDataList;
        }
    }

    [Serializable]
    public class JCinematicComponentData
    {
        public int CinematicObjectId;

        public JCinematicComponentData(int cinematicObjectId)
        {
            CinematicObjectId = cinematicObjectId;
        }
    }

    [Serializable]
    public class JCinematicComponentDataLoader : ILoader<int, JCinematicComponentData>
    {
        public List<JCinematicComponentData> cinematicComponents = new List<JCinematicComponentData>();

        public Dictionary<int, JCinematicComponentData> MakeDict()
        {
            Dictionary<int, JCinematicComponentData> dict = new Dictionary<int, JCinematicComponentData>();
            foreach (JCinematicComponentData cinematicComponent in cinematicComponents)
                dict.Add(cinematicComponent.CinematicObjectId, cinematicComponent);
            return dict;
        }
    }
    #endregion
    
    #endregion
}