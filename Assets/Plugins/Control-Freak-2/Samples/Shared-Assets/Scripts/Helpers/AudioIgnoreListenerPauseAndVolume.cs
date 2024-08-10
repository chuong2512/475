/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

// -------------------------------------------
// Control Freak 2
// Copyright (C) 2013-2018 Dan's Game Tools
// http://DansGameTools.blogspot.com
// -------------------------------------------

using UnityEngine;

namespace ControlFreak2.Demos.Helpers
{

public class AudioIgnoreListenerPauseAndVolume : MonoBehaviour 
	{
	public AudioSource[]
		targetSources;

	public bool ignoreListenerVolume = true;
	public bool ignoreListenerPause = true;


	// -----------------------	
	void OnEnable()
		{
		if ((this.targetSources == null) || (this.targetSources.Length == 0))
			this.targetSources = this.GetComponents<AudioSource>();

		for (int i = 0; i < this.targetSources.Length; ++i)
			{
			if (this.targetSources[i] != null)
				{
				this.targetSources[i].ignoreListenerPause = this.ignoreListenerPause;
				this.targetSources[i].ignoreListenerVolume = this.ignoreListenerVolume;
				}
			}
		}


	}
}
