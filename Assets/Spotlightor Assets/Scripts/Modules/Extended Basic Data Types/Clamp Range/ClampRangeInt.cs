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
public class ClampRangeInt : ClampRange<int>
{
	public ClampRangeInt (int min, int max) : base (min, max)
	{
	}

	public override int Clamp (int value)
	{
		return Mathf.Clamp (value, min, max);
	}

	public override bool Contains (int value)
	{
		return value <= max && value >= min;
	}

	public float InverseLerp (int value)
	{
		return Mathf.InverseLerp (min, max, value);
	}

	public int Lerp (float t)
	{
		return (int)Mathf.Lerp (min, max, t);
	}

	public int LerpUnclamped (float t)
	{
		return (int)Mathf.LerpUnclamped (min, max, t);
	}
}
