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
	
[CustomEditor(typeof(ControlFreak2.TouchTrackPadSpriteAnimator))]
public class TouchTrackPadSpriteAnimatorInspector : UnityEditor.Editor
	{
	private SpriteConfigInspector
		spriteNeutral,
		spritePressed;
	
	


	// ---------------------
	void OnEnable()
		{
		this.spriteNeutral 				= new SpriteConfigInspector(new GUIContent("Neutral State", "Neutral Sprite and Color"));
		this.spritePressed 				= new SpriteConfigInspector(new GUIContent("Pressed State", "Pressed Sprite and Color"));
		
		}

		
	// ---------------
	public override void OnInspectorGUI()
		{

		TouchTrackPadSpriteAnimator target = this.target as TouchTrackPadSpriteAnimator;
		if ((target == null))
			return;
			
		if (!TouchControlSpriteAnimatorInspector.DrawSourceGUI(target))
			return;

//		TouchControlSpriteAnimatorInspector.DrawTimingGUI(target);
			
		

		InspectorUtils.BeginIndentedSection(new GUIContent("Sprite Settings"));

			this.spriteNeutral.Draw(target.spriteNeutral, target, true, false);

			EditorGUILayout.Space();
			this.spritePressed.Draw(target.spritePressed, target, target.IsIllegallyAttachedToSource());
				

		InspectorUtils.EndIndentedSection();
			
//
//		TouchControlSpriteAnimatorInspector.DrawDefaultTransformGUI(target);
		
		}

	
	
	}

		
}
#endif
