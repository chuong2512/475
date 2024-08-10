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

[RequireComponent(typeof(Animation))]
public class AnimationFxAtTime : MonoBehaviour
{
	public abstract class FxSetting
	{
		public AnimationClip animationClip;
		public float normalizedTime = 0;
		public float minWeight = 0.6f;
		public float coolDownTime = 0.2f;
		private float lastFxPlayTime = float.MinValue;

		private bool CoolDownFinished {
			get { return TimeSinceLastPlay > coolDownTime;}
		}

		private float TimeSinceLastPlay {
			get { return Time.time - lastFxPlayTime;}
		}

		public void TryToPlayFx (Animation animation)
		{
			if (CoolDownFinished) {
				string clipName = animationClip.name;
				if (animation.IsPlaying (clipName) && animation [clipName].weight >= minWeight) {
					float currentNormalizedTime = animation [clipName].normalizedTime;
					WrapMode clipWrapMode = animation [clipName].wrapMode;
					if (clipWrapMode == WrapMode.Clamp || clipWrapMode == WrapMode.ClampForever && currentNormalizedTime > 1) {

					} else {
						currentNormalizedTime %= 1;
						float deltaNormalizedTime = Mathf.Abs (currentNormalizedTime - normalizedTime);

						if (deltaNormalizedTime < (1 / 30f) / animationClip.length) {
							PlayFx ();
							lastFxPlayTime = Time.time;
						}
					}
				}
			}
		}

		protected abstract void PlayFx ();
	}

	[System.Serializable]
	public class VfxSetting : FxSetting
	{
		public Transform vfxPrefab;
		public Transform waypoint;
		public bool copyRotation = true;
		public bool addAsChild;

		#region implemented abstract members of FxSetting
		protected override void PlayFx ()
		{
			Transform vfxInstance = Instantiate (vfxPrefab, waypoint.position, copyRotation ? waypoint.rotation : vfxPrefab.transform.rotation) as Transform;
			if (addAsChild)
				vfxInstance.parent = waypoint;
		}
		#endregion
	}

	[System.Serializable]
	public class SfxSetting : FxSetting
	{
		public AudioSource audioSource;
		public AudioClip clip;
		public float volume = 1;

		#region implemented abstract members of FxSetting
		protected override void PlayFx ()
		{
			if (audioSource != null) {
				if (audioSource.enabled && audioSource.gameObject.activeInHierarchy)
					audioSource.PlayOneShot (clip, volume);
			} else
				GlobalSoundPlayer.PlaySound (clip, volume);
		}
		#endregion
	}
	public SfxSetting[] sfxs;
	public VfxSetting[] vfxs;
	
	// Update is called once per frame
	void Update ()
	{
		foreach (SfxSetting sfx in sfxs)
			sfx.TryToPlayFx (GetComponent<Animation>());
		foreach (VfxSetting vfx in vfxs)
			vfx.TryToPlayFx (GetComponent<Animation>());
	}
}
