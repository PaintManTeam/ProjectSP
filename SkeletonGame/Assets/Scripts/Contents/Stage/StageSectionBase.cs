using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public abstract class StageSectionBase : InitBase
{
    public EStageSectionType SectionType { get; protected set; }

    [SerializeField, ReadOnly] public Transform PlayerStartPoint;

    [SerializeField, ReadOnly] int stageSectionId = 0;
    public int StageSectionId
    {
        get
        {
            if (stageSectionId <= 0)
            {
                string[] strs = gameObject.name.Split(' ');
                stageSectionId = int.Parse(strs[strs.Length - 1]);
            }

            return stageSectionId;
        }

        private set { stageSectionId = value; }
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;


        return true;
    }

    public virtual void StartSection(Player player)
    {
        player.transform.position = PlayerStartPoint.position;
    }

#if UNITY_EDITOR
    protected virtual void Reset()
    {
        PlayerStartPoint = gameObject.transform.Find("PlayerStartPoint");

        if (PlayerStartPoint == null)
        {
            GameObject go = Util.Editor_InstantiateObject(transform);
            go.name = "PlayerStartPoint";
            PlayerStartPoint = go.transform;
        }
    }

    public abstract void Editor_SaveSectionData();
    public abstract void Editor_LoadSectionData();

#endif
}
