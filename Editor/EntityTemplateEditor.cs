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

            EntityTemplate template = target as EntityTemplate;
            EntityMold mold = template.Mold;
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

                    if (template.SerializedStats.Count <= i)
                        template.SerializedStats.Add(new SerializableBattleStat());

                    SerializableBattleStat ent = template.SerializedStats[i];
                    
                    RangedInt templateStat = default;
                    templateStat = CustomEditorGUILayout.RangeIntField(ent.DefaultValue);

                    

                    //If the values are diferent then the users changed the value
                    if (templateStat != template.SerializedStats[i].DefaultValue
                        || templateStat.Flatten != template.SerializedStats[i].DefaultValue.Flatten)
                    {
                        SerializableBattleStat newSerStat = 
                            new SerializableBattleStat(bs.Name, templateStat); 

                        template.SerializedStats[i] = newSerStat;

                        BattleStat newStat = new BattleStat(newSerStat.DefaultValue, bs.Name);
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
