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

[System.Serializable]
public class SmoothFloat : SmoothValue<float>
{
	private float valueChangeVelocity = 0;

	public SmoothFloat (float followSmoothTime, float inertiaSmoothTime, float defaultValue, float maxSpeed) : base (followSmoothTime, inertiaSmoothTime, defaultValue, maxSpeed)
	{
	}

	protected override void OnValueSet ()
	{
		valueChangeVelocity = 0;
	}

	protected override float SmoothDampTo (float currentValue, float targetValue, float smoothTime, float deltaTime)
	{
		if (smoothTime > 0)
			return Mathf.SmoothDamp (currentValue, targetValue, ref valueChangeVelocity, smoothTime, maxSpeed, deltaTime);
		else {
			valueChangeVelocity = 0;
			return targetValue;
		}
	}

	public static implicit operator float (SmoothFloat smoothFloat)
	{
		return smoothFloat.Value;
	}

}
