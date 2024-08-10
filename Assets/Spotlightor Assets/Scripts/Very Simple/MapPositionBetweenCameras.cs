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

public class MapPositionBetweenCameras : MonoBehaviour
{
	[Header("Auto set to MainCamera if NULL")]
	public Camera
		sourceCamera;
	[Header("Auto set to parent Camera if NULL")]
	public Camera
		destCamera;
	public Transform waypoint;
	private float zDepth;

	void Start ()
	{
		if (sourceCamera == null)
			sourceCamera = Camera.main;
		if (destCamera == null)
			destCamera = GetComponentInParent<Camera> ();

		zDepth = destCamera.transform.InverseTransformPoint (transform.position).z;
	}

	void LateUpdate ()
	{
		if (sourceCamera != null && destCamera != null && waypoint != null) {
			Vector3 waypointScreenPosition = sourceCamera.WorldToScreenPoint (waypoint.position);

			if (waypointScreenPosition.z > 0) {
				waypointScreenPosition.z = zDepth;
				transform.position = destCamera.ScreenToWorldPoint (waypointScreenPosition);
			} else 
				transform.position = destCamera.ScreenToWorldPoint (waypointScreenPosition);
		}
	}
}
