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

[RequireComponent (typeof(AudioSource))]
public class AudioShaker : Shaker
{
	public float intensityToVolume = 0.5f;
	public float intensityToPitch = 0.1f;

	private AudioSource audioSource;
	private float defaultVolume = 1;
	private float defaultPitch = 1;

	void Start ()
	{
		audioSource = GetComponent<AudioSource> ();

		defaultVolume = audioSource.volume;
		defaultPitch = audioSource.pitch;
	}

	public override void Shake (Vector3 intensity)
	{
		audioSource.volume = defaultVolume;
		audioSource.pitch = defaultPitch;

		float intensityMagnitude = intensity.magnitude;

		if (intensityMagnitude > 0) {
			audioSource.volume += intensityMagnitude * intensityToVolume;
			audioSource.pitch += intensityMagnitude * intensityToPitch;
		}
	}
}
