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
    [CustomEditor(typeof(BattleMoveTemplate))]
    public class BattleMoveTemplateEditor : Inspector
    {
        SerializedProperty selectedMold;
        SerializedProperty param1;

        string[] molds;
        Type[] types;
        Type moldType;

        private void OnEnable()
        {
            selectedMold = serializedObject.FindProperty("selectedMold");
            param1 = serializedObject.FindProperty("param1");


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


         
            selectedMold.intValue =
                EditorGUILayout.Popup(selectedMold.intValue, molds);
            moldType = types[selectedMold.intValue];



            #region Battle Stat
            IEnumerable<PropertyInfo> statsInfo =
                moldType.GetProperties().
                    Where(x => x.PropertyType == typeof(BattleStat));

            string[] statsNames = statsInfo.Select(x => x.Name).ToArray();
            #endregion

            param1.intValue =
                EditorGUILayout.Popup(param1.intValue, statsNames);


            serializedObject.ApplyModifiedProperties();
        }
    }
}
