using Data;
using Newtonsoft.Json;
using Steamworks;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using UnityEngine;
using static Define;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();


}

public class DataMgr
{
    public Dictionary<int, Data.JDialogueData> DialogueDict { get; private set; } = new Dictionary<int, Data.JDialogueData>();
    public Dictionary<int, Data.JStageData> StageInfoDict { get; private set; } = new Dictionary<int, Data.JStageData>();
    
    public void Init()
    {
        DialogueDict = LoadJson<Data.JDialogueDataLoader, int, Data.JDialogueData>("DialogueData").MakeDict();

        
    }

    public Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/JsonData/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

    /// <summary>
    /// 런타임에서 호출
    /// </summary>
    public JStageData GetStageData(int stageId)
    {
        string stagePath = Application.dataPath + $"Resource/Data/JsonData/StageData/Stage {stageId}";
        if (File.Exists(stagePath) == false)
            return null;

        // 섹션들을 뽑아옴

        return null;
    }

    /// <summary>
    /// 에디터에서도 호출될 수 있음
    /// </summary>
    public JSectionData GetSectionData(int stageId, int sectionId)
    {
        // 경로 확인
        string path = Application.dataPath + $"Resources/Data/JsonData/StageData/Stage {stageId}/";
        string gimmickSectionPath = path + $"{EStageSectionType.GimmickSection}";
        string cinematicSectionPath = path + $"{EStageSectionType.CinematicSection}";

        // 기믹 섹션 데이터 - 컴포넌트
        List<JGimmickComponentData> gimmickComponentDataList = new();
        DirectoryInfo gimmickInfo = new DirectoryInfo(gimmickSectionPath);
        foreach(FileInfo fileInfo in gimmickInfo.GetFiles("*.json"))
        {
            string[] strs = fileInfo.Name.Split(' ');
            int objectId = int.Parse(strs[strs.Length - 1]);
            
            string jsonData = File.ReadAllText(gimmickSectionPath + $"/{EStageSectionType.GimmickSection} {objectId}");
            gimmickComponentDataList.Add(JsonUtility.FromJson<JGimmickComponentData>(jsonData));
        }
        
        // 시네마틱 섹션 데이터
        List<JCinematicComponentData> cinematicSectionDataList = new();
        DirectoryInfo cinematicInfo = new DirectoryInfo(cinematicSectionPath);
        foreach (FileInfo fileInfo in cinematicInfo.GetFiles("*.json"))
        {
            string[] strs = fileInfo.Name.Split(' ');
            int objectId = int.Parse(strs[strs.Length - 1]);

            string jsonData = File.ReadAllText(cinematicSectionPath + $"/{EStageSectionType.CinematicSection} {objectId}");
            cinematicSectionDataList.Add(JsonUtility.FromJson<JCinematicComponentData>(jsonData));
        }
        
        if (gimmickComponentDataList.Count == 0 && cinematicSectionDataList.Count == 0)
            return null;

        JGimmickSectionData gimmickSectionData = new JGimmickSectionData(gimmickComponentDataList);
        JCinematicSectionData cinematicSectionData = new JCinematicSectionData(cinematicSectionDataList);

        Dictionary<int, JSectionDataBase> SectionDict = new();
        
        return null;
    }
}
