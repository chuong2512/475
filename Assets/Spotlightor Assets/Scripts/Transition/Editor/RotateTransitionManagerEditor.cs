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

[CustomEditor (typeof(RotateTransitionManager))]
[CanEditMultipleObjects ()]
public class RotateTransitionManagerEditor : TransitionManagerEditorBase
{
	private RotateTransitionManager Target {
		get { return target as RotateTransitionManager; }
	}

	private Vector3 SerializedRotationIn {
		get { return serializedObject.FindProperty ("inEulerAngles").vector3Value; }
		set { serializedObject.FindProperty ("inEulerAngles").vector3Value = value; }
	}

	private Vector3 SerializedRotationOut {
		get { return serializedObject.FindProperty ("outEulerAngles").vector3Value; }
		set { serializedObject.FindProperty ("outEulerAngles").vector3Value = value; }
	}

	protected override void SetInState ()
	{
		SerializedRotationIn = Target.transform.localEulerAngles;
	}

	protected override void SetOutState ()
	{
		SerializedRotationOut = Target.transform.localEulerAngles;
	}

	protected override void ApplyInState ()
	{
		Undo.RecordObject (Target.transform, "Apply In State");
		Target.transform.localEulerAngles = SerializedRotationIn;
	}

	protected override void ApplyOutState ()
	{
		Undo.RecordObject (Target.transform, "Apply Out State");
		Target.transform.localEulerAngles = SerializedRotationOut;
	}
}
