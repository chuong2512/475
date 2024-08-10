/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;

public class ScreenshotCommand : CoroutineCommandBehavior
{
	private const string ScreenShotsFolderPath = "ScreenShots/";

	public float delay = 0;
	[Header ("留空则自动根据截图时间命名图片")]
	public string screenshotName = "";
	public int superSize = 1;

	public UnityEvent onBeforeTaken;

	protected override IEnumerator CoroutineCommand ()
	{
		yield return new WaitForSeconds (delay);

		onBeforeTaken.Invoke ();

		yield return new WaitForEndOfFrame ();

		Screenshot ();
	}

	protected virtual void Screenshot ()
	{
		string fileName = screenshotName;
		if (string.IsNullOrEmpty (fileName)) {
			DateTime now = DateTime.Now;
			fileName = string.Format ("Screenshot_{0}-{1:00}-{2:00}_{3:00}-{4:00}-{5:00}",
				now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
		}

		string filePath = ScreenShotsFolderPath + fileName + ".png";

		#if UNITY_EDITOR
		string screenshotsFolderPath = Application.dataPath + "/../" + filePath;
		screenshotsFolderPath = screenshotsFolderPath.Substring (0, screenshotsFolderPath.LastIndexOf ("/"));
		if (!System.IO.Directory.Exists (screenshotsFolderPath)) {
			System.IO.Directory.CreateDirectory (screenshotsFolderPath);
			Debug.LogFormat ("Create screenshots folder: {0}", screenshotsFolderPath);
		}
		#endif
		#if UNITY_2017_2_OR_NEWER
		UnityEngine.ScreenCapture.CaptureScreenshot (filePath, superSize);
		#else
		Application.CaptureScreenshot (filePath, superSize);
		#endif
	}
}
