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

public class SetupAnimationLayer : MonoBehaviour
{
	[System.Serializable]
	public class AnimationLayerSetting
	{
		public string animationName;
		public int layer = 1;
	}
	
	public Animation target;
	public AnimationLayerSetting[] layerSettings;
	
	void Awake ()
	{
		if (target == null)
			target = GetComponent<Animation>();
		if (target != null) {
			foreach (AnimationLayerSetting setting in layerSettings) {
				target [setting.animationName].layer = setting.layer;
			}
		} else
			this.LogWarning ("Target is null and has no Animation component!");
		
		Destroy (this);
	}
	
	void Reset ()
	{
		if (GetComponent<Animation>() != null)
			target = GetComponent<Animation>();
		else
			target = GetComponentInChildren<Animation> ();
	}
}
