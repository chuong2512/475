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

public class StaticCameraController : CameraControllerBase {
	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(transform.position, transform.forward * 1 + transform.position);
	}

	#region implemented abstract members of CameraControllerBase
	public override void BeforeTransition ()
	{
		
	}

	public override void AfterTransition ()
	{
		
	}
	#endregion
	
	public override void OnDirectionInput (float x, float y, float z)
	{
		
	}
	
	public override void OnIndexInput (uint index)
	{
		
	}
}