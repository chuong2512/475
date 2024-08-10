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

public class WaitXboxPlayerSaveEngineInstallation : CoroutineCommandBehavior
{
	public int playerIndex = 0;
	public XboxOneSaveEngine saveEngine;

	protected override IEnumerator CoroutineCommand ()
	{
		XboxPlayer player = XboxActivePlayers.Players [playerIndex];

		this.Log ("Wait Xbox Save Engine {0} installed for XboxPlayer {1}", saveEngine, player);
		while ((saveEngine.State == XboxOneSaveEngine.StateTypes.Installed && player.UserId == saveEngine.UserId) == false)
			yield return null;

		this.Log ("Wait installation completed.");
	}
}
