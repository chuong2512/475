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


// ----------------------
// Emulated Mouse Position Binding Inspector.
// ----------------------
public class MousePositionBindingInspector
	{	
	private GUIContent				labelContent;
	private Object					undoObject;

	// ------------------
	public MousePositionBindingInspector(Object undoObject, GUIContent labelContent)
		{	
		this.labelContent			= labelContent;
		this.undoObject				= undoObject;
		}


	// ------------------
	public void Draw( MousePositionBinding bind, InputRig rig)
		{
		bool		bindingEnabled	= bind.enabled;
		int 		priority		= bind.priority;
	
		EditorGUILayout.BeginVertical();
		if (bindingEnabled = EditorGUILayout.ToggleLeft(this.labelContent, bindingEnabled, GUILayout.MinWidth(30)))
			{
			InspectorUtils.BeginIndentedSection();

			priority = EditorGUILayout.IntSlider(new GUIContent("Pos. prio.", "Position priority used to pick mouse position if there is more than one mouse position source in the rig at given frame."),
				priority, 0, 100, GUILayout.MinWidth(30));

			InspectorUtils.EndIndentedSection();

			}
		EditorGUILayout.EndVertical();
		
		
		if ((bindingEnabled != bind.enabled) ||
			(priority		!=	bind.priority) )
			{		
			CFGUI.CreateUndo("Mouse Position Binding modification.", this.undoObject);
			
			bind.enabled		= bindingEnabled;
			bind.priority		= priority;
					
			CFGUI.EndUndo(this.undoObject);
			}

		}
	}

		
}
#endif
