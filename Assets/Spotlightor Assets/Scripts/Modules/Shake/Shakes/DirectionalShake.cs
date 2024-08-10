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

public class DirectionalShake : TimedShake
{
	public Vector3 directionalIntensity = Vector3.up;
	public AnimationCurve intensityOverTime = new AnimationCurve (new Keyframe (0, 0), new Keyframe (0.05f, 1), new Keyframe (1, 0));

	protected override Vector3 GetIntensityAtTime (float time, float deltaTime)
	{
		return directionalIntensity * intensityOverTime.Evaluate (this.NormalizedTime);
	}
}
