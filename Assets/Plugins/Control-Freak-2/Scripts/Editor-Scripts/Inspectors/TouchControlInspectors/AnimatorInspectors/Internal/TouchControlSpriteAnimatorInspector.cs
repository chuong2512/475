/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

// -------------------------------------------
// Control Freak 2
// Copyright (C) 2013-2018 Dan's Game Tools
// http://DansGameTools.blogspot.com
// -------------------------------------------

#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

using ControlFreak2.Internal;
using ControlFreak2;

namespace ControlFreak2Editor.Inspectors
{
public class TouchControlSpriteAnimatorInspector 
	{
	
	// -------------------
	static public bool DrawSourceGUI(TouchControlSpriteAnimatorBase target)
		{
		bool 
			autoConnect 		= target.autoConnectToSource;
		TouchControl	
			sourceControl 		= target.sourceControl;
			

		bool initiallyEnabled = GUI.enabled;	

		InspectorUtils.BeginIndentedSection(new GUIContent("Source Control Connection"));

		GUI.enabled = (sourceControl != null);
		if (GUILayout.Button(new GUIContent("Select Source Control"), GUILayout.ExpandWidth(true), GUILayout.Height(20)))
			{
			Selection.activeObject = sourceControl;
			return false;	
			}		

		GUI.enabled = initiallyEnabled;

		autoConnect = EditorGUILayout.ToggleLeft(new GUIContent("Auto Connect To Control", "When enabled, this animator will automatically pick source control whenever this gameobject's hierarchy changes."), 
			autoConnect, GUILayout.MinWidth(30), GUILayout.ExpandWidth(true));

		if (autoConnect)
			GUI.enabled = false;

		sourceControl = (TouchControl)EditorGUILayout.ObjectField(new GUIContent("Source Control"), sourceControl, target.GetSourceControlType(), true,
			GUILayout.MaxWidth(30), GUILayout.ExpandWidth(true));		
	
		GUI.enabled = initiallyEnabled; 

		if (sourceControl == null)
			InspectorUtils.DrawErrorBox("Source Control is not connected!");
		else if (target.IsIllegallyAttachedToSource())
			InspectorUtils.DrawErrorBox("This Animator is attached to the source control's game object!!\nTransformation Animation will not be possible!!");

		InspectorUtils.EndIndentedSection();


		// Register Undo...

		if ((autoConnect != target.autoConnectToSource) ||
			(sourceControl != target.sourceControl))
			{
			CFGUI.CreateUndo("Sprite Animator Source modification", target);

			target.autoConnectToSource 	= autoConnect;
			target.SetSourceControl(sourceControl);
				
			if (target.autoConnectToSource)
				target.AutoConnectToSource();

			CFGUI.EndUndo(target);
			}

		return true;		
		}
	
	
	}
}

#endif
