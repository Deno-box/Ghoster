using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerMovePath))]
public class PlayerMoveDataEditor : Editor
{
    public void OnEnable()
    {
        PlayerMovePath data = (PlayerMovePath)target;

        //EditorGUILayout.MinMaxSlider(ref data.nowPosMin, ref data.nowPosMax, 0.0f, 1000000.0f);
    }
}
