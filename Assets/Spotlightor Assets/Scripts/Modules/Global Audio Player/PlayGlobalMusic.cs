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

public class PlayGlobalMusic : MonoBehaviour
{
	public AudioClip audioClip;
	public bool loop = true;
	public float volume = 1;
	public float fadeInTime = 0.5f;
	public float fadeOutTime = 0.5f;

	void OnEnable ()
	{
		GlobalMusicPlayer.Instance.TransitTo (audioClip, loop, volume, fadeInTime, fadeOutTime);
	}

	public void Stop ()
	{
		if (GlobalMusicPlayer.Instance.GetComponent<AudioSource> ().clip == this.audioClip)
			GlobalMusicPlayer.Instance.Stop (fadeOutTime);
	}
}
