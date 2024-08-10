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

public class PlayAnimationByMatineeAction : MonoBehaviour
{
	[System.Serializable]
	public class AnimationSetting
	{
		public MatineeAction action;
		public string actionStartAnimation;
		public string actionCompletedAnimation;
		private Animation target;

		public void RegisterActionEvents (Animation animationTarget)
		{
			target = animationTarget;
			action.Started += OnActionStarted;
			action.Completed += OnActionCompleted;
		}
		
		public void UnregisterActionEvents ()
		{
			action.Started -= OnActionStarted;
			action.Completed -= OnActionCompleted;
		}

		void OnActionStarted (MatineeAction action)
		{
			if (actionStartAnimation != "")
				target.CrossFade (actionStartAnimation);
		}
		
		void OnActionCompleted (MatineeAction action)
		{
			if (actionCompletedAnimation != "")
				target.CrossFade (actionCompletedAnimation);
		}
	}
	
	public Animation animationTarget;
	public AnimationSetting[] animationSettings;
	
	void OnEnable ()
	{
		foreach (AnimationSetting animationSetting in animationSettings) {
			animationSetting.RegisterActionEvents (animationTarget);
		}
	}
	
	void OnDisable ()
	{
		foreach (AnimationSetting animationSetting in animationSettings) {
			animationSetting.UnregisterActionEvents ();
		}
	}
}
