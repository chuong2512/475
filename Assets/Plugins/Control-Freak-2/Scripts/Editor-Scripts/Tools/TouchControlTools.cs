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

using System.Collections.Generic;

using ControlFreak2;
using ControlFreak2.Internal;


namespace ControlFreak2Editor
{

static public class TouchControlTools
	{
	// ---------------
	[MenuItem("GameObject/CF2 Transform Tools/Unify selection's depth (Move to front)")]
	static public void UnifySelectionDepthFront()
		{ ModifyTransforms(Selection.transforms, ControlTransformMode.UnifyDepthFront); }	

	// ---------------
	[MenuItem("GameObject/CF2 Transform Tools/Unify selection's depth (Move to center)")]
	static public void UnifySelectionDepthCenter()
		{ ModifyTransforms(Selection.transforms, ControlTransformMode.UnifyDepthCenter); }	

	// ---------------
	[MenuItem("GameObject/CF2 Transform Tools/Unify selection's depth (Move to back)")]
	static public void UnifySelectionDepthBack()
		{ ModifyTransforms(Selection.transforms, ControlTransformMode.UnifyDepthBack); }	

	// ---------------
	[MenuItem("GameObject/CF2 Transform Tools/Round Depth to nearest integer")]
	static public void SnapSelectionDepth()
		{ ModifyTransforms(Selection.transforms, ControlTransformMode.SnapDepth); }	



	// -------------------
	public enum ControlTransformMode	
		{
		SnapDepth,

		UnifyDepthFront,
		UnifyDepthCenter,
		UnifyDepthBack
		}



	// ------------------------
	static public void ModifyTransforms(Transform[] trList, ControlTransformMode mode)
		{
		if (trList.Length <= 0)
			{
			Debug.Log("No touch controls selected...");
			return; 
			}					

	
		Bounds bb = new Bounds();
		
		for (int i = 0; i < trList.Length; ++i)
			{
			Transform t = trList[i];
	
			if (i == 0)
				bb.SetMinMax(t.position, t.position);
			else
				bb.Encapsulate(t.position);
			}



		CFGUI.CreateUndoMulti("Modify Transforms : " + mode, trList);

		for (int i = 0; i < trList.Length; ++i)
			{
			Transform t = trList[i];	
			Vector3 pos = t.position;

			switch (mode)
				{
				case ControlTransformMode.SnapDepth :
					pos.z = Mathf.Round(pos.z);
					break;

				case ControlTransformMode.UnifyDepthBack : 	
					pos.z = bb.min.z;
					break;

				case ControlTransformMode.UnifyDepthCenter : 	
					pos.z = bb.center.z;
					break;

				case ControlTransformMode.UnifyDepthFront :
					pos.z = bb.max.z;
					break;	
				
				}
	
			t.position = pos;
			}
		
		CFGUI.EndUndoMulti(trList);
		}


	}
}

#endif
