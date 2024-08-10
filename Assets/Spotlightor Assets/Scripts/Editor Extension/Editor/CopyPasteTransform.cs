/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class CopyPasteTransform : ScriptableObject
{
	private static Vector3 copiedPosition = Vector3.zero;
	private static Quaternion copiedRotation = Quaternion.identity;
	private static Vector3 copiedLocalPosition = Vector3.zero;
	private static Quaternion copiedLocalRotation = Quaternion.identity;

	[MenuItem ("GameObject/Copy Transform %#c", false, 200)]
	static void CopyTransform ()
	{
		if (Selection.activeGameObject != null) {
			copiedPosition = Selection.activeGameObject.transform.position;
			copiedRotation = Selection.activeGameObject.transform.rotation;
			Debug.Log (string.Format ("Position and Rotation of {0} copied.", Selection.activeObject.name));
		}
	}

	[MenuItem ("GameObject/Paste Transform %#v", false, 201)]
	static void PasteTransform ()
	{
		Transform[] transforms = Selection.transforms;
		Undo.RecordObjects (transforms, "Paste transform");
		foreach (Transform t in transforms) {
			t.position = copiedPosition;
			t.rotation = copiedRotation;
			Debug.Log (string.Format ("Position and Rotation of {0} set by copied transform.", t.name));
		}
	}

	[MenuItem ("GameObject/Copy Local Transform %#&c", false, 202)]
	static void CopyLocalTransform ()
	{
		if (Selection.activeGameObject != null) {
			copiedLocalPosition = Selection.activeGameObject.transform.localPosition;
			copiedLocalRotation = Selection.activeGameObject.transform.localRotation;
			Debug.Log (string.Format ("Local Position and Rotation of {0} copied.", Selection.activeObject.name));
		}
	}

	[MenuItem ("GameObject/Paste Local Transform %#&v", false, 203)]
	static void PasteLocalTransform ()
	{
		Transform[] transforms = Selection.transforms;
		
		Undo.RecordObjects (transforms, "Paste local transform");
		foreach (Transform t in transforms) {
			t.transform.localPosition = copiedLocalPosition;
			t.transform.localRotation = copiedLocalRotation;
			Debug.Log (string.Format ("Local Position and Rotation of {0} set by copied transform.", t.name));
		}
	}
}
