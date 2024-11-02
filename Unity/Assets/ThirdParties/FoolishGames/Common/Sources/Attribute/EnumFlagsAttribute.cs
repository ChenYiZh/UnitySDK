using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FoolishGames.Attribute
{
    /// <summary>
    /// 多选枚举
    /// 枚举不能默认为1
    /// </summary>
    public class EnumFlagsAttribute : PropertyAttribute
    {
    }
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(EnumFlagsAttribute))]
    public class EnumFlagsDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.intValue = EditorGUI.MaskField(position, label, property.intValue
                , property.enumNames);
        }
    }
#endif
}