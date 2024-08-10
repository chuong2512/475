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
using System.Collections.Generic;

public class BezierPathOfTransformPoints : BezierPath
{
	public bool pointsByChildren = true;
	[HideByBooleanProperty ("pointsByChildren", true)]
	public List<Transform>
		transformPoints;
	public bool closed = false;
	private bool pointsByChildrenInitialized = false;

	public override List<Vector3> Points {
		get {
			if (pointsByChildren && (!pointsByChildrenInitialized || (Application.isEditor && !Application.isPlaying))) {
				transformPoints = new List<Transform> (){ this.transform };
				for (int i = 0; i < transform.childCount; i++) {
					Transform childTransform = transform.GetChild (i);
					if (childTransform.gameObject.activeSelf)
						transformPoints.Add (childTransform);
				}
				pointsByChildrenInitialized = true;
			}

			List<Vector3> points = new List<Vector3> ();
			for (int i = 0; i < transformPoints.Count; i++)
				points.Add (transformPoints [i].position);
			if (closed)
				points.Add(transformPoints [0].position);

			return points;
		}
	}
}
