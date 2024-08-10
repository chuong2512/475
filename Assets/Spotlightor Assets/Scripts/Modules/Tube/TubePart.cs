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

public class TubePart : MonoBehaviour
{
	public Vector3 endPosition;
	public Vector3 endRotation;

	public Vector3 EndWorldPosition{ get { return transform.TransformPoint (endPosition); } }

	public Quaternion EndWorldRotation{ get { return transform.rotation * Quaternion.Euler (endRotation); } }

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.white;
		float pointRadius = 0.1f;
		Gizmos.DrawWireSphere (transform.TransformPoint (endPosition), pointRadius);
		Gizmos.DrawRay (transform.TransformPoint (endPosition), Quaternion.Euler (endRotation) * transform.forward);
	}
}
