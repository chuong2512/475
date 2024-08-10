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

public static class AudioSourceExtensionMethods
{
	public static void Play(this AudioSource audioSource, AudioClipPlaySetting clipPlaySetting)
	{
		clipPlaySetting.SetupAudioSource(audioSource);
		audioSource.Play();
	}

	public static void PlayOneShot (this AudioSource audioSource, AudioClipPlaySetting clipPlaySetting)
	{
		audioSource.PlayOneShot (clipPlaySetting.ClipToPlay, clipPlaySetting.volumeScale, clipPlaySetting.pitch);
	}

	public static void PlayOneShot (this AudioSource audioSource, AudioClip clip, float volumeScale, float pitch)
	{
		if (clip != null) {
			if (pitch != 1) {
				AudioSource tempAudioSource = new GameObject ("temp audio").AddComponent<AudioSource> ();
				tempAudioSource.transform.SetParent (audioSource.transform, false);

				tempAudioSource.pitch = pitch;
			
				tempAudioSource.priority = audioSource.priority;
				tempAudioSource.spread = audioSource.spread;
				tempAudioSource.spatialBlend = audioSource.spatialBlend;
				tempAudioSource.rolloffMode = audioSource.rolloffMode;
				tempAudioSource.spread = audioSource.spread;
				tempAudioSource.maxDistance = audioSource.maxDistance;
				tempAudioSource.minDistance = audioSource.minDistance;
				tempAudioSource.loop = false;
				tempAudioSource.dopplerLevel = audioSource.dopplerLevel;
				tempAudioSource.outputAudioMixerGroup = audioSource.outputAudioMixerGroup;
				tempAudioSource.mute = audioSource.mute;

				GameObject.Destroy (tempAudioSource.gameObject, clip.length);
				audioSource = tempAudioSource;
			}
			audioSource.PlayOneShot (clip, volumeScale);
		}
	}
}
