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
using System.Collections.Generic;

[RequireComponent (typeof(EnhancedCharacterController))]
[RequireComponent (typeof(CharacterMotorForwardWalker))]
// TODO: Move to CharacterMotorPushForce
public class CharacterMotorPushMessageBroadcaster : MonoBehaviour
{
	public struct PushInfo
	{
		public Vector3 force;
		public Vector3 point;
		public GameObject gameObject;
	}

	public List<string>
		pushableTags = new List<string> (){ "Pushable" };
	public float walkStrengthToForce = 1f;
	public float weightForce = 2f;
	private EnhancedCharacterController enhancedController;
	private CharacterMotorForwardWalker walker;

	void Start ()
	{
		enhancedController = GetComponent<EnhancedCharacterController> ();
		walker = GetComponent<CharacterMotorForwardWalker> ();
	}

	void OnControllerColliderHit (ControllerColliderHit hit)
	{
		if (hit.rigidbody != null && IsPushable (hit.rigidbody)) {
			PushInfo pushInfo = new PushInfo ();
			if (hit.point.y < enhancedController.BelowSphereCenter.y) {
				pushInfo.force = new Vector3 (0, -weightForce, 0);
			} else {
				pushInfo.force = walker.TargetForward * walker.ForwardStrength;
				pushInfo.force *= walkStrengthToForce;
			}
			pushInfo.point = hit.point;
			pushInfo.gameObject = gameObject;

			hit.rigidbody.SendMessage ("OnPushByCharacterMotor", pushInfo, SendMessageOptions.DontRequireReceiver);
		}
	}

	private bool IsPushable (Rigidbody targetRigidbody)
	{
		bool isPushable = false;
		foreach (string pushableTag in pushableTags) {
			if (targetRigidbody.CompareTag (pushableTag)) {
				isPushable = true;
				break;
			}
		}
		return isPushable;
	}
}
