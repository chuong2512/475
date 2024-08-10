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

[RequireComponent (typeof(GameCameraMovement))]
public class GameCameraRotateByEdgeHit : MonoBehaviour
{
	private GameCameraMovement CameraMovement{ get { return GetComponent<GameCameraMovement> (); } }

	void Start ()
	{
		Player.Instance.Walker.EdgeHit += HandlePlayerWalkerEdgeHit;
	}

	void HandlePlayerWalkerEdgeHit (StickyWalker walker, Vector3 edgeGroundNormal)
	{
		Vector3 groundNormalInCameraView = Camera.main.transform.InverseTransformDirection (edgeGroundNormal);
		if (Mathf.Abs (groundNormalInCameraView.x) > 0.01f) {
			if (!CameraMovement.yawAxisSetting.IsTurning) {
				if (groundNormalInCameraView.x < 0)
					CameraMovement.YawTurn (true);
				else
					CameraMovement.YawTurn (false);
			} 
		} else {
			if (!CameraMovement.pitchAxisSetting.IsTurning) {
				if (groundNormalInCameraView.y > 0)
					CameraMovement.PitchTurn (true);
				else
					CameraMovement.PitchTurn (false);
			}
		}
	}
}