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

public class AudioClipInstanceLimiterStorage : MonoBehaviour
{
	private static ObjectInstanceFinder<AudioClipInstanceLimiterStorage> instanceFinder = new ObjectInstanceFinder<AudioClipInstanceLimiterStorage> ();

	protected static AudioClipInstanceLimiterStorage Instance {
		get { return instanceFinder.Instance; }
	}

	public List<AudioClipInstanceLimiter> limiters;
	private AudioListener listener;

	public AudioListener Listener { get { return listener; } }

	void Start ()
	{
		listener = GameObject.FindObjectOfType<AudioListener> ();
		if (listener == null) {
			Debug.LogWarningFormat ("Cannot find AudioListener, auto destroy.");
			Destroy (this);
		}
	}

	public static bool TryToPlayAudioClip (AudioSource audioSource)
	{
		return TryToPlayAudioClip (audioSource.clip, audioSource);
	}

	public static bool TryToPlayAudioClip (AudioClip clip, AudioSource audioSource)
	{
		bool canPlay = true;
		if (Instance != null) {
			AudioClipInstanceLimiter limiter = Instance.limiters.Find (l => l.audioClilps.Contains (clip));
			if (limiter != null && Instance.Listener != null)
				canPlay = limiter.TryToPlayFromDistance (Instance.Listener.transform.DistanceTo (audioSource));
		} else
			Debug.LogWarningFormat ("AudioClipInstanceLimiterPlayer {0} won't has limiter effect without AudioClipInstanceLimiterStorage!", audioSource.transform.HierarchyName ());
		
		return canPlay;
	}

	void Update ()
	{
		limiters.ForEach (l => l.Update ());
	}
}
