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

public class XboxSinglePlayerEngager : MonoBehaviour
{
	public enum StateTypes
	{
		NotSet,
		Setup,
		Set,
		SwitchProfile,
		Watch,
		ReEngage,
	}

	private static ObjectInstanceFinder<XboxSinglePlayerEngager> instanceFinder = new ObjectInstanceFinder<XboxSinglePlayerEngager> ();

	public static XboxSinglePlayerEngager Instance{ get { return instanceFinder.Instance; } }

	public XboxEngager engager;
	public bool debug = false;

	private Fsm<XboxSinglePlayerEngager, StateTypes> finiteStateMachine;

	public Fsm<XboxSinglePlayerEngager, StateTypes> FiniteStateMachine {
		get {
			if (finiteStateMachine == null) {
				finiteStateMachine = new Fsm<XboxSinglePlayerEngager, StateTypes> (this);
				finiteStateMachine.AddStates (GetComponentsInChildren<XboxSinglePlayerEngagerState> (true));
				finiteStateMachine.GotoState (StateTypes.NotSet);
			}
			return finiteStateMachine;
		}
	}

	public XboxPlayer ActivePlayer {
		get { return XboxActivePlayers.First; }
	}

	void Awake ()
	{
		if (Instance == this) {
			if (transform.parent == null)
				DontDestroyOnLoad (gameObject);
			
			GotoState (StateTypes.NotSet);
		} else
			Destroy (gameObject);
	}

	public void GotoState (StateTypes stateType)
	{
		FiniteStateMachine.GotoState (stateType);
	}

	void OnGUI ()
	{
		if (debug) {
			GUI.Box (new Rect (30, 30, 500, 30), string.Format ("SinglePlayerEngager State: {0}", FiniteStateMachine.CurrentState.StateId));
			GUI.Box (new Rect (30, 60, 500, 30), string.Format ("ActivePlayer: {0}", ActivePlayer));
		}
	}
}
