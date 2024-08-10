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

public class LocalTransformVariator : Variator
{
	[System.Serializable]
	public class LocalTransformVariation : Variation
	{
		public Vector3 localPosition = Vector3.zero;
		public Vector3 localEulerAngles = new Vector3 (0, 0, 0);

		public override void Apply (GameObject target)
		{
			target.GetComponent<LocalTransformVariator> ().target.localPosition = localPosition;
			target.GetComponent<LocalTransformVariator> ().target.localEulerAngles = localEulerAngles;
		}

		public override Object[] GetModifiedObjects (GameObject target)
		{
			return new Object[]{ target.GetComponent<LocalTransformVariator> ().target };
		}
	}

	public Transform target;

	public LocalTransformVariation[] variations;

	public override Variation[] Variations { get { return variations; } }
}
