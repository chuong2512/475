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

[RequireComponent (typeof(GameCameraRotateByEdgeHit))]
public class GameCameraMovement : MonoBehaviour
{
	private static ObjectInstanceFinder<GameCameraMovement> instanceFinder = new ObjectInstanceFinder<GameCameraMovement> ();

	public static GameCameraMovement Instance{ get { return instanceFinder.Instance; } }

	[System.Serializable]
	public class RotationAxisSetting
	{
		public Transform axisTransform;
		public Vector3 eulerAngleAxis = Vector3.up;
		public float deltaAngle = 90;
		public float defaultAngle = 0;
		[SerializeField]
		private SmoothFloat smoothAngle = new SmoothFloat (0.5f, 0, 0, 1000);
		private float targetAngle = 0;

		public float SmoothAngle {
			get { return smoothAngle; }
		}

		public bool IsTurning{ get { return Mathf.Abs (smoothAngle - targetAngle) > 5; } }

		public void Turn (bool positive)
		{
			targetAngle += positive ? deltaAngle : -deltaAngle;
		}

		public void ResetToDefault ()
		{
			targetAngle = smoothAngle.Value = defaultAngle;
		}

		public void Update ()
		{
			smoothAngle.FollowToTargetValue (targetAngle);
			axisTransform.localEulerAngles = eulerAngleAxis * smoothAngle;
		}
	}

	[SerializeField]
	private Transform followTarget;
	public SmoothVector3 smoothFollowPosition;
	public RotationAxisSetting yawAxisSetting;
	public RotationAxisSetting pitchAxisSetting;

	private Vector3 defaultPisition = Vector3.zero;

	public bool RotateByEdgeHit {
		get { return GetComponent<GameCameraRotateByEdgeHit> ().enabled; }
		set { GetComponent<GameCameraRotateByEdgeHit> ().enabled = value; }
	}

	public Transform FollowTarget {
		get { return followTarget; }
		set { followTarget = value; }
	}

	void Start ()
	{
		defaultPisition = transform.position;
		ResetToDefault ();
	}

	public void YawTurn (bool right)
	{
		yawAxisSetting.Turn (right);
	}

	public void PitchTurn (bool up)
	{
		pitchAxisSetting.Turn (up);
	}

	public void ResetToDefault ()
	{
		smoothFollowPosition.Value = defaultPisition;
		yawAxisSetting.ResetToDefault ();
		pitchAxisSetting.ResetToDefault ();
	}

	void Update ()
	{
		smoothFollowPosition.FollowToTargetValue (FollowTarget.position);
		transform.position = smoothFollowPosition;

		yawAxisSetting.Update ();
		pitchAxisSetting.Update ();
	}
}
