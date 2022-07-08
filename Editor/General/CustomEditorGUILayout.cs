using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class CustomEditorGUILayout 
{

    /// <summary>
    /// Scuffed
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static RangedInt RangeIntField(RangedInt rangeInt, GUIContent content = null)
    {
        bool flatten = false;
        int min = 0;
        int max = 0;

        flatten = EditorGUILayout.Toggle(rangeInt.Flatten);
        EditorGUILayout.BeginHorizontal();
        min = EditorGUILayout.IntField(rangeInt.Min);
        max = EditorGUILayout.IntField(rangeInt.Max);
        EditorGUILayout.EndHorizontal();

        RangedInt newRange = !flatten ? 
            new RangedInt(min, max) : new RangedInt(min);

        return newRange;
    } 
}
