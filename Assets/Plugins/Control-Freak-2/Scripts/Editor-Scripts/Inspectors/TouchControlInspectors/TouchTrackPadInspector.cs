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
	
[CustomEditor(typeof(ControlFreak2.TouchTrackPad))]
public class TouchTrackPadInspector : TouchControlInspectorBase
	{

	public DigitalBindingInspector
		pressBindingInsp;
		
	public AxisBindingInspector
		horzSwipeBindingInsp,
		vertSwipeBindingInsp;

	private AxisBindingInspector
		touchPressureBindingInsp;
	


	// ---------------------
	void OnEnable()
		{
		this.pressBindingInsp 		= new DigitalBindingInspector(this.target, new GUIContent("Press Binding"));
		this.horzSwipeBindingInsp	= new AxisBindingInspector(this.target, new GUIContent("Horizontal Swipe"), true, InputRig.InputSource.TouchDelta);
		this.vertSwipeBindingInsp	= new AxisBindingInspector(this.target, new GUIContent("Vertical Swipe"), true, InputRig.InputSource.TouchDelta);

		this.touchPressureBindingInsp = new AxisBindingInspector(null, new GUIContent("Touch Pressure Binding"), false, 
			InputRig.InputSource.Analog, this.DrawPressureBindingExtraGUI);
			
		base.InitTouchControlInspector();
		}

	// ---------------
	public override void OnInspectorGUI()
		{
		TouchTrackPad c = (TouchTrackPad)this.target;

		float
			touchSmoothing			= c.touchSmoothing;

		this.emulateTouchPressure = c.emulateTouchPressure;

		GUILayout.Box(GUIContent.none, CFEditorStyles.Inst.headerTrackPad, GUILayout.ExpandWidth(true));


		this.DrawWarnings(c);			

			

		//Trackpad inspector....

		InspectorUtils.BeginIndentedSection(new GUIContent("TrackPad Settings"));

		touchSmoothing = CFGUI.Slider(new GUIContent("Touch smoothing", "Amount of smoothing applied to touch position. "),
			touchSmoothing, 0, 1,  110);

		InspectorUtils.EndIndentedSection();


		InspectorUtils.BeginIndentedSection(new GUIContent("TrackPad Bindings"));

	
			this.pressBindingInsp.Draw(c.pressBinding, c.rig);
			this.touchPressureBindingInsp.Draw(c.touchPressureBinding, c.rig);
			this.horzSwipeBindingInsp.Draw(c.horzSwipeBinding, c.rig);
			this.vertSwipeBindingInsp.Draw(c.vertSwipeBinding, c.rig);
	
		InspectorUtils.EndIndentedSection();


		// Register Undo...

		if ((touchSmoothing				!= c.touchSmoothing) ||
			(this.emulateTouchPressure != c.emulateTouchPressure) )
			{
			CFGUI.CreateUndo("Dynamic Touch Control modification.", c);

			c.SetTouchSmoothing(touchSmoothing);

			c.emulateTouchPressure	= this.emulateTouchPressure;

			CFGUI.EndUndo(c);
			}


		// Draw Shared Control Params...

		this.DrawTouchContolGUI(c);


	

		}
			
	
	

	}

		
}
#endif
