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

[CustomPropertyDrawer (typeof(StepperInt))]
public class StepperIntPropertyDrawer : PropertyDrawer
{
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
	{
		StepperInt stepperInt = attribute as StepperInt;

		EditorGUI.BeginProperty (position, label, property);
		EditorGUI.BeginChangeCheck ();
		int newValue = property.intValue;

		Rect labelRect = EditorGUI.PrefixLabel (position, label);

		Rect amountRect = new Rect (labelRect.x, position.y, 40, position.height);
		newValue = EditorGUI.IntField (amountRect, property.intValue);

		Rect addRect = new Rect (amountRect.xMax + 5, position.y, 30, position.height);
		if (GUI.Button (addRect, "+"))
			newValue += stepperInt.step;
		
		Rect minusRect = new Rect (addRect.xMax + 5, position.y, 30, position.height);
		if (GUI.Button (minusRect, "-"))
			newValue -= stepperInt.step;

		if (EditorGUI.EndChangeCheck ())
			property.intValue = Mathf.Clamp (newValue, stepperInt.min, stepperInt.max);
		
		EditorGUI.EndProperty ();
	}
}
