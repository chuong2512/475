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
	
[CustomEditor(typeof(ControlFreak2.TouchJoystickSpriteAnimator))]
public class TouchJoystickSpriteAnimatorInspector : UnityEditor.Editor
	{
	private SpriteConfigInspector
		spriteNeutral,
		spriteNeutralPressed,
		spriteUp, 
		spriteUpRight, 
		spriteRight,
		spriteDownRight, 
		spriteDown, 
		spriteDownLeft, 
		spriteLeft, 
		spriteUpLeft; 	
	


	// ---------------------
	void OnEnable()
		{
		this.spriteNeutral 			= new SpriteConfigInspector(new GUIContent("Neutral Released State", 	"Neutral Released Sprite and Color"));
		this.spriteNeutralPressed	= new SpriteConfigInspector(new GUIContent("Neutral Pressed State", 	"Neutral Pressed Sprite and Color"));
		this.spriteUp 				= new SpriteConfigInspector(new GUIContent("Up State",					"Up Sprite and Color"));
		this.spriteUpRight 			= new SpriteConfigInspector(new GUIContent("Up-Right State",			"Up-Right Sprite and Color"));
		this.spriteRight 			= new SpriteConfigInspector(new GUIContent("Right State",				"Right Sprite and Color"));
		this.spriteDownRight 		= new SpriteConfigInspector(new GUIContent("Down-Right State",			"Down-Right Sprite and Color"));
		this.spriteDown 			= new SpriteConfigInspector(new GUIContent("Down State",				"Down Sprite and Color"));
		this.spriteDownLeft 		= new SpriteConfigInspector(new GUIContent("Down-Left State",			"Down-Left Sprite and Color"));
		this.spriteLeft 			= new SpriteConfigInspector(new GUIContent("Left State", 				"Left Sprite and Color"));
		this.spriteUpLeft 			= new SpriteConfigInspector(new GUIContent("Up-Left State",				"Up-Left Sprite and Color"));
		
		; 	
	
		}

		
	// ---------------
	public override void OnInspectorGUI()
		{

		TouchJoystickSpriteAnimator target = this.target as TouchJoystickSpriteAnimator;
		if ((target == null))
			return;


			
		if (!TouchControlSpriteAnimatorInspector.DrawSourceGUI(target))
			return;


		TouchJoystickSpriteAnimator.SpriteMode 
			spriteMode 				= target.spriteMode;
		TouchJoystickSpriteAnimator.RotationMode
			rotationMode 			= target.rotationMode;
		float
			simpleRotationRange 	= target.simpleRotationRange,
			rotationSmoothingTime 	= target.rotationSmoothingTime,
			translationSmoothingTime= target.translationSmoothingTime;
		Vector2
			moveScale 				= target.moveScale;
		bool
			animateTransl 			= target.animateTransl;


		if (target.sourceControl == null)
			{} 
		else
			{	
			InspectorUtils.BeginIndentedSection(new GUIContent("Sprite Settings"));
	
			spriteMode = (TouchJoystickSpriteAnimator.SpriteMode)EditorGUILayout.EnumPopup(new GUIContent("Sprite Mode", ""), 
				spriteMode, GUILayout.MinWidth(30), GUILayout.ExpandWidth(true));

			this.spriteNeutral.Draw(target.spriteNeutral, target, true, false);
				
			EditorGUILayout.Space();		this.spriteNeutralPressed.Draw(target.spriteNeutralPressed, target, target.IsIllegallyAttachedToSource());

			if (spriteMode >= TouchJoystickSpriteAnimator.SpriteMode.FourWay)
				{
				EditorGUILayout.Space();		this.spriteUp.Draw(target.spriteUp, target, target.IsIllegallyAttachedToSource());

				if (spriteMode >= TouchJoystickSpriteAnimator.SpriteMode.EightWay)
					{
					EditorGUILayout.Space();	this.spriteUpRight.Draw(target.spriteUpRight, target, target.IsIllegallyAttachedToSource());
					}

				EditorGUILayout.Space();		this.spriteRight.Draw(target.spriteRight, target, target.IsIllegallyAttachedToSource());

				if (spriteMode >= TouchJoystickSpriteAnimator.SpriteMode.EightWay)
					{
					EditorGUILayout.Space();	this.spriteDownRight.Draw(target.spriteDownRight, target, target.IsIllegallyAttachedToSource());
					}

				EditorGUILayout.Space();		this.spriteDown.Draw(target.spriteDown, target, target.IsIllegallyAttachedToSource());

				if (spriteMode >= TouchJoystickSpriteAnimator.SpriteMode.EightWay)
					{
					EditorGUILayout.Space();	this.spriteDownLeft.Draw(target.spriteDownLeft, target, target.IsIllegallyAttachedToSource());
					}

				EditorGUILayout.Space();		this.spriteLeft.Draw(target.spriteLeft, target, target.IsIllegallyAttachedToSource());

				if (spriteMode >= TouchJoystickSpriteAnimator.SpriteMode.EightWay)
					{
					EditorGUILayout.Space();	this.spriteUpLeft.Draw(target.spriteUpLeft, target, target.IsIllegallyAttachedToSource());
					}


				} 

			InspectorUtils.EndIndentedSection();
			}	
			

		InspectorUtils.BeginIndentedSection(new GUIContent("Transform Animation Settings"));


			
	
			animateTransl = EditorGUILayout.ToggleLeft(new GUIContent("Animate Translation"), 
				animateTransl, GUILayout.MinWidth(30), GUILayout.ExpandWidth(true));
	
			if (animateTransl)
				{
				CFGUI.BeginIndentedVertical();
					
					moveScale.x = CFGUI.Slider(new GUIContent("X Move. Scale", "Horizontal Movement scale"), moveScale.x, 0, 2, 50);
					moveScale.y = CFGUI.Slider(new GUIContent("Y Move. Scale", "Vertical Movement scale"), moveScale.y, 0, 2, 50);
	
				translationSmoothingTime = CFGUI.FloatFieldEx(new GUIContent("Smooting Time (ms)", "Translation Smooting Time in milliseconds"), 
					translationSmoothingTime, 0, 10, 1000, true, 120);

				CFGUI.EndIndentedVertical();
				}
	
	
			EditorGUILayout.Space();
	
			rotationMode = (TouchJoystickSpriteAnimator.RotationMode)CFGUI.EnumPopup(new GUIContent("Rotation Animation Mode", "How rotation should be animated..."),
				rotationMode, 150);
	
			if (rotationMode != TouchJoystickSpriteAnimator.RotationMode.Disabled)
				{	
				CFGUI.BeginIndentedVertical();
		
				if ((rotationMode == TouchJoystickSpriteAnimator.RotationMode.SimpleHorizontal) || 
					(rotationMode == TouchJoystickSpriteAnimator.RotationMode.SimpleVertical))
					{
					simpleRotationRange = CFGUI.Slider(new GUIContent("Rotation Range"), 
						simpleRotationRange, -360, 360, 120);
					}
	
				rotationSmoothingTime = CFGUI.FloatFieldEx(new GUIContent("Smooting Time (ms)", "Rotation Smooting Time in milliseconds"), 
					rotationSmoothingTime, 0, 10, 1000, true, 120);
			
				CFGUI.EndIndentedVertical();
				}

			EditorGUILayout.Space();
				
//			TouchControlSpriteAnimatorInspector.DrawScaleGUI(target);

				

		InspectorUtils.EndIndentedSection();
		

			
		// Register Undo...

		if ((spriteMode 			!= target.spriteMode) ||
			(simpleRotationRange 	!= target.simpleRotationRange) ||
			(rotationSmoothingTime 	!= target.rotationSmoothingTime) ||
			(translationSmoothingTime 	!= target.translationSmoothingTime) ||
			(rotationMode 			!= target.rotationMode) ||
			(moveScale 				!= target.moveScale) ||
			(animateTransl 			!= target.animateTransl) )
			{
			CFGUI.CreateUndo("Joystick Sprite Animator modification", target);

	

			target.spriteMode				= spriteMode;
			target.simpleRotationRange		= simpleRotationRange;
			target.rotationSmoothingTime	= rotationSmoothingTime;
			target.translationSmoothingTime	= translationSmoothingTime;
			target.rotationMode				= rotationMode;
			target.moveScale				= moveScale;
			target.animateTransl			= animateTransl;


			CFGUI.EndUndo(target);
			}
	
		

		
		}

	
	
	}

		
}
#endif
