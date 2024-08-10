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

public class PlayerMotionRecord
{
	public class Record
	{
		public float time = 0;
		public Vector3 worldPosition = Vector3.zero;
		public Vector3 worldEulerAngles = Vector3.zero;

		public Record (Player player)
		{
			time = HuntGame.Instance.TimeElapsed;
			worldPosition = player.transform.position;
			worldEulerAngles = player.transform.eulerAngles;
		}
	}

	private int finishRecordIndex = 0;

	public int FinishRecordIndex {
		get {
			return finishRecordIndex;
		}
	}

	private List<Record> records = new List<Record> ();

	public List<Record> Records { get { return records; } }

	public float TotalTime {
		get {
			float totalTime = 0;
			if (records.Count > 0)
				totalTime = records [records.Count - 1].time;
			return totalTime;
		}
	}

	public void SavePlayerCurrentState (Player player)
	{
		bool addRecord = true;
		if (records.Count > 0) {
			Record lastRecord = records [records.Count - 1];
			if (Vector3.Distance (lastRecord.worldPosition, player.transform.position) < 0.02f)
				addRecord = false;
		}

		if (addRecord)
			records.Add (new Record (player));
	}

	public void SetFinish ()
	{
		finishRecordIndex = Mathf.Max (records.Count - 1, 0);
	}
}
