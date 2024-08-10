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

[System.Serializable]
public class AudioClipInstanceLimiter
{
	public List<AudioClip> audioClilps;
	[Space ()]
	public int maxInstanceCount = 10;
	public float instanceLifeTime = 1;
	[Space ()]
	public float minInstanceInterval = 0.1f;
	[Space ()]
	public float maxDistanceToListener = 10;
	[Space ()]
	public bool logInfo = false;

	public class AudioClipInstanceInfo
	{
		public float playTime = 0;

		public AudioClipInstanceInfo ()
		{
			playTime = Time.time;
		}
	}

	private List<AudioClipInstanceInfo> activeInstanceInfos = new List<AudioClipInstanceInfo> ();

	public bool TryToPlayFromDistance (float distance)
	{
		bool canPlay = false;
		if (distance < maxDistanceToListener) {
			if (activeInstanceInfos.Count < maxInstanceCount) {
				bool passMinInstanceInterval = true;
				if (activeInstanceInfos.Count > 0) {
					float latestPlayTime = activeInstanceInfos [activeInstanceInfos.Count - 1].playTime;
					passMinInstanceInterval = Time.time - latestPlayTime >= minInstanceInterval;
				}
				if (passMinInstanceInterval) {
					activeInstanceInfos.Add (new AudioClipInstanceInfo ());
					canPlay = true;
				} else {
					if (logInfo)
						Debug.Log ("Can't play because interval < minInstanceInterval");
				}
			} else {
				if (logInfo)
					Debug.LogFormat ("Can't play because acive instance count {0} > maxInstanceCount", activeInstanceInfos.Count);
			}
		} else {
			if (logInfo)
				Debug.LogFormat ("Can't play because distance {0} < maxDistanceToListener", distance);
		}
		return canPlay;
	}

	public void Update ()
	{
		int i = 0;
		while (i < activeInstanceInfos.Count) {
			AudioClipInstanceInfo info = activeInstanceInfos [i];
			if (Time.time - info.playTime >= instanceLifeTime)
				activeInstanceInfos.RemoveAt (i);
			else
				i++;
		}
	}
}
