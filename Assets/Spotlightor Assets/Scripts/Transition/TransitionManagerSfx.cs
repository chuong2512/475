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

public class TransitionManagerSfx : MonoBehaviour
{
	public TransitionManager transition;
	public AudioClipPlaySetting inSound;
	public AudioClipPlaySetting inCompletedSound;
	public AudioClipPlaySetting outSound;
	public AudioClipPlaySetting outCompletedSound;
	private AudioSource myAudio;

	void Awake ()
	{
		if (transition == null)
			transition = GetComponent<TransitionManager> ();
		
		transition.TransitionInStarted += OnTransitionInStarted;
		transition.TransitionOutStarted += OnTransitionOutStarted;
		transition.TransitionOutCompleted += HandleTransitionTransitionOutCompleted;
		transition.TransitionInCompleted += HandleTransitionTransitionInCompleted;

		myAudio = GetComponent<AudioSource> ();
	}

	void OnTransitionInStarted (TransitionManager source, bool isInstant, TransitionManager.StateTypes prevStateType)
	{
		if (!isInstant)
			PlaySound (inSound);
	}

	void HandleTransitionTransitionInCompleted (TransitionManager source)
	{
		PlaySound (inCompletedSound);
	}

	void OnTransitionOutStarted (TransitionManager source, bool isInstant, TransitionManager.StateTypes prevStateType)
	{
		if (!isInstant)
			PlaySound (outSound);
	}

	void HandleTransitionTransitionOutCompleted (TransitionManager source)
	{
		PlaySound (outCompletedSound);
	}

	private void PlaySound (AudioClipPlaySetting clipPlaySetting)
	{
		if (myAudio != null)
			myAudio.PlayOneShot (clipPlaySetting);
		else
			GlobalSoundPlayer.PlaySound (clipPlaySetting);
	}

	void Reset ()
	{
		if (transition == null)
			transition = GetComponent<TransitionManager> ();
	}
}
