/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using UnityEditor;
using System.Collections;

public abstract class HideByPropertyDrawer : PropertyDrawer
{
	public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
	{
		return IsHidden (property) ? 0 : base.GetPropertyHeight (property, label);
	}

	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		if (!IsHidden (property))
			EditorGUI.PropertyField (position, property, label);
	}

	private bool IsHidden (SerializedProperty property)
	{
		HideByProperty hideAttribute = attribute as HideByProperty;
		SerializedProperty hiderProperty = property.serializedObject.FindProperty (hideAttribute.propertyName);
		return IsHiddenBy (hiderProperty);
	}

	protected abstract bool IsHiddenBy (SerializedProperty hiderProperty);
}
