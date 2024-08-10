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

public class GameSceneStateGame : GameSceneState
{
	public override GameSceneStateTypes StateId { get { return GameSceneStateTypes.Game; } }

	public Transform gameSectionsRoot;
	private List<GameSection> gameSections;
	private int nextGameSectionIndex = 0;

	public GameSection CurrentGameSection { get; private set; }

	public bool Win{ get { return HasFinishedAllSections && HuntGame.Instance.LastGameWin; } }

	public bool HasFinishedAllSections{ get { return nextGameSectionIndex >= GameSections.Count; } }

	public List<GameSection> GameSections {
		get {
			if (gameSections == null)
				gameSections = new List<GameSection> (gameSectionsRoot.GetComponentsInChildren<GameSection> ());
			
			return gameSections;
		}
	}

	public override void BeginState (GameSceneStateTypes previousState)
	{
		base.BeginState (previousState);

		Player.Instance.Controllable = true;
		PlayerCopySpawner.Instance.AllCopiesPaused = false;

		if (CurrentGameSection == null)
			StartNextGameSection ();

		if (CurrentGameSection.playHuntGame)
			HuntGame.Instance.Ended += HandleHuntGameEnded;
	}

	private void StartNextGameSection ()
	{
		CurrentGameSection = GameSections [nextGameSectionIndex];
		nextGameSectionIndex++;

		CurrentGameSection.PrepareGame ();
	}

	public override void EndState (GameSceneStateTypes newState)
	{
		base.EndState (newState);

		Player.Instance.Controllable = false;
		HuntGame.Instance.Ended -= HandleHuntGameEnded;
	}

	void HandleHuntGameEnded (HuntGame huntGame, bool win)
	{
		EndCurrentGameSection (win);
	}

	public void EndCurrentGameSection (bool win)
	{
		CurrentGameSection = null;

		if (win) {
			if (!HasFinishedAllSections)
				StartNextGameSection ();
			else
				Owner.FiniteStateMachine.GotoState (GameSceneStateTypes.End);
		} else
			Owner.FiniteStateMachine.GotoState (GameSceneStateTypes.End);
	}

	public void SetNextGameSection (GameSection nextGameSection)
	{
		int index = GameSections.IndexOf (nextGameSection);
		if (index >= 0)
			nextGameSectionIndex = index;
	}
}
