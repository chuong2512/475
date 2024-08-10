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

[RequireComponent(typeof(RectTransform))]
public class MapPositionToRectTransform : MonoBehaviour
{
	private static Vector2 initialAnchoredPosition = new Vector2 (999999, 999999);
	public Camera sourceCamera;
	public Transform waypoint;

	IEnumerator Start ()
	{
		if (waypoint != null) {
			Canvas canvas = transform.GetComponentInParent<Canvas> ();
			if (canvas != null) {
				GetComponent<RectTransform> ().anchoredPosition = initialAnchoredPosition;

				while (canvas.worldCamera == null)
					yield return null;

				yield return null; // Wait for canvas update

				MapPositionBetweenCameras positionMapper = gameObject.AddComponent<MapPositionBetweenCameras> ();
				positionMapper.sourceCamera = sourceCamera;
				positionMapper.destCamera = canvas.worldCamera;
				positionMapper.waypoint = waypoint;
			} else
				this.LogWarning ("Need a canvas in parent to work!");
		}
	}
}
