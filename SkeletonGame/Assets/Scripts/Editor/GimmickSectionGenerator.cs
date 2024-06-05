using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Define;

[CustomEditor(typeof(GimmickSection))]
public class GimmickSectionGenerator : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GimmickSection gimmickSection = (GimmickSection)target;


    }
}
