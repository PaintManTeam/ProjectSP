using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Define;

public class DevHelperWindow : EditorWindow
{
	public enum Axis
	{
		Horizontal, 
		Vertical,
	}

	public enum FieldType
	{
		String,
		Float,
		Int,
	}

	private static float dpi = 0.0f;

	private Vector3 playerSpawnPos = Vector3.zero;
	private int playerId = -1;
	private Player currentPlayer = null;

	[MenuItem("Tools/DevHelperWindow %#D")]
	public static void OpenWindow()
	{
		var window = GetWindow<DevHelperWindow>();
		if (window != null)
		{
			window.Show();
		}
	}

	private void OnEnable()
	{
		dpi = Screen.dpi;
	}

	private void OnDisable()
	{
		
	}

	private void OnGUI()
	{
		DrawAxis(Axis.Horizontal, DrawAll);
	}

	private void DrawAll()
	{
		DrawAxis(Axis.Vertical, () =>
		{
			DrawSpace(20);

			DrawPlayerSpawnPos();
			DrawPlayerId();

			DrawSpace(10);

			DrawPlayerState();

			DrawSpace(10);

			DrawButton("플레이어 소환", 100, 20, SpawnPlayer);
		});
	}

	private void DrawPlayerSpawnPos()
	{
		DrawLabel("생성할 플레이어 위치", 120, 20);

		DrawAxis(Axis.Horizontal, () =>
		{
			float x = (float)DrawField(FieldType.Float, playerSpawnPos.x, 50, 20);
			float y = (float)DrawField(FieldType.Float, playerSpawnPos.y, 50, 20);
			float z = (float)DrawField(FieldType.Float, playerSpawnPos.z, 50, 20);

			playerSpawnPos = new Vector3(x, y, z);
		});
	}

	private void DrawPlayerId()
	{
		DrawLabel("플레이어 더미 ID", 100, 20);

		DrawAxis(Axis.Horizontal, () =>
		{
			playerId = (int)DrawField(FieldType.Int, playerId, 50, 20);
		});
	}

	private void DrawPlayerState()
	{
		ECreatureState currentState = ECreatureState.None;

		if (currentPlayer != null)
		{
			currentState = currentPlayer.CreatureState;
		}

		DrawLabel($"플레이어 현재 스테이트 : [{currentState}]", 250, 20);
	}

	private void DrawButton(string buttonName, float width, float height, Action onClickButton)
	{
		if (GUILayout.Button(buttonName, GUILayout.Width(width), GUILayout.Height(height)))
		{
			onClickButton?.Invoke();
		}
	}

	private void DrawLabel(string label, float width, float height)
	{
		EditorGUILayout.LabelField(label, GUILayout.Width(width), GUILayout.Height(height));
	}

	private object DrawField(FieldType fieldType, object oldValue, float width, float height)
	{
		switch(fieldType)
		{
			case FieldType.String:
				return EditorGUILayout.TextField((string)oldValue, GUILayout.Width(width), GUILayout.Height(height));

			case FieldType.Float:
				return EditorGUILayout.FloatField((float)oldValue, GUILayout.Width(width), GUILayout.Height(height));

			case FieldType.Int:
				return EditorGUILayout.IntField((int)oldValue, GUILayout.Width(width), GUILayout.Height(height));

			default:
				return EditorGUILayout.TextField((string)oldValue, GUILayout.Width(width), GUILayout.Height(height));
		}
	}

	private void DrawAxis(Axis type, Action onDrawLayout, float width = 0.0f, float height = 0.0f)
	{
		bool isValidWidthAndHeight = width > 0.01f && height > 0.01f;
		switch (type)
		{
			case Axis.Horizontal:

				if (isValidWidthAndHeight)
				{
					EditorGUILayout.BeginHorizontal(GUILayout.Width(width), GUILayout.Height(height));
				}
				else
				{
					EditorGUILayout.BeginHorizontal();
				}

				onDrawLayout?.Invoke();
				EditorGUILayout.EndHorizontal();
				break;

			case Axis.Vertical:

				if (isValidWidthAndHeight)
				{
					EditorGUILayout.BeginVertical(GUILayout.Width(width), GUILayout.Height(height));
				}
				else
				{
					EditorGUILayout.BeginVertical();
				}

				onDrawLayout?.Invoke();
				EditorGUILayout.EndVertical();
				break;
		}
	}

	public void DrawScrollView(Vector2 centerPos, Action onDrawLayout, float width, float height)
	{
		EditorGUILayout.BeginScrollView(centerPos, GUILayout.Width(width), GUILayout.Height(height));

		onDrawLayout?.Invoke();

		EditorGUILayout.EndScrollView();	
	}

	public void DrawSpace(float width = 6.0f)
	{
		GUILayout.Space(width);
	}

	#region 버튼 콜백 및 인게임 함수

	private void SpawnPlayer()
	{
		currentPlayer = Managers.Object.SpawnCreature<Player>(playerSpawnPos, playerId);
	}

	#endregion


}
