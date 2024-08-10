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

[RequireComponent (typeof(AudioSource))]
public class AudioTransitionManager : ValueTransitionManager
{
	public enum AutoControlTypes
	{
		None,
		AutoPlayStop,
		AutoPlayPause,
	}

	[System.Serializable]
	public class AudioStateSetting
	{
		[Range (0, 1)]
		public float volume = 1;

		public AudioStateSetting (float volume)
		{
			this.volume = volume;
		}
	}

	public AudioStateSetting inAudioSetting = new AudioStateSetting (1);
	public AudioStateSetting outAudioSetting = new AudioStateSetting (0);
	public AutoControlTypes autoControl = AutoControlTypes.None;

	private AudioSource MyAudio{ get { return GetComponent<AudioSource> (); } }

	protected override void OnProgressValueUpdated (float progress)
	{
		MyAudio.volume = Mathf.Lerp (outAudioSetting.volume, inAudioSetting.volume, progress);
	}

	protected override void Awake ()
	{
		this.TransitionInStarted += HandleTransitionInStarted;
		this.TransitionOutCompleted += HandleTransitionOutCompleted;

		base.Awake ();
	}

	void HandleTransitionInStarted (TransitionManager source, bool isInstant, StateTypes prevStateType)
	{
		if (autoControl != AutoControlTypes.None && !MyAudio.isPlaying)
			MyAudio.Play ();
	}

	void HandleTransitionOutCompleted (TransitionManager source)
	{
		if (autoControl != AutoControlTypes.None && MyAudio.isPlaying) {
			if (autoControl == AutoControlTypes.AutoPlayStop)
				MyAudio.Stop ();
			else if (autoControl == AutoControlTypes.AutoPlayPause)
				MyAudio.Pause ();
		}
	}

	void Reset ()
	{
		this.easeIn = this.easeOut = iTween.EaseType.linear;
	}
}
