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

/// <summary>
/// Play default animation clip forward & backward using normalized time.
/// </summary>
public class AnimationNormalizedController : FLEventDispatcherMono {
	private AnimationState _animState;
	private float _animLength;
	private Animation _animationComponent;
	public AnimationState AnimState {
		get{
			if(_animState == null){
				_animState = AnimationComponent[AnimationComponent.clip.name];
				// set up the animation state so it can be controlled by normalized time.
				_animState.weight = 1;
				_animState.enabled = true;
				_animState.speed = 0;
				_animState.wrapMode = WrapMode.ClampForever;
			}
			return _animState;
		}
	}
	public Animation AnimationComponent{
		get {
			if(_animationComponent == null){
				_animationComponent = GetComponent<Animation>();
				_animationComponent.playAutomatically = false;
				_animationComponent.Stop();
			}
			return _animationComponent;
		}
	}
	public float AnimLength{
		get {
			return AnimationComponent.clip.length;
		}
	}
	
	public bool IsPlaying{
		get {
			return AnimationComponent.enabled;
		}
	}
	
	public void SampleAtTime(float targetNormalizedTime)
	{
		StopCoroutine("TweenAnimationTo");
		AnimState.normalizedTime = targetNormalizedTime;
		AnimationComponent.Sample();
		AnimationComponent.enabled = false;
	}
	
	public void PlayTo(float targetNormalizedTime){
		StopCoroutine("TweenAnimationTo");
		StartCoroutine("TweenAnimationTo", targetNormalizedTime);
	}
	
	IEnumerator TweenAnimationTo(float targetNormalizedTime){
		AnimationComponent.enabled = true;
		
		if (AnimState.normalizedTime < 0) AnimState.normalizedTime = 0;
		
		float timeElapsed = 0;
		float duration = Mathf.Abs(targetNormalizedTime - AnimState.normalizedTime) * AnimLength;
		float startNormalizedTime = AnimState.normalizedTime;
		yield return 0;
		
		while(timeElapsed < duration){
			yield return null;
			timeElapsed += Time.deltaTime;
			AnimState.normalizedTime = Mathf.Lerp(startNormalizedTime, targetNormalizedTime, Mathf.Clamp01(timeElapsed / duration));
		}
		
		AnimState.normalizedTime = targetNormalizedTime;
		AnimationComponent.Sample();
		AnimationComponent.enabled = false;
		
		DispatchEvent(new FLEvent(FLEvent.COMPLETE));
	}
}
