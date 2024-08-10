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

public class MatineeStaticAction : MatineeAction
{
	public float duration;
	private float timeElapsed = 0;
	
	#region implemented abstract members of MatineeAction
	public override float Progress {
		get {
			return timeElapsed / duration;
		}
	}

	protected override void DoPlay ()
	{
		target.transform.position = transform.position;
		target.transform.rotation = transform.rotation;
		
		StopCoroutine ("DelayAndComplete");
		StartCoroutine ("DelayAndComplete");
	}
	
	private IEnumerator DelayAndComplete ()
	{
		while (timeElapsed < duration) {
			yield return null;
			timeElapsed += Time.deltaTime;
		}
		OnCompleted ();
		timeElapsed = 0;
	}

	protected override void DoPause ()
	{
		StopCoroutine ("DelayAndComplete");
	}

	protected override void DoStop ()
	{
		StopCoroutine ("DelayAndComplete");
		timeElapsed = 0;
	}
	#endregion
}
