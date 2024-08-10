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

public class PlayerMotionRecorder : MonoBehaviour
{
	private List<PlayerMotionRecord> allRecords = new List<PlayerMotionRecord> ();
	private PlayerMotionRecord currentRecord;

	public List<PlayerMotionRecord> AllRecords { get { return allRecords; } }

	public PlayerMotionRecord CurrentRecord {
		get {
			return allRecords.Count > 0 ? allRecords [allRecords.Count - 1] : null;
		}
	}

	void OnEnable ()
	{
		HuntGame.Instance.RoundStarted += HandleHuntGameRoundStarted;
		HuntGame.Instance.Ended += HandleHuntGameEnded;
	}

	void OnDisable ()
	{
		if (HuntGame.Instance != null) {
			HuntGame.Instance.RoundStarted -= HandleHuntGameRoundStarted;
			HuntGame.Instance.Ended -= HandleHuntGameEnded;
		}
	}

	void HandleHuntGameRoundStarted (HuntGame huntGame)
	{
		StartNewRecord ();
		Debug.Log ("New motion record started");
	}

	void HandleHuntGameEnded (HuntGame huntGame, bool win)
	{
		if (win == false)
			RemoveCurrentRecord ();
	}

	private void StartNewRecord ()
	{
		allRecords.Add (new PlayerMotionRecord ());
	}

	private void RemoveCurrentRecord ()
	{
		if (allRecords.Count > 0) {
			allRecords.RemoveAt (allRecords.Count - 1);
			Debug.Log ("Current motion record removed, because hunt game lose.");
		}
	}

	void LateUpdate ()
	{
		if (GameSceneController.Instance.FiniteStateMachine.CurrentState.StateId == GameSceneStateTypes.Game)
			CurrentRecord.SavePlayerCurrentState (GetComponent<Player> ());
	}
}
