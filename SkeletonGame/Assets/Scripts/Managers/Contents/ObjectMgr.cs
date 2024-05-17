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
    private GameObject _creatureRoot;
    public GameObject CreatureRoot
    {
        get
        {
            if (_creatureRoot == null) _creatureRoot = GameObject.Find("@Object_CreatureRoot");
            if (_creatureRoot == null) _creatureRoot = new GameObject { name = "@Object_CreatureRoot" };
            return _creatureRoot;
        }
    }

    private GameObject _npcRoot;
    public GameObject NpcRoot
    {
        get
        {
            if (_npcRoot == null) _npcRoot = GameObject.Find("@Object_NpcRoot");
            if (_npcRoot == null) _npcRoot = new GameObject { name = "@Object_NpcRoot" };
            return _npcRoot;
        }
    }

    public T SpawnCreature<T>(Vector3 position, int templateID = 1) where T : Creature
    {
        string prefabName = typeof(T).Name;
        string path = $"{PrefabPath.OBJECT_CREATURE_PATH}/{prefabName}";

        GameObject go = Managers.Resource.Instantiate(path, CreatureRoot.transform);
        go.transform.position = position;

        Creature obj = go.GetComponent<Creature>();

        if (obj.ObjectType == EObjectType.Creature)
        {
            Creature creature = go.GetComponent<Creature>();
            switch (creature.CreatureType)
            {
                case ECreatureType.Player:
                    creature.transform.parent = CreatureRoot.transform;
                    break;
                case ECreatureType.Npc:
                    creature.transform.parent = NpcRoot.transform;
                    break;
            }

            creature.SetInfo(templateID);
        }

        return obj as T;
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
