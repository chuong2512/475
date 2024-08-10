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

public abstract class SmoothValue<T>
{
	public float followSmoothTime = 0.2f;
	public float inertiaSmoothTime = 0.5f;
	public float maxSpeed;
	public T defaultValue;
	private T value;
	private bool valueInitialized = false;

	public T Value {
		get {
			if (!valueInitialized) {
				this.value = defaultValue;
				valueInitialized = true;
			}
			return this.value;
		}
		set {
			this.value = value;

			OnValueSet ();

			if (!valueInitialized)
				valueInitialized = true;
		}
	}

	public SmoothValue ()
	{
		
	}

	public SmoothValue (float followSmoothTime, float inertiaSmoothTime, T defaultValue, float maxSpeed)
	{
		this.followSmoothTime = followSmoothTime;
		this.inertiaSmoothTime = inertiaSmoothTime;
		this.defaultValue = this.Value = defaultValue;
		this.maxSpeed = maxSpeed;
	}

	protected abstract void OnValueSet ();

	public void FollowToTargetValue (T targetValue)
	{
		FollowToTargetValue (targetValue, Time.deltaTime);
	}

	public void FollowToTargetValue (T targetValue, float deltaTime)
	{
		if (followSmoothTime > 0) {
			if (deltaTime > 0)
				this.value = SmoothDampTo (this.Value, targetValue, followSmoothTime, deltaTime);
		} else
			this.value = targetValue;
	}

	public void InertiaToDefaultValue ()
	{
		InertiaToDefaultValue (Time.deltaTime);
	}

	public void InertiaToDefaultValue (float deltaTime)
	{
		InertiaToValue (this.defaultValue, deltaTime);
	}

	public void InertiaToValue (T targetValue, float deltaTime)
	{
		if (inertiaSmoothTime > 0) {
			if (deltaTime > 0)
				this.value = SmoothDampTo (this.Value, targetValue, inertiaSmoothTime, deltaTime);
		} else
			this.value = targetValue;
	}

	protected abstract T SmoothDampTo (T currentValue, T targetValue, float smoothTime, float deltaTime);
}
