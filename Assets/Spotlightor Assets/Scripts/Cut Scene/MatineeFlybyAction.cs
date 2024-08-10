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

[RequireComponent(typeof(LegacyBezierPath))]
public class MatineeFlybyAction : MatineeAction
{
	public float speed = 1;
	private float percentOnPath = 0;

	protected LegacyBezierPath FlyPath {
		get { return GetComponent<LegacyBezierPath> ();}
	}
	
	public float PercentOnPath {
		set {
			percentOnPath = Mathf.Clamp01 (value);
			FlyPath.PlaceObjectOnPath (target, percentOnPath);
		}
	}
	#region implemented abstract members of MatineeAction
	public override float Progress {
		get {
			return percentOnPath;
		}
	}

	protected override void DoPlay ()
	{
		StopCoroutine ("FlyOnPath");
		StartCoroutine ("FlyOnPath");
	}
	
	private IEnumerator FlyOnPath ()
	{
		PercentOnPath = Progress;
		
		float pathLength = FlyPath.EstimatedLength;
		float pathPercentSpeed = speed / pathLength;
		while (Progress < 1) {
			yield return null;
			PercentOnPath = Progress + pathPercentSpeed * Time.deltaTime;
		}
		
		OnCompleted ();
		
		percentOnPath = 0;
	}

	protected override void DoPause ()
	{
		StopCoroutine ("FlyOnPath");
	}

	protected override void DoStop ()
	{
		StopCoroutine ("FlyOnPath");
		percentOnPath = 0;
	}
	#endregion
}
