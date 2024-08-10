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

public class PlayerMotionRecordPlayer : MonoBehaviour
{
	public PlayerMotionRecord motionRecord;

	public bool IsPaused{ get ; set; }

	public int CurrentRecordIndex{ get; private set; }

	public void Restart ()
	{
		StopCoroutine ("PlayMotionRecord");
		StartCoroutine ("PlayMotionRecord");
	}

	IEnumerator PlayMotionRecord ()
	{
		while (motionRecord == null)
			yield return null;

		if (motionRecord.Records.Count == 0) {
			Destroy (gameObject);
			yield return null;
		}

		int playTimes = 0;
		while (true) {
			int startIndex = playTimes == 0 ? motionRecord.FinishRecordIndex : 0;
			float timeElapsed = 0;
			if (startIndex > 0)
				timeElapsed = motionRecord.Records [startIndex].time;
			for (int i = startIndex; i < motionRecord.Records.Count; i++) {
				PlayerMotionRecord.Record record = motionRecord.Records [i];
				transform.position = record.worldPosition;
				transform.eulerAngles = record.worldEulerAngles;

				CurrentRecordIndex = i;

				while (IsPaused)
					yield return null;

				while (timeElapsed < record.time) {
					yield return null;
					timeElapsed += Time.deltaTime;
				}
			}
			playTimes++;
		}
	}
}
