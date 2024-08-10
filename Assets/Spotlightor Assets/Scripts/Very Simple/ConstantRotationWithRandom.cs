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

public class ConstantRotationWithRandom : ConstantRotation
{
	public float rotationSpeedVaration = 30;
	public Vector3 axisRandomRotation = new Vector3 (30, 30, 30);

	protected override void Start ()
	{
		rotationSpeed += Random.Range (-rotationSpeedVaration, rotationSpeedVaration);

		Vector3 axisEulerRotation = axisRandomRotation;
		axisEulerRotation.x *= Random.Range (-1f, 1f);
		axisEulerRotation.y *= Random.Range (-1f, 1f);
		axisEulerRotation.z *= Random.Range (-1f, 1f);

		this.axis = Quaternion.Euler (axisEulerRotation) * this.axis;

		base.Start ();
	}
}
