using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Reflection;

namespace RpgEssentials.TurnBased
{
    [CustomPropertyDrawer(typeof(ConditionPacket))]
    public class ConditionPacketsDrawer : PropertyDrawer
    {
        private bool initialized = false;

        SerializedProperty conditionIndex;
        SerializedProperty sizeMulti;

        ConditionPacket target;


        string[] conditionList;
        Type[] conditions;
        

        

        private void Init(SerializedProperty property)
        {
            initialized = true;
         
            SetConsitionsNameList();
            SetupChosenCondition();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            sizeMulti =
              property.FindPropertyRelative("sizeMulti");

            return base.GetPropertyHeight(property, label) * (sizeMulti.intValue + 1);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {

        #if UNITY_2022
            
            target =
                (ConditionPacket)property.boxedValue;
            
        #else
            
            target =
                (ConditionPacket)EditorHelper.GetTargetObjectOfProperty(property);

        #endif

            sizeMulti = 
              property.FindPropertyRelative("sizeMulti");

            conditionIndex =
              property.FindPropertyRelative("conditionIndex");

            if (target == null)
                return;

            if (!initialized)
                Init(property);          

            //Setup size of a editor unit 
            Vector2 size = new Vector2( position.size.x,
                    position.size.y / (1 + sizeMulti.intValue));
            Vector2 pos = position.position;
            Rect currentRect = new Rect(pos,size);


            int previous = conditionIndex.intValue;
            conditionIndex.intValue = 
                EditorGUI.Popup(currentRect, "Condition" ,
                    conditionIndex.intValue, conditionList);
            

            if(conditionIndex.intValue != previous)
            {
                SetupChosenCondition();
            }

            if(target.intList == null)
            {
                SetupChosenCondition();
            }

            #region StatCondition Implementation
            if(conditions[conditionIndex.intValue] == typeof(StatCondition))
            {
                int index = 0;
                SerializedProperty intHelperList =
                         property.FindPropertyRelative("values");
                
                if(intHelperList.arraySize == 0)
                    for (int i = 0; i < sizeMulti.intValue; i++)
                    {
                        intHelperList.InsertArrayElementAtIndex(0);
                    }

                //Select Some Mold
                

                foreach (FieldInfo i in target.intList)
                {
                    Vector2 newPos = new Vector2(currentRect.position.x,
                            currentRect.position.y + size.y);
                    currentRect = new Rect(newPos, size);

                    SerializedProperty prop = 
                            intHelperList.GetArrayElementAtIndex(index);

                    prop.intValue
                            = EditorGUI.IntField(currentRect, i.Name, prop.intValue);
                    index++;
                }
            }
            #endregion

        }

        private void SetupChosenCondition()
        {
            //Get List of properties
            Type currentCondition = conditions[conditionIndex.intValue];

            List<FieldInfo> intList =
                currentCondition.GetFields(
                    BindingFlags.Instance |
                    BindingFlags.Public |
                    BindingFlags.NonPublic)
                    .Where(x => x.FieldType == typeof(int)).ToList();

            //Get new size
            sizeMulti.intValue = intList.Count;
            target.intList = intList;
        }

        private void SetConsitionsNameList()
        {
            //Create Name List of Possible Conditions                    
            var type = typeof(ICondition);
            
            //Using Reflection get all classes that implement ICondition
            conditions = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p)
                    && !p.IsInterface && !p.IsAbstract).ToArray();

            conditionList = conditions.Select(x => x.Name).ToArray();
        }
    }
}