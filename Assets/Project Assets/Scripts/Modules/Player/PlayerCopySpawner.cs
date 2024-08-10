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

public class PlayerCopySpawner : MonoBehaviour
{
	private static ObjectInstanceFinder<PlayerCopySpawner> instanceFinder = new ObjectInstanceFinder<PlayerCopySpawner> ();

	public static PlayerCopySpawner Instance{ get { return instanceFinder.Instance; } }

	public PlayerCopy playerCopyPrefab;
	private List<PlayerCopy> playerCopies = new List<PlayerCopy> ();
	private int bestCopiesCount = 0;

	public int BestCopiesCount {
		get {
			return bestCopiesCount;
		}
	}

	public bool AllCopiesPaused {
		get {
			if (playerCopies.Count > 0)
				return playerCopies [0].RecordPlayer.IsPaused;
			else
				return false;
		}
		set { playerCopies.ForEach (p => p.RecordPlayer.IsPaused = value); }
	}

	public int PlayerCopiesCount {
		get{ return playerCopies.Count; }
	}

	public PlayerCopy LatestCopy {
		get { return playerCopies.Count > 0 ? playerCopies [playerCopies.Count - 1] : null; }
	}

	public void SpawnLastRecord (int recordIndex)
	{
		int index = Player.Instance.MotionRecorder.AllRecords.Count - recordIndex - 1;
		PlayerMotionRecord motionRecord = Player.Instance.MotionRecorder.AllRecords [index];
		Spawn (motionRecord);
	}

	public void Spawn (PlayerMotionRecord motionRecord)
	{
		PlayerCopy playerCopy = GameObject.Instantiate<PlayerCopy> (playerCopyPrefab);
		playerCopy.RecordPlayer.motionRecord = motionRecord;
		playerCopy.RecordPlayer.Restart ();

		playerCopies.Add (playerCopy);

		bestCopiesCount = Mathf.Max (bestCopiesCount, PlayerCopiesCount);
	}

	public void DestroyLatestCopy ()
	{
		if (playerCopies.Count > 0) {
			playerCopies [playerCopies.Count - 1].Destruct ();
			playerCopies.RemoveAt (playerCopies.Count - 1);
		}
	}
}
