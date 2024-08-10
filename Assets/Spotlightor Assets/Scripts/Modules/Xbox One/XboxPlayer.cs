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

#if UNITY_XBOXONE
using Users;
#endif
public class XboxPlayer
{
	private class ControllerUserPairValidator : MonoBehaviour
	{
		public XboxPlayer player;

		void Update ()
		{
			if (player != null)
				player.CheckControllerUserPairValid ();
		}
	}

	private const int InvalidUserId = -1;

	public delegate void GenericEventHandler (XboxPlayer xboxPlayer);

	public delegate void ControllerChangedEventHandler (XboxPlayer xboxPlayer, ulong newControllerId, ulong oldControllerId);

	public delegate void UserChangedEventHandler (XboxPlayer xboxPlayer, int newUserId, int oldUserId);

	public event GenericEventHandler BecameValid;
	public event GenericEventHandler BecameInvalid;
	public event ControllerChangedEventHandler ControllerChanged;
	public event UserChangedEventHandler UserChanged;

	private ulong controllerId = 0;
	private int userId = InvalidUserId;
	private bool isControllerUserPairValid = false;
	private ControllerUserPairValidator validator;

	private ControllerUserPairValidator Validator {
		get {
			if (validator == null) {
				validator = new GameObject ("XboxPlayer Validator").AddComponent<ControllerUserPairValidator> ();
				validator.player = this;
				GameObject.DontDestroyOnLoad (validator.gameObject);
			}
			return validator;
		}
	}

	public bool IsControllerUserPairValid {
		get { return isControllerUserPairValid; }
		private set {
			if (isControllerUserPairValid != value) {
				isControllerUserPairValid = value;
				if (value) {
					if (BecameValid != null)
						BecameValid (this);
				} else {
					if (BecameInvalid != null)
						BecameInvalid (this);
				}
			}
		}
	}

	public bool HasSet {
		get { return ControllerId > 0 && UserId > InvalidUserId; }
	}

	public ulong ControllerId {
		get { return controllerId; }
		set {
			if (controllerId != value) {
				ulong oldControllerId = controllerId;
				controllerId = value;

				CheckControllerUserPairValid ();

				if (ControllerChanged != null)
					ControllerChanged (this, controllerId, oldControllerId);

				Validator.enabled = HasSet;
			}
		}
	}

	#if UNITY_XBOXONE
	public User User {
		get { return UserId >= 0 ? UsersManager.FindUserById (UserId) : null; }
	}
	#endif

	public int UserId {
		get { return userId; }
		set {
			if (value != userId) {
				int oldUserId = userId;
				userId = value;

				CheckControllerUserPairValid ();

				if (UserChanged != null)
					UserChanged (this, userId, oldUserId);

				Validator.enabled = HasSet;
			}
		}
	}
		
	void CheckControllerUserPairValid ()
	{
		bool isPairValid = false;

		#if UNITY_XBOXONE
		uint joystickId = XboxOneInput.GetJoystickId (controllerId);
		if (joystickId > 0 && XboxOneInput.IsGamepadActive (joystickId)) {
			int controllerPairedUserId = XboxOneInput.GetUserIdForGamepad (joystickId);
			if (userId == controllerPairedUserId) {
				User user = UsersManager.FindUserById (userId);
				if (user != null && user.IsSignedIn)
					isPairValid = true;
			}
		}
		#endif

		this.IsControllerUserPairValid = isPairValid;
	}

	public void Clear ()
	{
		this.ControllerId = 0;
		this.UserId = InvalidUserId;
	}

	public override string ToString ()
	{
		return string.Format ("[XboxPlayer: HasSet={0}, ValidPair={1}, UserId={2}, ControllerId={3}]", HasSet, IsControllerUserPairValid, UserId, ControllerId);
	}
}
