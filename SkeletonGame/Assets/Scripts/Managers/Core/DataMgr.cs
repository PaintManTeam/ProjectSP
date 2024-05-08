using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();


}

public class DataMgr
{
    public Dictionary<int, Data.DialogueData> DialogueDict { get; private set; } = new Dictionary<int, Data.DialogueData>();
    

    public void Init()
    {
        DialogueDict = LoadJson<Data.DialogueDataLoader, int, Data.DialogueData>("DialogueData").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}