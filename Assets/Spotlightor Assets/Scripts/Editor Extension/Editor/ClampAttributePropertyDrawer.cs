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

[CustomPropertyDrawer(typeof(ClampAttribute))]
public class ClampAttributePropertyDrawer : PropertyDrawer
{
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		ClampAttribute clamp = attribute as ClampAttribute;

		EditorGUI.PropertyField (position, property, label);

		if (property.propertyType == SerializedPropertyType.Float) {
			property.floatValue = Mathf.Clamp (property.floatValue, clamp.min, clamp.max);
		} else if (property.propertyType == SerializedPropertyType.Integer) {
			property.intValue = Mathf.Clamp (property.intValue, (int)clamp.min, (int)clamp.max);
		}
	}
}
