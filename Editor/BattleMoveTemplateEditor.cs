using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEditor;
using Inspector = UnityEditor.Editor;
using System;
using System.Linq;

namespace RpgEssentials.TurnBased.Editor
{
    [CustomEditor(typeof(BattleMoveTemplate), editorForChildClasses: true)]
    public class BattleMoveTemplateEditor : Inspector
    {
        SerializedProperty selectedMold;

        bool isFold;

        string[] molds;
        Type[] types;
        Type moldType;

        BattleMoveTemplate move;

        private void OnEnable()
        {
            selectedMold = serializedObject.FindProperty("selectedMold");

            move = (BattleMoveTemplate)target;

            #region Get Mold Type
            var type = typeof(EntityMold);

            types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && !p.IsAbstract).ToArray();

            molds = types.Select(x => x.Name).ToArray();

            #endregion
        }

        

        public override void OnInspectorGUI()
        {           
            base.OnInspectorGUI();


            serializedObject.Update();

            GUILayout.Space(10);
         
            selectedMold.intValue =
                EditorGUILayout.Popup(selectedMold.intValue, molds);
            moldType = types[selectedMold.intValue];



            #region Battle Stat
            IEnumerable<PropertyInfo> statsInfo =
                moldType.GetProperties().
                    Where(x => x.PropertyType == typeof(BattleStat));

            string[] statsNames = statsInfo.Select(x => x.Name).ToArray();
            #endregion


            isFold = EditorGUILayout.BeginFoldoutHeaderGroup(isFold, "Parameters");

            if (isFold)
            {
                IList<int> list = move.GetParams();
                IList<int> returnList = new List<int> { };
                for (int i = 0; i < list.Count; i++)
                {                     
                    int value = EditorGUILayout.Popup(list[i], statsNames);
                    returnList.Add(value);
                }
                move.SetParams(returnList);
            }

            EditorGUILayout.EndFoldoutHeaderGroup();



            serializedObject.ApplyModifiedProperties();
        }
    }
}
