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
	
[CustomEditor(typeof(DynamicRegion))]
public class DynamicRegionInspector : TouchControlInspectorBase
	{

	// ---------------------
	public DynamicRegionInspector() : base()
		{
		}



	// ---------------------
	void OnEnable()
		{
		base.InitTouchControlInspector();
		}


	// ---------------
	public override void OnInspectorGUI()
		{
		DynamicRegion c = (DynamicRegion)this.target;
			
		GUILayout.Box(GUIContent.none, CFEditorStyles.Inst.headerDynamicRegion, GUILayout.ExpandWidth(true));

		this.DrawWarnings(c);			

		DynamicTouchControl tc = c.GetTargetControl();
	
		if (tc == null)
			{
			EditorGUILayout.HelpBox("There's no control linked to this Dynamic Region!", MessageType.Warning);
			}
		else
			{
			EditorGUILayout.HelpBox("Region linked to [" + tc.name + "]!", MessageType.Info);
			}

		this.DrawTouchContolGUI(c);
			
		}
			
	
	


	

	}

		
}
#endif
