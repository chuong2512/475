/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class TransitionManagerEditorBase : Editor
{
	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector ();

		GUILayoutOption setButtonLayoutOption = GUILayout.Width (50);
		GUILayoutOption transitionButtonLayoutOption = GUILayout.Width (100);

		GUILayout.BeginHorizontal ();
		EditorGUILayout.LabelField ("Set State:", GUILayout.Width (110));
		if (GUILayout.Button ("In =", setButtonLayoutOption))
			SetInState ();
		if (GUILayout.Button ("Out =", setButtonLayoutOption))
			SetOutState ();
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		EditorGUILayout.LabelField ("Transition:", GUILayout.Width (110));
		if (GUILayout.Button ("Transition In", transitionButtonLayoutOption))
			ApplyInState ();
		if (GUILayout.Button ("Transition Out", transitionButtonLayoutOption))
			ApplyOutState ();
		GUILayout.EndHorizontal ();

		serializedObject.ApplyModifiedProperties ();
	}

	protected abstract void SetInState ();

	protected abstract void SetOutState ();

	protected abstract void ApplyInState ();

	protected abstract void ApplyOutState ();
}
