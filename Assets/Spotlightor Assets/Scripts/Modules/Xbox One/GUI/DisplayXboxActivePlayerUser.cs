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
[RequireComponent (typeof(XboxUserDisplayer))]
public class DisplayXboxActivePlayerUser : MonoBehaviour
{
	#if UNITY_XBOXONE
	public uint playerIndex = 0;
	[SingleLineLabel ()]
	public bool updateOnActivePlayerChange = true;
	private XboxUser displayingXboxUser;

	private XboxPlayer DisplayingPlayer {
		get { return XboxActivePlayers.Players [(int)playerIndex]; }
	}

	void OnEnable ()
	{
		UpdateDisplay ();

		if (updateOnActivePlayerChange)
			DisplayingPlayer.UserChanged += HandleXboxActivePlayerUserChanged;
		
		UsersManager.OnDisplayInfoChanged += HandleDisplayInfoChanged;
	}

	void OnDisable ()
	{
		DisplayingPlayer.UserChanged -= HandleXboxActivePlayerUserChanged;
		UsersManager.OnDisplayInfoChanged -= HandleDisplayInfoChanged;
	}

	void HandleXboxActivePlayerUserChanged (XboxPlayer xboxPlayer, int newUserId, int oldUserId)
	{
		UpdateDisplay ();
	}

	void HandleDisplayInfoChanged (int id)
	{
		this.Log ("User {0} display info changed", id);
		if (id == DisplayingPlayer.UserId) {
			this.Log ("Displaying user {0} displayInfo changed, update display.", id);
			UpdateDisplay ();
		}
	}

	private void UpdateDisplay ()
	{
		displayingXboxUser = new XboxUser (DisplayingPlayer.User);
		GetComponent<XboxUserDisplayer> ().UpdateDisplay (displayingXboxUser);
	}
	#endif
}
