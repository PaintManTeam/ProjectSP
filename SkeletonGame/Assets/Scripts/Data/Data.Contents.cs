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
    #region GimmickComponentInfoData
    [Serializable]
    public class GimmickComponentInfoData
    {
        public int stageId;
        public int sectionId;
        public int gimmickObjectId;
        public List<int> activeObjectConditionList; // 오브젝트 Id
        public List<int> gimmickReadyConditionList; // 오브젝트 Id

        public GimmickComponentInfoData(int stageId, int sectionId, List<int> activeObjectConditionList, List<int> gimmickReadyConditionList)
        {
            this.stageId = stageId;
            this.sectionId = sectionId;
            this.activeObjectConditionList = activeObjectConditionList;
            this.gimmickReadyConditionList = gimmickReadyConditionList;
        }
    }
    #endregion
}