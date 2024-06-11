using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Define;

[CustomEditor(typeof(PortalComponent))]
public class PortalComponentInfoSetting : Editor
{
    PortalConnectionObject portalConnectionObject;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PortalComponent portalComponent = (PortalComponent)target;

        StaticInfoSetting.OnGimmcikInspectorGUI((GimmickComponentBase)target);

        GUILayout.Space(10);
        GUILayout.Label("- 포탈 오브젝트 -", EditorStyles.boldLabel);

        // 포탈 오브젝트 연결
    }
}
