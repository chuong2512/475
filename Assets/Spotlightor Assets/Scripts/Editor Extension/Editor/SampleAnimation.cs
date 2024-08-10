/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using UnityEditor;
using System.Collections;

public class SampleAnimation : ScriptableWizard
{
	public AnimationClip animationClip;
	[Range (0, 1)]
	public float normalizedTime = 0;

	[MenuItem ("Spotlightor/Animation/Sample")]
	public static void CreateWizard ()
	{
		ScriptableWizard.DisplayWizard ("Sample Animation", typeof(SampleAnimation), "Sample");
	}

	void OnWizardCreate ()
	{
		SampleAnimationByWizard ();
	}

	void OnWizardUpdate ()
	{
		SampleAnimationByWizard ();
	}

	private void SampleAnimationByWizard ()
	{
		if (Selection.activeGameObject != null) {
			if (animationClip != null)
				SampleAnimationAtNormalizedTime (Selection.activeGameObject, animationClip, normalizedTime);
		} else
			this.LogWarning ("You need to select GO with Animation.");
	}

	[MenuItem ("Spotlightor/Animation/Sample 0")]
	static void SampleTheAnimation0 ()
	{
		SampleAnimationAtNormalizedTime (Selection.activeGameObject, GetActiveGameObjectAnimationClip (Selection.activeGameObject), 0f);
	}

	[MenuItem ("Spotlightor/Animation/Sample 1")]
	static void SampleTheAnimation1 ()
	{
		SampleAnimationAtNormalizedTime (Selection.activeGameObject, GetActiveGameObjectAnimationClip (Selection.activeGameObject), 1f);
	}

	[MenuItem ("Spotlightor/Animation/Sample 0.5")]
	static void SampleTheAnimationHalf ()
	{
		SampleAnimationAtNormalizedTime (Selection.activeGameObject, GetActiveGameObjectAnimationClip (Selection.activeGameObject), 0.5f);
	}

	[MenuItem ("Spotlightor/Animation/Sample 0.25")]
	static void SampleTheAnimationQuarter ()
	{
		SampleAnimationAtNormalizedTime (Selection.activeGameObject, GetActiveGameObjectAnimationClip (Selection.activeGameObject), 0.25f);
	}

	private static AnimationClip GetActiveGameObjectAnimationClip (GameObject activeGo)
	{
		AnimationClip clip = null;
		Animation anim = activeGo.GetComponent<Animation> ();
		if (anim != null)
			clip = anim.clip;
		else {
			Animator animator = activeGo.GetComponent<Animator> ();
			if (animator != null && animator.runtimeAnimatorController != null)
				clip = animator.runtimeAnimatorController.animationClips [0];
		}
		return clip;
	}

	private static void SampleAnimationAtNormalizedTime (GameObject target, AnimationClip clip, float normalizedTime)
	{
		#if UNITY_5_3_OR_NEWER
		clip.SampleAnimation (target, normalizedTime * clip.length);
		#else
		target.SampleAnimation (clip, clip.length * normalizedTime);
		#endif
	}
}
