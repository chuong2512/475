/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosRotation : MonoBehaviour
{
	public float rotateSpeed = 10;

	[Space ()]
	public Vector3 axisMask = Vector3.one;
	public float axisRotateSpeed = 60;

	private Vector3 axis = Vector3.one;
	private Vector3 targetAxis = Vector3.one;

	void Start ()
	{
		axis = GetMaskedRandomAxis ();
		targetAxis = GetMaskedRandomAxis ();
	}

	void Update ()
	{
		Vector3 worldAxis = axis;
		if (transform.parent != null)
			worldAxis = transform.parent.TransformDirection (axis);
		
		transform.Rotate (worldAxis, rotateSpeed * Time.deltaTime, Space.World);

		if (Vector3.Angle (axis, targetAxis) > 1)
			axis = Vector3.RotateTowards (axis, targetAxis, axisRotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 0);
		else
			targetAxis = GetMaskedRandomAxis ();
	}

	private Vector3 GetMaskedRandomAxis ()
	{
		Vector3 randomAxis = Random.rotation * Vector3.up;
		randomAxis.Scale (axisMask);
		return randomAxis.normalized;
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.DrawRay (transform.position, axis);
	}
}
