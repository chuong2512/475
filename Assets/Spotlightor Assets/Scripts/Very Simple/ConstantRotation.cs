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

public class ConstantRotation : MonoBehaviour
{
	public float rotationSpeed = 30;
	public bool isRandomRotateDirection = false;
	public Vector3 axis = Vector3.up;
	public bool randomInitialRotation = false;
	public bool ignoreTimeScale = false;
	private float lastUpdateRealTime = 0;
	
	protected virtual void Start ()
	{
		if (isRandomRotateDirection) {
			if (Random.value >= 0.5f) {
				rotationSpeed = -rotationSpeed;
			}
		}
		if (randomInitialRotation)
			transform.Rotate (axis, Random.Range (0, 360));
	}
	
	void Update ()
	{
		float deltaTime = Time.deltaTime;
		if (ignoreTimeScale) {
			deltaTime = Time.realtimeSinceStartup - lastUpdateRealTime;
			lastUpdateRealTime = Time.realtimeSinceStartup;
		}
		transform.Rotate (axis, rotationSpeed * deltaTime);
		
	}
}

