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

public static class RigidbodyExtensionMethods
{

	public static void SetVelocityX (this Rigidbody rigidbody, float x)
	{
		rigidbody.velocity = new Vector3 (x, rigidbody.velocity.y, rigidbody.velocity.z);
	}

	public static void SetVelocityY (this Rigidbody rigidbody, float y)
	{
		rigidbody.velocity = new Vector3 (rigidbody.velocity.x, y, rigidbody.velocity.z);
	}

	public static void SetVelocityZ (this Rigidbody rigidbody, float z)
	{
		rigidbody.velocity = new Vector3 (rigidbody.velocity.z, rigidbody.velocity.y, z);
	}
}
