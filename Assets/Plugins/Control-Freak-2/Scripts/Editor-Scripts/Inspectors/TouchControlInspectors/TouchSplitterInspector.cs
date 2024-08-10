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
using UnityEditor;
using UnityEngine;

using ControlFreak2Editor;
using ControlFreak2;
using ControlFreak2.Internal;
using ControlFreak2Editor.Internal;

namespace ControlFreak2Editor.Inspectors
{
	
[CustomEditor(typeof(ControlFreak2.TouchSplitter))]
public class TouchSplitterInspector : TouchControlInspectorBase
	{
	public SplitterTargetControlListInspector
		targetControlListInsp;



	// ---------------------
	void OnEnable()
		{
		this.targetControlListInsp = new SplitterTargetControlListInspector(new GUIContent("Target Control List"), (TouchSplitter)this.target); //, typeof(TouchControl), ((TouchSplitter)this.target).targetControlList);

		base.InitTouchControlInspector();
		}

	// ---------------
	public override void OnInspectorGUI()
		{
		TouchSplitter c = (TouchSplitter)this.target;

		this.DrawWarnings(c);			
	
		

		GUILayout.Box(GUIContent.none, CFEditorStyles.Inst.headerTouchSplitter, GUILayout.ExpandWidth(true));

		// Steering Wheel specific inspector....

		InspectorUtils.BeginIndentedSection(new GUIContent("Target Controls"));
			
			this.targetControlListInsp.DrawGUI();

		InspectorUtils.EndIndentedSection();



		// Draw Shared Touch Control Params...

		this.DrawTouchContolGUI(c);
		}
			
	
	// ------------------
	public class SplitterTargetControlListInspector : ObjectListInspector
		{
		public TouchSplitter
			splitter;

		// ----------------
		public SplitterTargetControlListInspector(GUIContent titleContent, TouchSplitter splitter) : 
			base(titleContent, splitter, typeof(TouchControl), splitter.targetControlList)
			{
			this.splitter = splitter;	
			}

		// ---------------
		override protected Object HandleObjectChange(Object originalObj, Object newObj)
			{
			if (newObj == this.splitter)
				return ((originalObj == this.splitter) ? null : originalObj);
				
			if (this.splitter.targetControlList.Contains((TouchControl)newObj))
				return originalObj;
			
			return newObj;
			}	
			
		}



	}

		
}
#endif
