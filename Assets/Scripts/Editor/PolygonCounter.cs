using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MeshFilter))]
public class PolygonCounter : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		MeshFilter filter = target as MeshFilter;
		string polygons = "Polygons: " + filter.sharedMesh.triangles.Length / 3;
		EditorGUILayout.LabelField(polygons);
	}
}