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

public static class XboxActivePlayers
{
	public const int MaxPlayersCount = 8;
	private static List<XboxPlayer> players;

	public static XboxPlayer First{ get { return Players [0]; } }

	public static List<XboxPlayer> Players {
		get {
			if (players == null) {
				players = new List<XboxPlayer> ();
				for (int i = 0; i < MaxPlayersCount; i++)
					players.Add (new XboxPlayer ());
			}
			return players;
		}
	}
}
