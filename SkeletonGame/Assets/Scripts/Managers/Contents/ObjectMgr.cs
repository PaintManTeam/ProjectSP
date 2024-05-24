using JetBrains.Annotations;
using NUnit;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UIElements;
using static Define;

public class ObjectMgr
{
    private GameObject _spawnObjectRoot;
    public GameObject SpawnObjectRoot
    {
        get
        {
            if (_spawnObjectRoot == null) _spawnObjectRoot = GameObject.Find("@SpawnObjectRoot");
            if (_spawnObjectRoot == null) _spawnObjectRoot = new GameObject { name = "@SpawnObjectRoot" };
            return _spawnObjectRoot;
        }
    }

    public T SpawnObject<T>(Vector3 position, int templateID = 1) where T : BaseObject
    {
        string prefabName = typeof(T).Name;
        string path = null;

        if(typeof(Creature).IsAssignableFrom(typeof(T)))
            path = $"{PrefabPath.OBJECT_CREATURE_PATH}/{prefabName}";
        else
        {
            Debug.LogError($"{typeof(T)} 처리 로직 필요 ");
            return null;
        }

        BaseObject spawnObject = Managers.Resource.Instantiate(path, SpawnObjectRoot.transform).GetComponent<BaseObject>();
        spawnObject.transform.position = position;

        spawnObject.SetInfo(templateID);

        return spawnObject as T;
    }

    public BaseMap SpawnMap(string mapName, int templateId = 1)
    {
        string path = $"{PrefabPath.OBJECT_MAP_PATH}/{mapName}";

        GameObject go = Managers.Resource.Instantiate(path);
        go.transform.position = Vector3.zero;

        BaseMap map = go.GetComponent<BaseMap>();

        map.SetInfo(templateId);

        return map;
    }

    public void Despawn<T>(T obj) where T : Creature
    {
        if (obj == null)
            return;

        Managers.Resource.Destroy(obj.gameObject);
    }
}
