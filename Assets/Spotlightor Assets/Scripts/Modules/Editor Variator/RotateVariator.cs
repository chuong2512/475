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

public class RotateVariator : Variator
{
	[System.Serializable]
	public class RotateVariation : Variation
	{
		public Vector3 rotation = new Vector3 (0, 90, 0);

		public override void Apply (GameObject target)
		{
			target.transform.Rotate (rotation, Space.Self);
		}

		public override Object[] GetModifiedObjects (GameObject target)
		{
			return new Object[]{ target.transform };
		}
	}

	public RotateVariation[] variations;

	public override Variation[] Variations { get { return variations; } }

	void Reset ()
	{
		if (variations == null || variations.Length == 0) {
			List<RotateVariation> defaultVariations = new List<RotateVariation> ();
			Vector3[] defaultRotations = new Vector3[]{ Vector3.up * 90, Vector3.up * -90, Vector3.up * 180 };
			for (int i = 0; i < defaultRotations.Length; i++) {
				RotateVariation variation = new RotateVariation ();
				variation.rotation = defaultRotations [i];
				variation.name = defaultRotations [i].y.ToString ();
				defaultVariations.Add (variation);
			}

			variations = defaultVariations.ToArray ();
		}
	}
}
