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

public class AnimationSampleVariator : Variator
{
	[System.Serializable]
	public class AnimationSampleVariation : Variation
	{
		public AnimationClip clip;
		[Range (0, 1)]
		public float normalizedTime = 0;

		public override void Apply (GameObject target)
		{
			AnimationSampleVariator variator = target.GetComponent<AnimationSampleVariator> ();
			if (clip != null)
				clip.SampleAnimation (variator.sampleTarget, clip.length * normalizedTime);
		}

		public override Object[] GetModifiedObjects (GameObject target)
		{
			GameObject sampleTarget = target.GetComponent<AnimationSampleVariator> ().sampleTarget;
			Transform[] sampleTargetChildTransforms = sampleTarget.GetComponentsInChildren<Transform> (true);

			List<Object> objs = new List<Object> ();
			foreach (Transform t in sampleTargetChildTransforms)
				objs.Add (t);
			return objs.ToArray ();
		}
	}

	public AnimationSampleVariation[] variations;
	public GameObject sampleTarget;

	public override Variation[] Variations { get { return variations; } }

	void Reset ()
	{
		if (sampleTarget == null) {
			Animator childAnimator = GetComponentInChildren<Animator> (true);
			if (childAnimator != null)
				sampleTarget = childAnimator.gameObject;
		}
	}
}
