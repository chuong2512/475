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
public class ClampRangeFloat : ClampRange<float>
{
	public ClampRangeFloat (float min, float max) : base (min, max)
	{
	}

	public override float Clamp (float value)
	{
		return Mathf.Clamp (value, min, max);
	}

	public override bool Contains (float value)
	{
		return value <= max && value >= min;
	}

	public float InverseLerp (float value)
	{
		return Mathf.InverseLerp (min, max, value);
	}

	public float Lerp (float t)
	{
		return Mathf.Lerp (min, max, t);
	}

	public float LerpUnclamped (float t)
	{
		return Mathf.LerpUnclamped (min, max, t);
	}
}
