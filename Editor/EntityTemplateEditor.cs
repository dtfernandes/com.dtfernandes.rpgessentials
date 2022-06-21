using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Inspector = UnityEditor.Editor;
using System.Linq;

namespace RpgEssentials.TurnBased.Editor
{
    [CustomEditor(typeof(EntityTemplate), editorForChildClasses: true)]
    [CanEditMultipleObjects]
    public class EntityTemplateEditor : Inspector
    {
        bool moldfoldout;
        bool movesfoldout;
        SerializedProperty moves;

        private void OnEnable()
        {
            EditorUtility.SetDirty(target);
            moves = serializedObject.FindProperty("moves");

            moldfoldout = true;
            movesfoldout = true;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            EntityMold mold = (target as EntityTemplate).Mold;
            List<BattleStat> listOfStats =
                mold.ToList().ToList();



            moldfoldout = EditorGUILayout.BeginFoldoutHeaderGroup(moldfoldout, "Mold");
           
            if (moldfoldout)
            {
                for (int i = 0; i < listOfStats.Count; i++)
                {
                    BattleStat bs = listOfStats[i];
                    //Handle BattleStat Display 
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(bs.Name);
                    GUILayout.Space(10);

                    float temp = 0;
                    temp = EditorGUILayout.FloatField(bs.DefaultValue);

                    //If the values are diferent than the users changed the value
                    if (temp != bs.DefaultValue)
                    {
                        BattleStat newStat = new BattleStat(temp, bs.Name);
                        mold.SetAtIndex(i, newStat);                        
                    }

                    GUILayout.EndHorizontal();
                    GUILayout.Space(5);
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();


            movesfoldout = EditorGUILayout.BeginFoldoutHeaderGroup(movesfoldout, "Moves");
            if (movesfoldout)
            {
                //Get list of moves
                if(GUILayout.Button("Add Move"))
                {
                    moves.InsertArrayElementAtIndex(moves.arraySize);
                }         
                for (int i = 0; i < moves.arraySize; i++)
                {
                    SerializedProperty s = moves.GetArrayElementAtIndex(i);
                    EditorGUILayout.PropertyField(s);
                }

            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
