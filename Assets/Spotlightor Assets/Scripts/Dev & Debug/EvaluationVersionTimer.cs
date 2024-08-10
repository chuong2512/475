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
using System;

public class EvaluationVersionTimer : MonoBehaviour
{
	private static EvaluationVersionTimer instance = null;

	[TextArea ()]
	public string timeLimitReachedText = "DEMO TIME EXPIRED";
	public string clickOpenUrl = "";
	private bool timeLimitReached = false;
	private EvaluationVersionTimerSettings settings;

	void Start ()
	{
		if (instance == null && EvaluationVersionTimerSettings.Instance != null && EvaluationVersionTimerSettings.Instance.enabled) {
			instance = this;
			transform.SetParent (null);
			DontDestroyOnLoad (gameObject);
			settings = EvaluationVersionTimerSettings.Instance;

			StartCoroutine (DemoVersionTimeLimitCountDown ());
		} else
			Destroy (gameObject);
	}

	private IEnumerator DemoVersionTimeLimitCountDown ()
	{
		if (settings.DueDateTime.CompareTo (DateTime.Now) > 0) {
			while (Time.realtimeSinceStartup / 60f < settings.timeLimitInMinutes)
				yield return new WaitForSeconds (1);
		}
		
		timeLimitReached = true;
		Time.timeScale = 0;
	}

	void OnGUI ()
	{
		if (timeLimitReached) {
			if (GUI.Button (new Rect (0, Screen.height * 0.5f - 30, Screen.width, 60), timeLimitReachedText))
				Application.OpenURL (clickOpenUrl);
		}
	}

	void OnDisable ()
	{
		Time.timeScale = 1;
	}
}
