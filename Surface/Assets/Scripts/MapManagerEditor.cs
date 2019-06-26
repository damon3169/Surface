using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if (UNITY_EDITOR) 
[CustomEditor(typeof(MapManager))]
public class MapManagerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		MapManager myScript = (MapManager)target;
		if (GUILayout.Button("Build Object"))
		{
			myScript.CreateTile();
		}
	}

	private void OnEnable()
	{
		MapManager myScript = (MapManager)target;

		EditorApplication.update += myScript.inEditor;
	}
}
#endif