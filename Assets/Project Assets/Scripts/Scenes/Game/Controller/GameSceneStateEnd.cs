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

public class GameSceneStateEnd : GameSceneState
{
	public override GameSceneStateTypes StateId { get { return GameSceneStateTypes.End; } }

	[Header ("Lose")]
	public CoroutineCommandBehavior loseCommand;
	public int loseRestartCopiesLeft = 1;
	public GameSection loseRestartGameSection;

	[Header ("Win")]
	public CoroutineCommandBehavior firstWinCommand;
	public CoroutineCommandBehavior moreWinsCommand;
	private int winTimes = 0;

	public override void BeginState (GameSceneStateTypes previousState)
	{
		base.BeginState (previousState);

		Player.Instance.Controllable = false;

		StartCoroutine (EndProcess ());
	}

	private IEnumerator EndProcess ()
	{
		PlayerCopySpawner.Instance.AllCopiesPaused = true;

		bool win = Owner.StateGame.Win;

		if (win) {
			winTimes++;
			if (winTimes == 1)
				yield return StartCoroutine (firstWinCommand.ExecuteCoroutine ());
			else
				yield return StartCoroutine (moreWinsCommand.ExecuteCoroutine ());
		} else {
			if (winTimes > 0)
				yield return StartCoroutine (moreWinsCommand.ExecuteCoroutine ());
			else
				yield return StartCoroutine (loseCommand.ExecuteCoroutine ());
		}

		if (winTimes == 0)
			LoseRestartGame ();
	}

	public void LoseRestartGame ()
	{
		PlayerCopySpawner spawner = PlayerCopySpawner.Instance;
		while (spawner.PlayerCopiesCount > loseRestartCopiesLeft)
			spawner.DestroyLatestCopy ();

		if (spawner.LatestCopy != null)
			spawner.LatestCopy.RecordPlayer.Restart ();

		Player.Instance.TeleportTo (Home.Instance.transform.position);
		Player.Instance.Respawn ();

		GameCameraMovement.Instance.ResetToDefault ();

		Owner.StateGame.SetNextGameSection (loseRestartGameSection);

		Owner.FiniteStateMachine.GotoState (GameSceneStateTypes.Game);
	}
}
