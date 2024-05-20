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
        public List<string> Dialogue;
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
}