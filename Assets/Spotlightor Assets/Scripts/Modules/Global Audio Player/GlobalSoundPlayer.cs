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

public static class GlobalSoundPlayer
{
	private static AudioSource audio;

	private static AudioSource Audio {
		get {
			if (audio == null) {
				GameObject audioGo = new GameObject ("Global Sound Player [Dont Destroy]");
				audio = audioGo.AddComponent<AudioSource> ();
#if UNITY_5
				audio.spatialBlend = 0;
				audio.bypassReverbZones = true;
				audio.reverbZoneMix = 0;
#endif
				GameObject.DontDestroyOnLoad (audioGo);
			}
			return audio;
		}
	}
	
	public static void PlaySound (AudioClipPlaySetting soundSetting)
	{
		PlaySound (soundSetting.ClipToPlay, soundSetting.volumeScale, soundSetting.pitch);
	}

	public static void PlaySound (AudioClip audioClip)
	{
		PlaySound (audioClip, 1, 1);
	}
	
	public static void PlaySound (AudioClip audioClip, float volume)
	{
		PlaySound (audioClip, volume, 1);
	}
	
	public static void PlaySound (AudioClip audioClip, float volume, float pitch)
	{
		if (audioClip != null) 
			Audio.PlayOneShot (audioClip, volume, pitch);
	}
}
