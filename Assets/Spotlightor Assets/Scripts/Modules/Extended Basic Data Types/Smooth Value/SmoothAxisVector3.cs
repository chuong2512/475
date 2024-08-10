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
public class SmoothAxisVector3
{
	public SmoothFloat x = new SmoothFloat (0.5f, 0.5f, 0, 10);
	public SmoothFloat y = new SmoothFloat (0.5f, 0.5f, 0, 10);
	public SmoothFloat z = new SmoothFloat (0.5f, 0.5f, 0, 10);

	public Vector3 Value {
		get { return new Vector3 (x, y, z); }
		set {
			x.Value = value.x;
			y.Value = value.y;
			z.Value = value.z;
		}
	}

	public void FollowToTargetValue (Vector3 targetValue)
	{
		FollowToTargetValue (targetValue, Time.deltaTime);
	}

	public void FollowToTargetValue (Vector3 targetValue, float deltaTime)
	{
		x.FollowToTargetValue (targetValue.x, deltaTime);
		y.FollowToTargetValue (targetValue.y, deltaTime);
		z.FollowToTargetValue (targetValue.z, deltaTime);
	}

	public void InertiaToDefaultValue ()
	{
		InertiaToDefaultValue (Time.deltaTime);
	}

	public void InertiaToDefaultValue (float deltaTime)
	{
		x.InertiaToDefaultValue (deltaTime);
		y.InertiaToDefaultValue (deltaTime);
		z.InertiaToDefaultValue (deltaTime);
	}

	public static implicit operator Vector3 (SmoothAxisVector3 smoothAxisVector3)
	{
		return new Vector3 (smoothAxisVector3.x, smoothAxisVector3.y, smoothAxisVector3.z);
	}
}
