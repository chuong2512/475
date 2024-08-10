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

[CustomPropertyDrawer(typeof(PopupString))]
public class PopupStringDrawer : PropertyDrawer
{
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		PopupString popupString = attribute as PopupString;

		if (property.propertyType == SerializedPropertyType.String) {
			string stringValue = property.stringValue;
			int selectedIndex = popupString.options.IndexOf (stringValue);
			selectedIndex = EditorGUI.Popup (position, label.text, selectedIndex, popupString.options.ToArray ());
			if (selectedIndex >= 0)
				property.stringValue = popupString.options [selectedIndex];
			else
				property.stringValue = "";
		} else
			EditorGUI.LabelField (position, label.text, "Use PopupString with string.");
	}
}
