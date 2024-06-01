using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Data
{
    #region DialogueData
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

    // 데이터 구조 고민 좀 천천히 해봐야 될 것 같음 
    
    #region StageData
    [SerializeField]
    public class StageData
    {
        public int DataId;
        public List<int> SectionType;
        public List<int> DataKeyId;
    }

    [SerializeField]
    public class StageDataLoader : ILoader<int, StageData>
    {
        public List<StageData> stageDatas = new List<StageData>();

        public Dictionary<int , StageData> MakeDict()
        {
            Dictionary<int, StageData> dict = new Dictionary<int, StageData>();
            foreach(StageData stageData in stageDatas)
                dict.Add(stageData.DataId, stageData);
            return dict;
        }
    }
    #endregion

    #region GimmickPuzzleData
    [SerializeField]
    public class GimmickPuzzleData
    {
        public int DataId;

    }

    [SerializeField]
    public class GimmickPuzzleDataLoader : ILoader<int, GimmickPuzzleData>
    {
        public List<GimmickPuzzleData> gimmickPuzzleDatas = new List<GimmickPuzzleData>();

        public Dictionary<int, GimmickPuzzleData> MakeDict()
        {
            Dictionary<int, GimmickPuzzleData> dict = new Dictionary<int, GimmickPuzzleData>();
            foreach(GimmickPuzzleData gimmickPuzzleData in gimmickPuzzleDatas)
                dict.Add(gimmickPuzzleData.DataId, gimmickPuzzleData);
            return dict;
        }
    }
    #endregion

    #region CinematicData
    [SerializeField]
    public class CinematicData
    {
        public int DataId;

    }

    [SerializeField]
    public class CinematicDataLoader : ILoader<int, CinematicData>
    {
        public List<CinematicData> cinematicDatas = new List<CinematicData>();

        public Dictionary<int, CinematicData> MakeDict()
        {
            Dictionary<int, CinematicData> dict = new Dictionary<int, CinematicData>();
            foreach(CinematicData cinematicData in cinematicDatas)
                dict.Add(cinematicData.DataId, cinematicData);
            return dict;
        }
    }
    #endregion
}