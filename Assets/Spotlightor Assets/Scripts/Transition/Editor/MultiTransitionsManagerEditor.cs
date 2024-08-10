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

[CustomEditor(typeof(MultiTransitionsManager))]
[CanEditMultipleObjects()]
public class MultiTransitionsManagerEditor : Editor
{
	private MultiTransitionsManager Target {
		get { return target as MultiTransitionsManager;}
	}

	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector ();
		
		GUILayoutOption activeChildrenButtonLayout = GUILayout.Width (100);
		GUILayout.BeginHorizontal ();
		EditorGUILayout.LabelField ("Children:", GUILayout.Width (100));
		if (GUILayout.Button ("Active", activeChildrenButtonLayout)) 
			Target.childTransitions.ForEach (t => t.gameObject.SetActive (true));
		if (GUILayout.Button ("Deactive", activeChildrenButtonLayout)) 
			Target.childTransitions.ForEach (t => t.gameObject.SetActive (false));
		GUILayout.EndHorizontal ();
	}
}
