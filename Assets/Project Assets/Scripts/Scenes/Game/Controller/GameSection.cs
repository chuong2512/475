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
using UnityEngine.Events;

public class GameSection : MonoBehaviour
{
	public bool playHuntGame = true;
	public HuntGame.Rules huntGameRules;

	[Header ("Player")]
	public bool canClimbWall = true;
	public bool autoCameraRotation = true;

	[TextArea ()]
	public string opDialog = "";
	[TextArea ()]
	public string treasureDialog = "";

	[Header ("Events")]
	public UnityEvent onPrepare;

	public void PrepareGame ()
	{
		if (playHuntGame) {
			HuntGame.Instance.rules = huntGameRules;
			HuntGame.Instance.StartGame ();
		}
		
		Player.Instance.Walker.canClimbWall = canClimbWall;
		GameCameraMovement.Instance.RotateByEdgeHit = autoCameraRotation;

		if (!string.IsNullOrEmpty (opDialog))
			DialogPopup.Instance.Display (opDialog);
		else
			DialogPopup.Instance.Hide ();

		Finish.Instance.dialogText = treasureDialog;

		onPrepare.Invoke ();
	}
}
