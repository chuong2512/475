/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class ParticleSystemsPlayer : MonoBehaviour
{
	private List<ParticleSystem> childParticleSystems;

	private List<ParticleSystem> ChildParticleSystems {
		get {
			if (childParticleSystems == null)
				childParticleSystems = new List<ParticleSystem> (GetComponentsInChildren<ParticleSystem> (true));
			return childParticleSystems;
		}
	}

	public UnityEvent onPlay;
	public UnityEvent onStop;

	public void PlayAfterDelay (float delay)
	{
		Invoke ("Play", delay);
	}

	public void Play ()
	{
		ChildParticleSystems.ForEach (p => p.Play ());
		onPlay.Invoke ();
	}

	public void StopAfterDelay (float delay)
	{
		Invoke ("Stop", delay);
	}

	public void Stop ()
	{
		ChildParticleSystems.ForEach (p => p.Stop ());
		onStop.Invoke ();
	}
}
