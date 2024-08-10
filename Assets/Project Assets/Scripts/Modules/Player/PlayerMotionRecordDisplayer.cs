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

public class PlayerMotionRecordDisplayer : MonoBehaviour
{
	public PlayerMotionRecordPlayer recordPlayer;
	public float forwardTime = 1;
	public float pointsCount = 5;
	public Transform pointPrefab;

	public float minScale = 0.2f;

	private List<Transform> points = new List<Transform> ();

	void Start ()
	{
		for (int i = 0; i < pointsCount; i++) {
			Transform point = GameObject.Instantiate<Transform> (pointPrefab);
			point.SetParent (transform, false);
			point.SetUniformLocalScale (Mathf.Lerp (1, minScale, Mathf.InverseLerp (0, pointsCount - 1, i)));

			points.Add (point);
		}
	}

	void LateUpdate ()
	{
		if (recordPlayer != null && recordPlayer.motionRecord != null) {
			float timeSplit = forwardTime / (float)pointsCount;
			int currentRecordIndex = recordPlayer.CurrentRecordIndex;
			float baseTime = recordPlayer.motionRecord.Records [currentRecordIndex].time;
			float nextTime = baseTime;
			for (int i = 0; i < points.Count; i++) {
				float totalTime = recordPlayer.motionRecord.TotalTime;
				PlayerMotionRecord.Record record = recordPlayer.motionRecord.Records [currentRecordIndex];
				do {
					currentRecordIndex++;
					if (currentRecordIndex >= recordPlayer.motionRecord.Records.Count) {
						currentRecordIndex = 0;
						baseTime -= totalTime;
					}
					record = recordPlayer.motionRecord.Records [currentRecordIndex];
					nextTime = record.time;
				} while(timeSplit < totalTime && nextTime - baseTime < timeSplit);

				Transform point = points [i];
				point.position = record.worldPosition;
				point.eulerAngles = record.worldEulerAngles;
				point.gameObject.SetActive (true);



				baseTime = nextTime;
			}
		}
	}
}
