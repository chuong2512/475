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

[RequireComponent(typeof(EnhancedCharacterController))]
public class CharacterMotorGroundClamp : FunctionalMonoBehaviour
{
	// Execute at last, to do the final clamp.
	[Header("Script Order: 5")]
	public float
		clampSlopeLimit = 60;
	private EnhancedCharacterController enhancedController;

	protected override void OnBecameFunctional (bool forTheFirstTime)
	{
		if (forTheFirstTime)
			enhancedController = GetComponent<EnhancedCharacterController> ();

		enhancedController.BeforeApplyMove += HandleBeforeApplyMove;
	}

	protected override void OnBecameUnFunctional ()
	{
		enhancedController.BeforeApplyMove -= HandleBeforeApplyMove;
	}

	void HandleBeforeApplyMove (EnhancedCharacterController source, Vector3 totalMotion)
	{
		if (enhancedController.IsGrounded 
			&& enhancedController.Speed.y <= 0 
			&& enhancedController.GroundHitNormal.y < 0.99f
			&& enhancedController.GroundSlopeAngle <= clampSlopeLimit) {
			ClampByRaycastPredict (totalMotion);
		}
	}
	
	private void ClampByRaycastPredict (Vector3 totalMotion)
	{
		Vector3 predictOrigin = enhancedController.GroundHitPoint + totalMotion + Vector3.up * enhancedController.SkinWidth;

		Vector3 horizontalMotion = new Vector3 (totalMotion.x, 0, totalMotion.z);
		float castDistance = Mathf.Tan (clampSlopeLimit * Mathf.Deg2Rad) * horizontalMotion.magnitude;
		castDistance += enhancedController.nearGroundDistance + enhancedController.SkinWidth;

		RaycastHit clampHit;
		if (Physics.Raycast (predictOrigin, Vector3.down, out clampHit, castDistance, enhancedController.nearGroundLayerMask)) {
			float deltaY = clampHit.point.y - predictOrigin.y;
			
			enhancedController.Move (Vector3.up * deltaY);
		}
	}
}
