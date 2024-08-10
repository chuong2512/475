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

[CustomPropertyDrawer(typeof(SingleLineLabel))]
public class SingleLineLabelDrawer : PropertyDrawer
{
	public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
	{
		return base.GetPropertyHeight (property, label) * 2;
	}

	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		Rect labelPosition = position;
		labelPosition.height *= 0.5f;

		EditorGUI.LabelField (labelPosition, label);

		Rect propertyPosition = position;
		propertyPosition.height *= 0.5f;
		propertyPosition.y = labelPosition.yMax;

		EditorGUI.PropertyField (propertyPosition, property, GUIContent.none);
	}
}
