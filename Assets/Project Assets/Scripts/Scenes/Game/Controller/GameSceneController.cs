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

public class GameSceneController : MonoBehaviour
{
	private static ObjectInstanceFinder<GameSceneController> instanceFinder = new ObjectInstanceFinder<GameSceneController> ();

	public static GameSceneController Instance { get { return instanceFinder.Instance; } }

	private Fsm<GameSceneController, GameSceneStateTypes> finiteStateMachine;

	public Fsm<GameSceneController, GameSceneStateTypes> FiniteStateMachine {
		get {
			if (finiteStateMachine == null) {
				finiteStateMachine = new Fsm<GameSceneController, GameSceneStateTypes> (this);
				finiteStateMachine.AddStates (GetComponentsInChildren<GameSceneState> (true));
				finiteStateMachine.GotoState (GameSceneStateTypes.Game);
			}
			return finiteStateMachine;
		}
	}

	public GameSceneStateGame StateGame{ get { return GetComponentInChildren<GameSceneStateGame> (true); } }

	void Start ()
	{
		FiniteStateMachine.GotoState (GameSceneStateTypes.Game);
	}
}
