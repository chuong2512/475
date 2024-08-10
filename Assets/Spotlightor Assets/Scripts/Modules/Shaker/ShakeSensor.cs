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

public class ShakeSensor : MonoBehaviour
{
	public List<ShakeChannel> channels;
	public List<ShakeSource> targets;

	public Vector3 ShakeIntensity{ get; private set; }

	void Update ()
	{
		ShakeIntensity = Vector3.zero;

		if (targets != null && targets.Count > 0) {
			foreach (ShakeSource source in targets) {
				if (source.IsPlaying)
					ShakeIntensity += source.GetShakeIntensityAtPosition (transform.position);
			}
		} else {
			for (int i = 0; i < ShakeSource.globalInstances.Count; i++) {
				ShakeSource source = ShakeSource.globalInstances [i];
				if (channels.Contains (source.channel))
					ShakeIntensity += source.GetShakeIntensityAtPosition (transform.position);
			}
		}
	}
}
