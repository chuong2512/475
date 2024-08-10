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

public class GameSceneStateCutscene : GameSceneState
{
	public override GameSceneStateTypes StateId { get { return GameSceneStateTypes.Cutscene; } }

	private CoroutineCommandBehavior cutsceneCommand;

	public void StartCutscene (CoroutineCommandBehavior cutsceneCommand)
	{
		this.cutsceneCommand = cutsceneCommand;
		Owner.FiniteStateMachine.GotoState (GameSceneStateTypes.Cutscene);
	}

	public override void BeginState (GameSceneStateTypes previousState)
	{
		base.BeginState (previousState);

		Player.Instance.Controllable = false;

		cutsceneCommand.Execute ();
	}

	void Update ()
	{
		if (!cutsceneCommand.IsExecuting)
			Owner.FiniteStateMachine.GotoState (GameSceneStateTypes.Game);
	}
}
