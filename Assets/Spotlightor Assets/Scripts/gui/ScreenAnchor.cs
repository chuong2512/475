/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class ScreenAnchor : MonoBehaviour
{
	public enum AlignTypes
	{
		TopLeft,
		Top,
		TopRight,
		Left,
		Right,
		BottomLeft,
		Bottom,
		BottomRight,
	}
	public AlignTypes alignType;
	private Camera parentCamera;

	private Camera ParentCamera {
		get {
			if (parentCamera == null)
				parentCamera = transform.GetComponentInParent<Camera> ();
			return parentCamera;
		}
	}
	
	void Update ()
	{
		if (ParentCamera != null) {
			float viewportX = 0;
			if (alignType == AlignTypes.Top || alignType == AlignTypes.Bottom)
				viewportX = 0.5f;
			else if (alignType == AlignTypes.BottomRight || alignType == AlignTypes.Right || alignType == AlignTypes.TopRight)
				viewportX = 1f;
			
			float viewportY = 0;
			if (alignType == AlignTypes.Left || alignType == AlignTypes.Right)
				viewportY = 0.5f;
			else if (alignType == AlignTypes.Top || alignType == AlignTypes.TopLeft || alignType == AlignTypes.TopRight)
				viewportY = 1f;
			
			float viewportZ = ParentCamera.WorldToViewportPoint (transform.position).z;
			
			Vector3 targetPosition = ParentCamera.ViewportToWorldPoint (new Vector3 (viewportX, viewportY, viewportZ));
			if (targetPosition != transform.position) 
				transform.position = targetPosition;
		}
	}
}
