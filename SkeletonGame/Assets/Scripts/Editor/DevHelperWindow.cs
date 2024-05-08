using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public struct MenuInfo
{
	public readonly string name;
	public readonly Action onClickMenu;

	public MenuInfo(string name, Action onClickMenu)
	{
		this.name = name;
		this.onClickMenu = onClickMenu;
	}
}

public class DevHelperWindow : EditorWindow
{
	private static List<MenuInfo> menuInfos = new List<MenuInfo>()
	{
		new MenuInfo("테스트 버튼", OnClickTest)
	};

	[MenuItem("Tools/DevHelperWindow %#D")]
	public static void OpenWindow()
	{
		var window = GetWindow<DevHelperWindow>();
		if (window != null)
		{
			window.Show();
		}
	}

	private void OnGUI()
	{
		EditorGUILayout.BeginHorizontal();
		{
			MainProc();
		}
		EditorGUILayout.EndHorizontal();
	}

	private void MainProc()
	{
		foreach (var menuInfo in menuInfos)
		{
			string menuName = menuInfo.name;
			if (GUILayout.Button(menuName))
			{
				menuInfo.onClickMenu?.Invoke();
			}
		}
	}

	private static void OnClickTest()
	{
		Debug.Log("테스트 버튼 입력");
	}
}
